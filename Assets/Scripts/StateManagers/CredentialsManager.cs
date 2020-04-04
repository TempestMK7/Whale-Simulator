using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.CognitoIdentity.Model;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Newtonsoft.Json;
using UnityEngine;
using System.Text.RegularExpressions;

using Com.Tempest.Whale.StateObjects;

public class CredentialsManager : MonoBehaviour {

    private const string identityPoolId = "us-west-2:8f446e6c-1559-49bc-be35-3f9462c5205c";
    private const string appClientId = "1h91d25ov2bdnh9co13hqqg6cc";
    private const string userPoolId = "us-west-2_z7fUu2iQv";

    private CognitoAWSCredentials credentials;
    private AmazonLambdaClient lambdaClient;
    private AmazonCognitoIdentityProviderClient providerClient;

    public void Awake() {
        DontDestroyOnLoad(gameObject);
        if (credentials == null || lambdaClient == null || providerClient == null) {
            credentials = new WhaleCredentials(identityPoolId, RegionEndpoint.USWest2);
            credentials.GetIdentityId();
            credentials.GetCredentials();

            providerClient = new AmazonCognitoIdentityProviderClient(credentials, RegionEndpoint.USWest2);

            lambdaClient = new AmazonLambdaClient(credentials, RegionEndpoint.USWest2);
            credentials.IdentityChangedEvent += delegate (object sender, CognitoAWSCredentials.IdentityChangedArgs args) {
                Debug.Log("Changed credentials to: " + args.NewIdentityId);
            };
        }
    }

    public async void DownloadStateFromServer(Action releaseAction) {
        var request = new InvokeRequest() {
            FunctionName = "DownloadStateFunction",
            Payload = "\"{\"Empty\":\"None\"}\"",
            InvocationType = InvocationType.RequestResponse,
        };
        var result = await lambdaClient.InvokeAsync(request);
        var responseReader = new StreamReader(result.Payload);
        var responseBody = await responseReader.ReadToEndAsync();
        responseReader.Dispose();
        var newState = DeserializeObject<AccountState>(responseBody);
        StateManager.OverrideState(newState);
        releaseAction.Invoke();
    }

    public async void UploadStateToServer() {
        var state = StateManager.GetCurrentState();
        var stateJson = JsonConvert.SerializeObject(state);
        // miguel bugfix
        stateJson = stateJson.Replace("\"", "\\\"");
        var stringifiedJson = string.Format("\"{0}\"", stateJson);
        var request = new InvokeRequest() {
            FunctionName = "UploadStateFunction",
            Payload = stringifiedJson,
            InvocationType = InvocationType.RequestResponse
        };
        var result = await lambdaClient.InvokeAsync(request);
        var responseReader = new StreamReader(result.Payload);
        responseReader.Dispose();
    }

    public async void CreateAccount(string email, string password) {
        var attributes = new List<AttributeType>() {
            new AttributeType() { Name = "email", Value = email },
            new AttributeType() { Name = "userGuid", Value = StateManager.GetCurrentState().Id }
        };

        var signupRequest = new SignUpRequest() {
            ClientId = appClientId,
            Username = email,
            Password = password,
            UserAttributes = attributes
        };

        try {
            var result = await providerClient.SignUpAsync(signupRequest);
            if (result.UserConfirmed) {
                LoginUser(email, password);
            }
        } catch (Exception e) {
            Debug.LogError(e);
        }
    }

    public async void LoginUser(string email, string password) {
        CognitoUserPool userPool = new CognitoUserPool(userPoolId, appClientId, providerClient);
        CognitoUser user = new CognitoUser(email, appClientId, userPool, providerClient);
        InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest() {
            Password = password
        };
        AuthFlowResponse authResponse = null;
        try {
            authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);
        } catch (Exception e) {
            Debug.LogError(e);
        }

        GetUserRequest getUserRequest = new GetUserRequest();
        getUserRequest.AccessToken = authResponse.AuthenticationResult.AccessToken;
        GetUserResponse getUserResponse = await providerClient.GetUserAsync(getUserRequest);
        string userGuid = getUserResponse.UserAttributes
            .Find((AttributeType attribute) => { return attribute.Name.Equals("userGuid"); })
            .Value;
        credentials = user.GetCognitoAWSCredentials(identityPoolId, RegionEndpoint.USWest2);
        lambdaClient = new AmazonLambdaClient(credentials, RegionEndpoint.USWest2);
    }

    private T DeserializeObject<T>(string serverResponse) {
        var trimmedString = serverResponse.Substring(1, serverResponse.Length - 2);
        return JsonConvert.DeserializeObject<T>(Regex.Unescape(trimmedString));
    }
}