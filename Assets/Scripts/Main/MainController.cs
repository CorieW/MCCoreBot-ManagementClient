using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;
using WebSocketSharp.Net;

public class MainController : MonoBehaviour
{
    public const string SCENE_NAME = "Main";

    [SerializeField] private MainUIHandler _mainUIHandler;
    [SerializeField] private WebSocketHandler _webSocketHandler;

    public static void LoadPage()
    {
        SceneManager.LoadScene(SCENE_NAME);
    }
}
