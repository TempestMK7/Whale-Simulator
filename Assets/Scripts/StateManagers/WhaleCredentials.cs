using System;
using System.IO;

using Amazon;
using Amazon.CognitoIdentity;
using Amazon.Runtime;

using UnityEngine;

class WhaleCredentials : CognitoAWSCredentials {

    public static readonly string identityFile = Application.persistentDataPath + "/whale_identity.txt";
    public static readonly string credentialsFile = Application.persistentDataPath + "/whale_credentials.txt";

    public static void CacheCredentials(ImmutableCredentials credentials) {
        StreamWriter writer = new StreamWriter(credentialsFile, false);
        writer.WriteLine(credentials.AccessKey);
        writer.WriteLine(credentials.SecretKey);
        writer.WriteLine(credentials.Token);
        writer.WriteLine(new DateTime().AddMinutes(30).Ticks);
        writer.Close();
    }

    public static void CacheIdentity(string identityId) {
        StreamWriter writer = new StreamWriter(identityFile, false);
        writer.WriteLine(identityId);
        writer.Close();
    }

    public WhaleCredentials(string identityPoolId, RegionEndpoint region) : base(identityPoolId, region) {
        
    }

    public override void CacheIdentityId(string identityId) {
        base.CacheIdentityId(identityId);
        StreamWriter writer = new StreamWriter(identityFile, false);
        writer.WriteLine(identityId);
        writer.Close();
    }

    public override string GetCachedIdentityId() {
        base.GetCachedIdentityId();
        if (!File.Exists(identityFile)) return string.Empty;
        StreamReader reader = new StreamReader(identityFile);
        var identityId = reader.ReadLine();
        reader.Close();
        return identityId;
    }

    public override void ClearIdentityCache() {
        base.ClearIdentityCache();
        File.Delete(identityFile);
    }

    public override void CacheCredentials(CredentialsRefreshState credentialsState) {
        base.CacheCredentials(credentialsState);
        StreamWriter writer = new StreamWriter(credentialsFile, false);
        writer.WriteLine(credentialsState.Credentials.AccessKey);
        writer.WriteLine(credentialsState.Credentials.SecretKey);
        writer.WriteLine(credentialsState.Credentials.Token);
        writer.WriteLine(credentialsState.Expiration.Ticks);
        writer.Close();
    }

    public override CredentialsRefreshState GetCachedCredentials() {
        base.GetCachedCredentials();
        if (!File.Exists(credentialsFile)) {
            return null;
        }
        StreamReader reader = new StreamReader(credentialsFile);
        var credentials = new ImmutableCredentials(reader.ReadLine(), reader.ReadLine(), reader.ReadLine());
        var expiration = new DateTime(long.Parse(reader.ReadLine()));
        reader.Close();
        return new CredentialsRefreshState(credentials, expiration);
    }

    public override void ClearCredentials() {
        base.ClearCredentials();
        File.Delete(credentialsFile);
    }
}
