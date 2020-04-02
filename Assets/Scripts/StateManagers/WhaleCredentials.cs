using System.IO;

using Amazon;
using Amazon.CognitoIdentity;

using UnityEngine;

class WhaleCredentials : CognitoAWSCredentials {

    private readonly string credentialsFile = Application.persistentDataPath + "/whale_credentials.txt";

    public WhaleCredentials(string identityPoolId, RegionEndpoint region) : base(identityPoolId, region) {
        
    }

    public override void CacheIdentityId(string identityId) {
        base.CacheIdentityId(identityId);
        StreamWriter writer = new StreamWriter(credentialsFile, false);
        writer.WriteLine(identityId);
        writer.Close();
    }

    public override string GetCachedIdentityId() {
        base.GetCachedIdentityId();
        if (!File.Exists(credentialsFile)) return string.Empty;
        StreamReader reader = new StreamReader(credentialsFile);
        var identityId = reader.ReadLine();
        reader.Close();
        return identityId;
    }

    public override void ClearIdentityCache() {
        File.Delete(credentialsFile);
    }
}
