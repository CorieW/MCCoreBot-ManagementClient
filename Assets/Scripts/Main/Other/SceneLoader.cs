using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    private IEnumerator RunAfterLoadMethod(Action afterLoadMethod)
    {
        yield return new WaitForFixedUpdate();
        afterLoadMethod();
    }

    public static void LoadScene(string sceneName, Action afterLoadMethod)
    {
        SceneManager.LoadScene(sceneName);
        Instance.StartCoroutine(Instance.RunAfterLoadMethod(afterLoadMethod));
    }
}
