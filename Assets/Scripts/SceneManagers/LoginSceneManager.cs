using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginSceneManager : MonoBehaviour {

    public GameObject baseLoginPanel;
    public GameObject baseCreatePanel;

    public InputField loginUsernameField;
    public InputField loginPasswordField;

    public InputField createUsernameField;
    public InputField createEmailField;
    public InputField createPasswordField;
    public InputField createConfirmField;

    public GameObject tooltipPopup;

    public void Awake() {
        baseLoginPanel.gameObject.SetActive(true);
        baseCreatePanel.gameObject.SetActive(false);

        loginPasswordField.inputType = InputField.InputType.Password;
        createPasswordField.inputType = InputField.InputType.Password;
        createConfirmField.inputType = InputField.InputType.Password;
    }

    public async void OnLoginPressed() {
        string username = loginUsernameField.text;
        string password = loginPasswordField.text;
        if (username.Length == 0 || password.Length == 0) return;

        var credentialManager = FindObjectOfType<CredentialsManager>();
        bool successful = await credentialManager.LoginUser(username, password);

        if (successful) {
            var tooltip = Instantiate(tooltipPopup, baseLoginPanel.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Success!", "Logged in successfully.");
            credentialManager.DownloadStateFromServer(null);
        } else {
            var tooltip = Instantiate(tooltipPopup, baseLoginPanel.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Failed.", "Unable to log in.");
        }
    }

    public void OnForgotPasswordPressed() {

    }

    public void OnLoginCreatePressed() {
        baseLoginPanel.gameObject.SetActive(false);
        baseCreatePanel.gameObject.SetActive(true);
    }

    public void OnLoginBackPressed() {
        SceneManager.LoadSceneAsync("HubScene");
    }

    public async void OnCreateAccountPressed() {
        string username = createUsernameField.text;
        string email = createEmailField.text;
        string password = createPasswordField.text;
        string confirmation = createConfirmField.text;

        if (username.Length == 0 || email.Length == 0 || password.Length == 0) return;
        if (!password.Equals(confirmation)) return;

        var credentialManager = FindObjectOfType<CredentialsManager>();
        bool successful = await credentialManager.CreateAccount(username, email, password);

        if (successful) {
            var tooltip = Instantiate(tooltipPopup, baseCreatePanel.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Success!", "Your account was created successfully.  Please check your email for a verification and then log in.");
        } else {
            var tooltip = Instantiate(tooltipPopup, baseCreatePanel.transform).GetComponent<TooltipPopup>();
            tooltip.SetTooltip("Failed.", "We were unable to create your account.");
        }
    }

    public void OnCreateBackPressed() {
        baseLoginPanel.gameObject.SetActive(true);
        baseCreatePanel.gameObject.SetActive(false);
    }
}
