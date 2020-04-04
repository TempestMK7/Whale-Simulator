using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
    private const string appClientId = "78kml34kbghal04vem0mf3eff8";
    private const string userPoolId = "us-west-2_r9S6PYbdH";

    private string refreshTokenFile;

    private CognitoAWSCredentials cachedCredentials;
    private AmazonCognitoIdentityProviderClient cachedProvider;

    public async void Awake() {
        DontDestroyOnLoad(gameObject);
        refreshTokenFile = Application.persistentDataPath + "/refresh_token.txt";
        await GetCredentials();
    }

    private async Task<CognitoAWSCredentials> GetCredentials() {
        if (cachedCredentials != null) return cachedCredentials;
        if (File.Exists(refreshTokenFile)) {
            StreamReader reader = new StreamReader(refreshTokenFile);
            var username = reader.ReadLine();
            var idToken = reader.ReadLine();
            var accessToken = reader.ReadLine();
            var refreshToken = reader.ReadLine();
            var issuedTime = new DateTime(long.Parse(reader.ReadLine()));
            var expirationTime = new DateTime(long.Parse(reader.ReadLine()));
            reader.Close();

            var provider = new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials(), RegionEndpoint.USWest2);
            CognitoUserPool userPool = new CognitoUserPool(userPoolId, appClientId, provider);
            CognitoUser user = new CognitoUser(username, appClientId, userPool, provider);
            user.SessionTokens = new CognitoUserSession(idToken, accessToken, refreshToken, issuedTime, expirationTime);
            var request = new InitiateRefreshTokenAuthRequest() { AuthFlowType = AuthFlowType.REFRESH_TOKEN_AUTH };
            await user.StartWithRefreshTokenAuthAsync(request).ConfigureAwait(false);
            cachedCredentials = user.GetCognitoAWSCredentials(identityPoolId, RegionEndpoint.USWest2);
            return cachedCredentials;
        } else {
            cachedCredentials = new WhaleCredentials(identityPoolId, RegionEndpoint.USWest2);
            return cachedCredentials;
        }
    }

    private async Task<AmazonCognitoIdentityProviderClient> GetProvider() {
        if (cachedProvider != null) return cachedProvider;
        var credentials = await GetCredentials();
        cachedProvider = new AmazonCognitoIdentityProviderClient(credentials, RegionEndpoint.USWest2);
        return cachedProvider;
    }

    public async void DownloadStateFromServer(Action releaseAction) {
        var lambdaClient = new AmazonLambdaClient(await GetCredentials(), RegionEndpoint.USWest2);
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
        if (releaseAction != null) releaseAction.Invoke();
    }

    public async void UploadStateToServer() {
        var lambdaClient = new AmazonLambdaClient(await GetCredentials(), RegionEndpoint.USWest2);
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

    public async Task<bool> CreateAccount(string username, string email, string password) {
        var provider = await GetProvider();

        var attributes = new List<AttributeType>() {
            new AttributeType() { Name = "email", Value = email },
            new AttributeType() { Name = "custom:custom:accountGuid", Value = StateManager.GetCurrentState().Id }
        };

        var signupRequest = new SignUpRequest() {
            ClientId = appClientId,
            Username = username,
            Password = password,
            UserAttributes = attributes
        };

        try {
            var result = await provider.SignUpAsync(signupRequest);
            return true;
        } catch (Exception e) {
            Debug.LogError(e);
            return false;
        }
    }

    public async Task<bool> LoginUser(string username, string password) {
        var provider = await GetProvider();
        CognitoUserPool userPool = new CognitoUserPool(userPoolId, appClientId, provider);
        CognitoUser user = new CognitoUser(username, appClientId, userPool, provider);
        InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest() {
            Password = password
        };
        try {
            var authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);
            cachedCredentials = user.GetCognitoAWSCredentials(identityPoolId, RegionEndpoint.USWest2);

            StreamWriter writer = new StreamWriter(refreshTokenFile);
            writer.WriteLine(username);
            writer.WriteLine(user.SessionTokens.IdToken);
            writer.WriteLine(user.SessionTokens.AccessToken);
            writer.WriteLine(user.SessionTokens.RefreshToken);
            writer.WriteLine(user.SessionTokens.IssuedTime.Ticks);
            writer.WriteLine(user.SessionTokens.ExpirationTime.Ticks);
            writer.Close();

            provider = null;
            await GetProvider();
        } catch (Exception e) {
            Debug.LogError(e);
            return false;
        }
        return true;
    }

    private T DeserializeObject<T>(string serverResponse) {
        var trimmedString = serverResponse.Substring(1, serverResponse.Length - 2);
        return JsonConvert.DeserializeObject<T>(Regex.Unescape(trimmedString));
    }
}