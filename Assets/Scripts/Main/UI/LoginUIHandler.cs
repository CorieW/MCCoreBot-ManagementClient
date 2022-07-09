using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using Newtonsoft.Json;

[RequireComponent(typeof(UIDocument))]
public class LoginUIHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LoginController _controller;

    private TextField _emailField;
    private TextField _passwordField;
    private Label _errorLabel;
    private Button _submitBtn;

    private void Awake()
    {
        if (!_controller) Debug.LogError("No controller reference.");
    }

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        _emailField = root.Q<TextField>("emailInputField");
        _passwordField = root.Q<TextField>("passwordInputField");
        _errorLabel = root.Q<Label>("errorText");
        _submitBtn = root.Q<Button>("submitButton");

        // TODO: Change
        _emailField.SetValueWithoutNotify("watson.corie@gmail.com");
        _passwordField.SetValueWithoutNotify("Password123!");

        // Register submit btn click event
        _submitBtn.RegisterCallback<ClickEvent>(e => Login());
        _submitBtn.RegisterCallback<KeyDownEvent>(e => {
            if (e.keyCode == KeyCode.Return) Login();
        });
    }

    /// <summary>
    /// Passes the input values of the username and password to a login function in the AppController class.
    /// <para>Performed by UI button.</para>
    /// </summary>
    public void Login()
    {
        // Validation
        if (!EmailFieldLengthValidation() || !PasswordLengthValidation()) return;

        SetLoading(true);
        _errorLabel.visible = false;

        MakeLoginRequest();
    }

    public void SetError(string error)
    {
        _errorLabel.visible = true;
        _errorLabel.text = error;
    }

    private async void MakeLoginRequest()
    {
        try {
            HttpResponseMessage response = await _controller.Login(_emailField.text, _passwordField.text);

            string json = await response.Content.ReadAsStringAsync();
            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            // If the response hasn't got the status code OK (200), see what the server sent back.
            if (response.StatusCode != System.Net.HttpStatusCode.OK) SetError(values["message"]);
        } catch (TaskCanceledException)
        {
            SetError("Servers are busy, please try again later!");
        } catch (Exception)
        {
            SetError("Something went wrong, please try again later!");
        }

        SetLoading(false);
    }

    private bool EmailFieldLengthValidation()
    {
        if (_emailField.text.Length > 0) return true;
        
        SetError("Enter an email address.");
        return false;
    }

    private bool PasswordLengthValidation()
    {
        if (_passwordField.text.Length > 0) return true;
        
        SetError("Enter a password.");
        return false;
    }

    private void SetLoading(bool loading)
    {
        SetFormInteractability(!loading);
    }

    private void SetFormInteractability(bool interactability)
    {
        _emailField.SetEnabled(interactability);
        _passwordField.SetEnabled(interactability);
        _submitBtn.SetEnabled(interactability);
    }

    /// <summary>
    /// Redirects to web page to subscribe to services.
    /// <para>Performed by UI button.</para>
    /// </summary>
    public void OpenCreateAnAccount()
    {
        System.Diagnostics.Process.Start(URLHelper.GetAddress() + URLHelper.GetRouteStringOfRoute(WebsiteRouteEnum.Subscriptions));
    }
}
