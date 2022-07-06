using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;

/// <summary>
/// Used to communicate with the main unity thread from another thread.
/// <para>Mainly used for communicating from websocket thread to unity thread.</para>
/// </summary>
public class UnityThreadCommunicator : MonoBehaviour
{
    public static UnityThreadCommunicator Instance;

    public static readonly ConcurrentQueue<Action> RunOnMainThread = new ConcurrentQueue<Action>();
        
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if(!RunOnMainThread.IsEmpty)
        {
            while(RunOnMainThread.TryDequeue(out var action))
            {
                action?.Invoke();
            }
        }
    }
}