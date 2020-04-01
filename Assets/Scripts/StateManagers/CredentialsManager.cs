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

public class CredentialsManager : MonoBehaviour {

    private CognitoAWSCredentials credentials;
    private AmazonLambdaClient lambdaClient;

    public void Awake() {
        DontDestroyOnLoad(gameObject);
        UnityInitializer.AttachToGameObject(gameObject);
        if (credentials == null || lambdaClient == null) {
            credentials = new CognitoAWSCredentials("us-west-2:8f446e6c-1559-49bc-be35-3f9462c5205c", RegionEndpoint.USWest2);
            lambdaClient = new AmazonLambdaClient(credentials, RegionEndpoint.USWest2);
            credentials.IdentityChangedEvent += delegate (object sender, CognitoAWSCredentials.IdentityChangedArgs args) {
                Debug.Log("Changed credentials to: " + args.NewIdentityId);
            };
        }
    }

    public void DownloadStateFromServer(Action releaseAction) {
        var request = new InvokeRequest() {
            FunctionName = "DownloadStateFunction",
            Payload = "\"{\"Empty\":\"None\"}\"",
            InvocationType = InvocationType.RequestResponse
        };
        lambdaClient.InvokeAsync(request, (result) => {
            if (result.Exception == null) {
                var responseReader = new StreamReader(result.Response.Payload);
                var newState = DeserializeObject<AccountState>(responseReader.ReadLine());
                responseReader.Dispose();

                StateManager.OverrideState(newState);
                releaseAction.Invoke();
            } else {
                Debug.LogError(result.Exception);
            }
        });
    }

    public void UploadStateToServer() {
        var state = StateManager.GetCurrentState();
        var stateJson = JsonConvert.SerializeObject(state);
        stateJson = stateJson.Replace("\"", "\\\"");
        var stringifiedJson = string.Format("\"{0}\"", stateJson);
        var request = new InvokeRequest() {
            FunctionName = "UploadStateFunction",
            Payload = stringifiedJson,
            InvocationType = InvocationType.RequestResponse
        };
        lambdaClient.InvokeAsync(request, (result) => {
            if (result.Exception == null) {
                var responseReader = new StreamReader(result.Response.Payload);
                Debug.Log(responseReader.ReadToEnd());
                responseReader.Dispose();
            } else {
                Debug.LogError(result.Exception);
            }
        });
    }

    private T DeserializeObject<T>(string serverResponse) {
        var trimmedString = serverResponse.Substring(1, serverResponse.Length - 2);
        return JsonConvert.DeserializeObject<T>(Regex.Unescape(trimmedString));
    }
}