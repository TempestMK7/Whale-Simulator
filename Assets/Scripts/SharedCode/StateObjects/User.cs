using System;
using System.Diagnostics.CodeAnalysis;

namespace Com.Tempest.Whale.StateObjects {

    [Serializable]
    public class User { 

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool EmailVerified { get; set; }
        public int? EmailVerificationCode { get; set; }
        public int? RecoveryCode { get; set; }
        public long? RecoveryCodeIssueTimeStamp { get; set; }

        public User() {
            EmailVerified = false;
        }
    }
}
