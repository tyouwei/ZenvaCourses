// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Threading;
using System.Threading.Tasks;
using AmazonGameLiftPlugin.Core.GameLiftLocalTesting.Models;
using AmazonGameLiftPlugin.Core.SettingsManagement.Models;
using UnityEditor;
using UnityEngine;

namespace AmazonGameLift.Editor
{
    /// <summary>
    /// A view model for <see cref="LocalTestWindow"/>.
    /// </summary>
    [Serializable]
    internal class LocalTest
    {
        private const int GameLiftLocalReadinessDelayMs = 10000;

        private const int MinPort = 1;
        private const int MaxPort = 65535;
        private CoreApi _coreApi;
        private TextProvider _textProvider;
        private Delay _delay;
        private ILogger _logger;
        [SerializeField]
        private Status _status = new Status();

        private int _glProcessId;
        private int _serverProcessId;
        private int _gameLiftLocalPort = 8080;

        public IReadStatus Status => _status;

        public bool IsDeploymentRunning { get; private set; }

        public string GameLiftLocalPath { get; private set; }

        public bool IsFormFilled =>
            _coreApi.FileExists(BuildExePath) && GameLiftLocalPort > 0;

        public bool IsBootstrapped =>
            GameLiftLocalPath != null && _coreApi.FileExists(GameLiftLocalPath);

        public bool CanStart => !IsDeploymentRunning && IsBootstrapped && IsFormFilled;

        public bool CanStop => IsDeploymentRunning;

        public string BuildExePath { get; set; }

        public int GameLiftLocalPort
        {
            get => _gameLiftLocalPort;
            set => _gameLiftLocalPort = Mathf.Min(Mathf.Max(MinPort, value), MaxPort);
        }

        public LocalTest(CoreApi coreApi, TextProvider textProvider, Delay delay, ILogger logger) => Restore(coreApi, textProvider, delay, logger);

        internal void Restore(CoreApi coreApi, TextProvider textProvider, Delay delay, ILogger logger)
        {
            _coreApi = coreApi ?? throw new ArgumentNullException(nameof(coreApi));
            _textProvider = textProvider ?? throw new ArgumentNullException(nameof(textProvider));
            _delay = delay ?? throw new ArgumentNullException(nameof(delay));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Refresh()
        {
            GetSettingResponse response = _coreApi.GetSetting(SettingsKeys.GameLiftLocalPath);
            GameLiftLocalPath = (response.Success && _coreApi.FileExists(response.Value)) ? response.Value : null;
            GetSettingResponse portResponse = _coreApi.GetSetting(SettingsKeys.GameLiftLocalPort);

            if (portResponse.Success)
            {
                int? port = SettingsFormatter.ParseInt(portResponse.Value);
                GameLiftLocalPort = port.HasValue ? port.Value : GameLiftLocalPort;
            }

            GetSettingResponse serverPathResponse = _coreApi.GetSetting(SettingsKeys.LocalServerPath);

            if (serverPathResponse.Success)
            {
                BuildExePath = serverPathResponse.Value;
            }
        }

        public void Save()
        {
            _coreApi.PutSettingOrClear(SettingsKeys.LocalServerPath, BuildExePath);
            _coreApi.PutSetting(SettingsKeys.GameLiftLocalPort, SettingsFormatter.FormatInt(GameLiftLocalPort));
        }

        public void Stop()
        {
            if (!CanStop)
            {
                return;
            }

            _coreApi.StopProcess(_glProcessId);
            _coreApi.StopProcess(_serverProcessId);
            IsDeploymentRunning = false;
            _status.IsDisplayed = false;
        }

        public async Task Start(CancellationToken cancellationToken = default)
        {
            if (!CanStart)
            {
                return;
            }

            StartResponse response = _coreApi.StartGameLiftLocal(GameLiftLocalPath, GameLiftLocalPort);

            if (!response.Success)
            {
                _status.IsDisplayed = true;
                string message = string.Format(_textProvider.Get(Strings.StatusLocalTestErrorTemplate), _textProvider.GetError(response.ErrorCode));
                _status.SetMessage(message, MessageType.Error);
                _logger.LogResponseError(response);
                return;
            }

            IsDeploymentRunning = true;
            _glProcessId = response.ProcessId;
            await _delay.Wait(GameLiftLocalReadinessDelayMs, cancellationToken);

            // If Stop() was called
            if (!IsDeploymentRunning)
            {
                return;
            }

            RunLocalServerResponse serverResponse = _coreApi.RunLocalServer(BuildExePath);

            if (!serverResponse.Success)
            {
                _status.IsDisplayed = true;
                string message = string.Format(_textProvider.Get(Strings.StatusLocalTestServerErrorTemplate), _textProvider.GetError(serverResponse.ErrorCode));
                _status.SetMessage(message, MessageType.Error);
                _logger.LogResponseError(serverResponse);
                return;
            }

            _serverProcessId = serverResponse.ProcessId;
            SetStatus(Strings.StatusLocalTestRunning, MessageType.Info);
        }

        private void SetStatus(string statusKey, MessageType messageType)
        {
            _status.SetMessage(_textProvider.Get(statusKey), messageType);
            _status.IsDisplayed = true;
        }
    }
}
