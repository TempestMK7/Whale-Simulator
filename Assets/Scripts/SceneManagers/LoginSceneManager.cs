using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginSceneManager : MonoBehaviour {

    public Canvas mainCanvas;
    public GameObject baseLoginPanel;
    public GameObject baseCreatePanel;

    public Text loginLabel;
    public InputField loginUsernameField;
    public InputField loginPasswordField;
    public GameObject loginCreatePanel;

    public InputField createUsernameField;
    public InputField createEmailField;
    public InputField createPasswordField;
    public InputField createConfirmField;

    public GameObject loadingPopup;
    public GameObject tooltipPopup;

    private bool loading = false;

    public void Awake() {
        baseLoginPanel.gameObject.SetActive(true);
        baseCreatePanel.gameObject.SetActive(false);

        loginPasswordField.inputType = InputField.InputType.Password;
        createPasswordField.inputType = InputField.InputType.Password;
        createConfirmField.inputType = InputField.InputType.Password;

        ResetLoginPanel();
    }

    private void ResetLoginPanel() {
/*        var credentialsManager = FindObjectOfType<CredentialsManager>();
        if (credentialsManager.UserIsAuthenticated()) {
            loginCreatePanel.SetActive(false);
            loginLabel.text = string.Format("Logged in as: {0}", credentialsManager.GetUsername());
        } else {
            loginCreatePanel.SetActive(true);
            loginLabel.text = "Not logged in.";
        }*/
        loginCreatePanel.SetActive(true);
        loginLabel.text = "Not logged in.";
    }

    public async void OnLoginPressed() {
        if (ButtonsBlocked()) return;

        string username = loginUsernameField.text;
        if (username.Length < 6) {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Invalid username.", "Username must be at least 6 characters long.");
            return;
        }
        var regexItem = new Regex("^\\w*$");
        if (!regexItem.IsMatch(username)) {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Invalid username.", "Username may only contain alphanumeric characters (letters and numbers).");
            return;
        }

        string password = loginPasswordField.text;
        if (password.Length < 8) {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Invalid password.", "Password must be at least 8 characters long.");
            return;
        }

        loading = true;
        var loadingPopupBehavior = Instantiate(loadingPopup, mainCanvas.transform).GetComponent<LoadingPopup>();
        loadingPopupBehavior.LaunchPopup("Logging in...", "Contacting identity server...");
        var credentialManager = FindObjectOfType<CredentialsManager>();
        bool successful = await credentialManager.LoginUser(username, password);
        loadingPopupBehavior.DismissPopup();

        if (successful) {
            await credentialManager.DownloadState();
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
        baseCreatePanel.gameObject.SetActive(true);
    }

    public void OnLoginBackPressed() {
        if (ButtonsBlocked()) return;
        loading = true;
        SceneManager.LoadSceneAsync("HubScene");
    }

    public async void OnCreateAccountPressed() {
        if (ButtonsBlocked()) return;
        string username = createUsernameField.text;
        string email = createEmailField.text;
        string password = createPasswordField.text;
        string confirmation = createConfirmField.text;

        // Validate username field.
        if (username.Length < 6) {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Invalid username.", "Username must be at least 6 characters long.");
            return;
        }
        var regexItem = new Regex("^\\w*$");
        if (!regexItem.IsMatch(username)) {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Invalid username.", "Username may only contain alphanumeric characters (letters and numbers).");
            return;
        }

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
        var credentialManager = FindObjectOfType<CredentialsManager>();
        bool successful = await credentialManager.CreateAccount(username, email, password);
        loadingPopupBehavior.DismissPopup();

        // Display results.
        if (successful) {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Success!", "Your account was created successfully.  Please check your email for a verification and then log in.");
            baseLoginPanel.gameObject.SetActive(true);
            baseCreatePanel.gameObject.SetActive(false);
        } else {
            var tooltip = Instantiate(tooltipPopup, mainCanvas.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Failed.", "We were unable to create your account.");
        }
        loading = false;
    }

    public void OnCreateBackPressed() {
        if (ButtonsBlocked()) return;
        baseLoginPanel.gameObject.SetActive(true);
        baseCreatePanel.gameObject.SetActive(false);
    }

    private bool ButtonsBlocked() {
        return loading || FindObjectOfType<TooltipPopup>() != null;
    }
}
