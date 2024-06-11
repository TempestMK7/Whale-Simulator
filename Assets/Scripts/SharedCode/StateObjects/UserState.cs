namespace Com.Tempest.Whale.StateObjects {

    public class UserState {

        public bool AccountCreated { get; set; }
        public bool EmailVerified { get; set; }

        public UserState() { 
            AccountCreated = false;
            EmailVerified = false;
        }
    }
}
