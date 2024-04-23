using System;

namespace Com.Tempest.Whale.RequestObjects {

    public enum ErrorType {
        NONE, CREDENTIALS_INVALID, LOGIN_DESYNC
    }

    [Serializable]
    public class WhaleResponse<T> {

        public bool Successful { get; set; }
        public ErrorType? Error { get; set; }
        public T Response { get; set; }

        public WhaleResponse(bool successful, ErrorType? error, T response) {
            Successful = successful;
            Error = error;
            Response = response;
        }
    }
}
