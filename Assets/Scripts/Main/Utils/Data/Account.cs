using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using UnityEngine;

public class Account
{
    public string SessionToken { get; private set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public bool Subscribed { get; set; }

    public Account(string sessionToken)
    {
        this.SessionToken = sessionToken;
    }
}