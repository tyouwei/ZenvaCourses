// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: Apache-2.0

using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using AmazonGameLiftPlugin.Core.CredentialManagement.Models;
using AmazonGameLiftPlugin.Core.Shared;
using AmazonGameLiftPlugin.Core.Shared.FileSystem;
using AmazonGameLiftPlugin.Core.Shared.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AmazonGameLiftPlugin.Core.CredentialManagement
{
    public class CredentialsStore : ICredentialsStore
    {
        private readonly SharedCredentialsFile _sharedFile;
        private readonly CredentialProfileStoreChain _credentialProfileStoreChain;
        private readonly IFileWrapper _file;

        public CredentialsStore(IFileWrapper fileWrapper, string filePath = default)
        {
            _file = fileWrapper ?? throw new ArgumentNullException(nameof(fileWrapper));
            _sharedFile = string.IsNullOrEmpty(filePath) ? new SharedCredentialsFile() : new SharedCredentialsFile(filePath);
            _credentialProfileStoreChain = string.IsNullOrEmpty(filePath) ? new CredentialProfileStoreChain() : new CredentialProfileStoreChain(filePath);
        }

        public SaveAwsCredentialsResponse SaveAwsCredentials(SaveAwsCredentialsRequest request)
        {
            try
            {
                var options = new CredentialProfileOptions
                {
                    AccessKey = request.AccessKey,
                    SecretKey = request.SecretKey
                };

                if (_credentialProfileStoreChain.TryGetAWSCredentials(request.ProfileName, out AWSCredentials awsCredentials))
                {
                    return Response.Fail(new SaveAwsCredentialsResponse()
                    {
                        ErrorCode = ErrorCode.ProfileAlreadyExists
                    });
                }

                var profile = new CredentialProfile(request.ProfileName, options);

                _sharedFile.RegisterProfile(profile);
                FixEndOfFile();

                return Response.Ok(new SaveAwsCredentialsResponse());
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);

                return Response.Fail(new SaveAwsCredentialsResponse()
                {
                    ErrorMessage = ex.Message
                });
            }
        }

        public RetriveAwsCredentialsResponse RetriveAwsCredentials(RetriveAwsCredentialsRequest request)
        {
            if (_credentialProfileStoreChain.TryGetAWSCredentials(request.ProfileName, out AWSCredentials awsCredentials))
            {
                ImmutableCredentials credentials = awsCredentials.GetCredentials();

                return Response.Ok(new RetriveAwsCredentialsResponse()
                {
                    AccessKey = credentials.AccessKey,
                    SecretKey = credentials.SecretKey
                });
            }

            return Response.Fail(new RetriveAwsCredentialsResponse()
            {
                ErrorCode = ErrorCode.NoProfileFound
            });
        }

        public UpdateAwsCredentialsResponse UpdateAwsCredentials(UpdateAwsCredentialsRequest request)
        {
            try
            {
                if (_sharedFile.TryGetProfile(request.ProfileName, out CredentialProfile profile))
                {
                    profile.Options.AccessKey = request.AccessKey;
                    profile.Options.SecretKey = request.SecretKey;

                    _sharedFile.RegisterProfile(profile);
                    FixEndOfFile();

                    return Response.Ok(new UpdateAwsCredentialsResponse());
                }

                return Response.Fail(new UpdateAwsCredentialsResponse()
                {
                    ErrorCode = ErrorCode.NoProfileFound
                });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);

                return Response.Fail(new UpdateAwsCredentialsResponse()
                {
                    ErrorMessage = ex.Message
                });
            }
        }

        public GetProfilesResponse GetProfiles(GetProfilesRequest request)
        {
            IEnumerable<string> profiles = _credentialProfileStoreChain.ListProfiles().Select(x => x.Name);

            return Response.Ok(new GetProfilesResponse()
            {
                Profiles = profiles
            });
        }

        private void FixEndOfFile()
        {
            string filePath = _sharedFile.FilePath;
            string text = _file.ReadAllText(filePath);
            text += Environment.NewLine;
            _file.WriteAllText(filePath, text);
        }
    }
}
