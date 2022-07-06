using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class LoginUIHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LoginController _controller;
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private InputField _emailField;
    [SerializeField] private InputField _passwordField;
    [SerializeField] private Text _errorField;
    [SerializeField] private Button _submitBtn;

    /// <summary>
    /// Passes the input values of the username and password to a login function in the AppController class.
    /// <para>Performed by UI button.</para>
    /// </summary>
    public void Login()
    {
        // Validation
        if (!EmailFieldLengthValidation() || !PasswordLengthValidation()) return;

        SetLoading(true);

        MakeLoginRequest();
    }

    public void SetError(string error)
    {
        _errorField.text = error;
    }

    private async void MakeLoginRequest()
    {
        try {
            HttpResponseMessage response = await _controller.Login(_emailField.text, _passwordField.text);

            string json = await response.Content.ReadAsStringAsync();
            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            
            _errorField.text = values["message"];
        } catch (TaskCanceledException) {
            _errorField.text = "Servers are busy, please try again later!";
        } catch (Exception)
        {
            _errorField.text = "Something went wrong, please try again later!";
        }

        SetLoading(false);
    }

    private bool EmailFieldLengthValidation()
    {
        if (_emailField.text.Length > 0) return true;
        
        _errorField.text = "Enter an email address.";
        return false;
    }

    private bool PasswordLengthValidation()
    {
        if (_passwordField.text.Length > 0) return true;
        
        _errorField.text = "Enter a password.";
        return false;
    }

    private void SetLoading(bool loading)
    {
        _loadingScreen.SetLoadingScreenVisibility(loading);
        SetFormInteractability(!loading);
    }

    private void SetFormInteractability(bool interactability)
    {
        _emailField.interactable = interactability;
        _passwordField.interactable = interactability;
        _submitBtn.interactable = interactability;
    }

    /// <summary>
    /// Redirects to web page to subscribe to services.
    /// <para>Performed by UI button.</para>
    /// </summary>
    public void CreateAnAccount()
    {
        System.Diagnostics.Process.Start(URLHelper.GetAddress() + URLHelper.GetRouteStringOfRoute(WebsiteRouteEnum.Subscriptions));
    }
}
