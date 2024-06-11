using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginSceneManager : MonoBehaviour {

    public Canvas mainCanvas;
    public GameObject baseLoginPanel;
    public GameObject baseCreatePanel;
    public GameObject baseConfirmEmailPanel;

    public Text loginLabel;
    public InputField loginUsernameField;
    public InputField loginPasswordField;
    public GameObject loginCreatePanel;
    public Text createLoginButtonText;

    public InputField createEmailField;
    public InputField createPasswordField;
    public InputField createConfirmField;

    public InputField confirmEmailCodeField;

    public GameObject loadingPopup;
    public GameObject tooltipPopup;

    private bool loading = false;
    private StateManager stateManager;
    private CredentialsManager credentialsManager;

    public void Awake() {
        stateManager = FindObjectOfType<StateManager>();
        credentialsManager = FindObjectOfType<CredentialsManager>();

        baseLoginPanel.gameObject.SetActive(true);
        baseCreatePanel.gameObject.SetActive(false);
        baseConfirmEmailPanel.gameObject.SetActive(false);

        loginPasswordField.inputType = InputField.InputType.Password;
        createPasswordField.inputType = InputField.InputType.Password;
        createConfirmField.inputType = InputField.InputType.Password;

        ResetLoginPanel();
    }

    private void ResetLoginPanel() {
        loginCreatePanel.SetActive(!stateManager.CurrentUserState.EmailVerified);
        var text = "Logged in.";
        if (!stateManager.CurrentUserState.AccountCreated) {
            text = "Not logged in.";
        } else if (!stateManager.CurrentUserState.EmailVerified) {
            text = "Need to verify email.";
        }
        loginLabel.text = text;

        createLoginButtonText.text = stateManager.CurrentUserState.AccountCreated && !stateManager.CurrentUserState.EmailVerified ? "Confirm Email" : "Create Account";
    }

    public async void OnLoginPressed() {
        if (ButtonsBlocked()) return;

        // Validate email field.
        string email = loginUsernameField.text;
        if (email.Length < 3) {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Invalid email.", "Email address is invalid.");
            return;
        }
        try {
            var validatedAddress = new System.Net.Mail.MailAddress(email);
            if (!validatedAddress.Equals(email)) {
                var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
                tooltip.SetTooltip("Invalid email.", "Email address is invalid.");
                return;
            }
        } catch {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Invalid email.", "Email address is invalid.");
            return;
        }

        // Validate password field.
        string password = loginPasswordField.text;
        if (password.Length < 8) {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Invalid password.", "Password must be at least 8 characters long.");
            return;
        }

        loading = true;
        var loadingPopupBehavior = Instantiate(loadingPopup, mainCanvas.transform).GetComponent<LoadingPopup>();
        loadingPopupBehavior.LaunchPopup("Logging in...", "Contacting identity server...");
        bool successful = await credentialsManager.LoginUser(email, password);
        loadingPopupBehavior.DismissPopup();

        if (successful) {
            await credentialsManager.DownloadUserInfo();
            await credentialsManager.DownloadState();
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Success!", "Logged in successfully.");
        } else {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Failed.", "Unable to log in.");
        }
        ResetLoginPanel();
        loading = false;
    }

    public void OnForgotPasswordPressed() {
        if (ButtonsBlocked()) return;
    }

    public void OnLoginCreatePressed() {
        if (ButtonsBlocked()) return;
        baseLoginPanel.gameObject.SetActive(false);
        if (stateManager.CurrentUserState.AccountCreated) {
            baseCreatePanel.gameObject.SetActive(false);
            baseConfirmEmailPanel.gameObject.SetActive(true);
        } else {
            baseCreatePanel.gameObject.SetActive(true);
            baseConfirmEmailPanel.gameObject.SetActive(false);
        }
    }

    public void OnLoginBackPressed() {
        if (ButtonsBlocked()) return;
        loading = true;
        SceneManager.LoadSceneAsync("HubScene");
    }

    public async void OnCreateAccountPressed() {
        if (ButtonsBlocked()) return;
        string email = createEmailField.text;
        string password = createPasswordField.text;
        string confirmation = createConfirmField.text;

        // Validate email field.
        if (email.Length < 3) {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Invalid email.", "Email address is invalid.");
            return;
        }
        try {
            var validatedAddress = new System.Net.Mail.MailAddress(email);
            if (!validatedAddress.Equals(email)) {
                var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
                tooltip.SetTooltip("Invalid email.", "Email address is invalid.");
                return;
            }
        } catch {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Invalid email.", "Email address is invalid.");
            return;
        }

        // Validate password field.
        if (password.Length < 8) {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Invalid password.", "Password must be at least 8 characters long.");
            return;
        }
        bool validPassword = password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit);
        if (!validPassword) {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Invalid password.", "Password must contain at least one uppercase character, lowercase character, and number.  Special characters are allowed but not required.");
            return;
        }

        // Validate confirmation field.
        if (!password.Equals(confirmation)) {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Invalid confirmation.", "Password does not match confirmation.");
            return;
        }

        // Actually create the account.
        loading = true;
        var loadingPopupBehavior = Instantiate(loadingPopup, mainCanvas.transform).GetComponent<LoadingPopup>();
        loadingPopupBehavior.SetText("Creating account...", "Contacting identity server...");
        bool successful = await credentialsManager.CreateAccount(email, password);
        loadingPopupBehavior.DismissPopup();

        // Display results.
        if (successful) {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Success!", "Your account was created successfully.  Please check your email for a verification code.");
            baseLoginPanel.gameObject.SetActive(false);
            baseCreatePanel.gameObject.SetActive(false);
            baseConfirmEmailPanel.gameObject.SetActive(true);
        } else {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Failed.", "We were unable to create your account.");
        }
        loading = false;
        ResetLoginPanel();
    }

    public void OnCreateBackPressed() {
        if (ButtonsBlocked()) return;
        baseLoginPanel.gameObject.SetActive(true);
        baseCreatePanel.gameObject.SetActive(false);
        baseConfirmEmailPanel.gameObject.SetActive(false);
    }

    public async void OnConfirmEmailPressed() {
        int confirmationCode = int.Parse(confirmEmailCodeField.text);
        if (confirmationCode < 100000 || confirmationCode > 999999) {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Invalid code.", "Code should be a six digit number.");
            return;
        }

        loading = true;
        var loadingPopupBehavior = Instantiate(loadingPopup, mainCanvas.transform).GetComponent<LoadingPopup>();
        loadingPopupBehavior.SetText("Confirming Email...", "Contacting identity server...");
        bool successful = await credentialsManager.ConfirmEmail(confirmationCode);
        loadingPopupBehavior.DismissPopup();

        // Display results.
        if (successful) {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Success!", "Your email was verified.");
            baseLoginPanel.gameObject.SetActive(true);
            baseCreatePanel.gameObject.SetActive(false);
            baseConfirmEmailPanel.gameObject.SetActive(false);
        } else {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Failed.", "We were unable to create your account.");
        }
        loading = false;
        ResetLoginPanel();
    }

    public void OnConfirmBackPressed() {
        if (ButtonsBlocked()) return;
        baseLoginPanel.gameObject.SetActive(true);
        baseCreatePanel.gameObject.SetActive(false);
        baseConfirmEmailPanel.gameObject.SetActive(false);
    }

    private bool ButtonsBlocked() {
        return loading || FindObjectOfType<TooltipPopup>() != null;
    }
}
