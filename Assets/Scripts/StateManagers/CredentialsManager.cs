using System;
using System.IO;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Newtonsoft.Json;
using UnityEngine;
using System.Text.RegularExpressions;

using Com.Tempest.Whale.StateObjects;
using System.Threading;

public class CredentialsManager : MonoBehaviour {

    private CognitoAWSCredentials credentials;
    private AmazonLambdaClient lambdaClient;

    public void Awake() {
        DontDestroyOnLoad(gameObject);
        if (credentials == null || lambdaClient == null) {
            credentials = new WhaleCredentials("us-west-2:8f446e6c-1559-49bc-be35-3f9462c5205c", RegionEndpoint.USWest2);
            credentials.GetIdentityId();
            credentials.GetCredentials();
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

    private T DeserializeObject<T>(string serverResponse) {
        var trimmedString = serverResponse.Substring(1, serverResponse.Length - 2);
        return JsonConvert.DeserializeObject<T>(Regex.Unescape(trimmedString));
    }
}