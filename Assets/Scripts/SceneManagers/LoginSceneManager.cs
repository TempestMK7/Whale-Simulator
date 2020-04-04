using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginSceneManager : MonoBehaviour {

    public GameObject baseLoginPanel;
    public GameObject baseCreatePanel;

    public InputField loginEmailField;
    public InputField loginPasswordField;

    public InputField createEmailField;
    public InputField createPasswordField;
    public InputField createConfirmField;

    public void Awake() {
        baseLoginPanel.gameObject.SetActive(true);
        baseCreatePanel.gameObject.SetActive(false);

        loginPasswordField.inputType = InputField.InputType.Password;
        createPasswordField.inputType = InputField.InputType.Password;
        createConfirmField.inputType = InputField.InputType.Password;
    }

    public void OnLoginPressed() {
        string email = loginEmailField.text;
        string password = loginPasswordField.text;
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

    public void OnCreateAccountPressed() {
        string email = createEmailField.text;
        string password = createPasswordField.text;
        string confirmation = createConfirmField.text;

        if (email.Length == 0 || password.Length == 0) return;
        if (!password.Equals(confirmation)) return;

        FindObjectOfType<CredentialsManager>().CreateAccount(email, password);
    }

    public void OnCreateBackPressed() {
        baseLoginPanel.gameObject.SetActive(true);
        baseCreatePanel.gameObject.SetActive(false);
    }
}
