using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour
{
    public const string SCENE_NAME = "Login";

    [SerializeField] private string _successfulLoginSceneName;

    /// <summary>
    /// Sends a login request to the RESTful API.
    /// <para>If successful, a session token will be returned.</para>
    /// </summary>
    public async Task<HttpResponseMessage> Login(string email, string password)
    {
        // Add a cookies container, so the backend can respond with session token in the cookies.
        CookieContainer cookies = new CookieContainer();
        Uri uri = new Uri(URLHelper.GetAPIAddress() + "/login");

        using (HttpClientHandler handler = new HttpClientHandler())
        {
            handler.CookieContainer = cookies;

            // Create the HTTP client
            using (HttpClient client = new HttpClient(handler))
            {
                client.Timeout = TimeSpan.FromSeconds(10);
                // Set the content to send
                var values = new Dictionary<string, string>
                {
                    { "email", email },
                    { "password", password }
                };
                HttpContent content = new FormUrlEncodedContent(values);

                // Get the response
                HttpResponseMessage response = await client.PostAsync(uri, content);

                IEnumerable<Cookie> responseCookies = cookies.GetCookies(uri).Cast<Cookie>();

                string sessionToken = "";
                foreach (Cookie cookie in responseCookies)
                {
                    if (cookie.Name == "sessionToken") sessionToken = cookie.Value;
                }

                if (sessionToken == "") return response;

                AppController.Instance.Account = new Account(sessionToken);
                MainController.LoadPage();

                return response;
            }
        }
    }

    public static void Load(string errorMessage)
    {
        SceneLoader.LoadScene(SCENE_NAME, () => {
            FindObjectOfType<LoginUIHandler>().SetError(errorMessage);
        });
    }
}
