using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginUIHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputField usernameField;
    [SerializeField] private InputField passwordField;

    /// <summary>
    /// Passes the input values of the username and password to a login function in the AppController class.
    /// <para>Performed by UI button.</para>
    /// </summary>
    public void Login()
    {
        AppController.Instance.Login(usernameField.text, passwordField.text);
    }

    /// <summary>
    /// Redirects to web page to subscribe to services.
    /// <para>Performed by UI button.</para>
    /// </summary>
    public void CreateAnAccount()
    {
        // TODO: Change url when I have a domain
        System.Diagnostics.Process.Start(GlobalValues.GetAddress() + "/subscriptions");
    }
}
