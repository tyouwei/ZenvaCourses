CHANGELOG

# 1.1.1 (10/13/2021)

- Deployment Scenario
    - Add support to create player session in "Single Region Fleet" deployment scenario
    - Pass back player session ID in all get_game_connection responses
- Sample Game
    - Sample game client now accepts player session ID from get_game_connection responses and pass it to the game server
      after establishing a connection
    - Sample game server now accepts player session ID from client and call AcceptPlayerSession with the provided ID.
      This fixes deployment scenarios not accounting for fulfilled player session slots
- Testing
    - Added assembly definition to run plugin unit tests via the Unity test runner
    - Fixed some unit tests

# 1.1.0 (09/28/2021)

- Files
    - Update project file structure to comply
      with [Unity standards](https://docs.unity3d.com/Manual/CustomPackages.html)
        - Move package.json into project root; remove unnecessary Unity project wrapper
        - Move Core Library under `Runtime`; change .csproj to use the vs2019 convention and updated to use dotnet build
          tool to resolve dependencies
        - Move sample game project to `Sample~`
        - Move scripts to `Scripts~`; modularize scripts and add more fail-fast mechanisms
        - Use npm to package the plugin tarball
        - Remove other misc files
    - Update assembly definition namings to comply
      with [Unity convention](https://docs.unity3d.com/Manual/cus-tests.html)
- Documentation
    - Update and re-format READMEs
    - Use the correct URL for deployment scenario help link
- Bug Fix
    - Fix bug where bootstrap S3 buckets show all buckets in all regions, add async loading for S3 bucket loading

# 1.0.0 (09/17/2021)

- Documentation
    - Update root and sample game READMEs
    - Delete plugin README in favor of AWS documentation links in the plug-in UI
    - Update Download GameLiftLocal and JDK instructions in settings UI
- Bug Fix
    - Fix "NotConfigured" font style in settings UI

# 0.2.5 (09/16/2021)

- Security
    - Add server-side encryption, versioning and audit-logging during bootstrap bucket creation
    - Add WAF/WebACL to all CloudFormation templates
- Testing
    - Add integ/load tests for CloudFormation templates
- Documentation
    - Update help links to the latest 'unity-plug-in.html' URLs
    - Add README for CloudFormation template development
    - Update fleet descriptions in CloudFormation templates
- Bug Fix
    - Remove fleet tags in CloudFormation templates
    - Fix local testing NextStep label not showing
    - Increase height for several UI windows to compensate for the recent font size and margin changes
    - Require setup steps to be run from root directory; Add more descriptive logs to the setup scripts
    - Rename several occurrences of "GameLift Unity Plugin" to "GameLift Plugin for Unity"

# 0.2.4 (09/15/2021)

- Bump up GameLiftLocal readiness delay to 10 seconds
- Add "CreatedBy" Tag for plugin-created fleets
- Move powershell scripts to bin/windows
- Update UI Strings and font styles for Settings UI
- Show distinct NextStep label for individual settings UI Panel
- Update Help URLs
- Add GameLift Server SDK doc link buttons
- Fix GameLift filepath label truncation
- Delete unnecessary project settings files
- Fix NPE in SettingsState
- Add light-themed GameLift logo

# 0.2.3 (09/14/2021)

- Update UI Strings
- Fix NullPointerException when reload plugin with all settings configured
- Update GameLift Local download URL to the S3 zip file

# 0.2.2 (09/13/2021)

- Add AWS Logo and retexture toolbar to use the default Unity toolbar styling
- Enable Local Testing UI regardless if Java is configured (Temp fix to issue #9)
- Reorder and rename sample game menu items
- Select "Single-Region Fleet" deployment scenario by default
- Removed PDF documentation and related button. Users should go to AWS docs for source of truth
- Removed redundant files related to deployment scenarios
- Ensured all files have license file headers
- Ensured all sample code have MIT-0 license instead of Apache-2.0

# 0.2.1 (09/07/2021)

- Added persistence states to settings UI

# 0.2.0 (08/31/2021)

- Fixed conflicting newtonsoft.json dll with 2020.3LTS Unity
- Revamped Settings UI
- Added a "Local Mode" and "GameLift Mode" to enable user to easily select the localhost endpoint
- Hint users that they are in "Local Mode" when running against server on https://localhost
- Made the default bucket policy to be "None", and added warning regarding potential fleet creation failure if user
  selects other bucket policy options, e.g. 3 days or 7 days
- Allow user to customize S3 bucket name during bootstrap bucket creation
- Other minor bug fixes and usability/string changes

# 0.1.0 (08/09/2021)

## Added:

- Plugin installation package and the plugin file structure
- The GameLift item in the Unity menu bar with the following sub-menu items:
    - Plugin Settings
    - Local Testing
    - Deployment
- The GameLift Plugin Settings window with the following configurations:
    - .NET
        - Ability to update the .NET version
    - JRE
        - Ability to download JRE
    - Local Testing
        - Ability to download GameLift Local
        - Ability to specify the path to the GameLiftLocal.jar file
    - AWS profile
        - Ability to add a new profile
        - Ability to choose an existing profile
        - Ability to specify a region
        - Ability to open the AWS instructions
    - AWS account bootstrapping
        - Ability to create a new S3 bucket
            - Ability to specify an expiration date of the S3 bucket
        - Ability to choose an existing S3 bucket
            - Ability to search by the S3 bucket name
        - Displaying the currently selected S3 bucket
        - Ability to open the S3 console
    - Displaying "Configured" and "Not Configured" statuses of the plugin settings
    - Automatically displaying the GameLift Plugin Settings window when the plugin is not fully configured
- The Local Testing window providing the following functionality:
    - Ability to specify the build path
    - Ability to specify the GL Local port
- 5 predefined deployment scenario templates:
    - Auth Only
    - Single-Region Fleet
    - Multi-Region Fleets with Queue and Custom Matchmaker
    - SPOT Fleets with Queue and Custom Matchmaker
    - FlexMatch
- The Deployment window providing the following functionality:
    - Ability to choose a predefined scenario for deployment
        - Displaying the scenario template description
        - Ability to open the AWS instructions
    - Ability to choose a custom scenario for deployment
    - Ability to specify a game name
    - Ability to specify a build path
    - Ability to start deployment
    - Ability to cancel current deployment
    - Displaying the stack deployment status and details
    - Displaying the deployment outcomes (Cognito Client ID, API Gateway Endpoint)
    - Ability to open the AWS CloudFormation console
- A game sample for testing the plugin
- A custom scenario sample
- Dark/light Unity theme support
