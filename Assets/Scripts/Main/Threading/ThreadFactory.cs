using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>Handles all of the threads that the application creates.</summary>
public class ThreadFactory : MonoBehaviour
{
    public static ThreadFactory Instance;

    public List<CancellationTokenSource> _ctsList = new List<CancellationTokenSource>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else Instance = this;
    }

    private void OnDestroy()
    {
        CancelAll();
    }

    public void Add(CancellationTokenSource cts)
    {
        _ctsList.Add(cts);
    }

    public void Cancel(CancellationToken ct)
    {
        for (int i = 0; i < _ctsList.Count; i++)
        {
            if (_ctsList[i].Token == ct) 
            {
                _ctsList[i].Cancel();
                _ctsList.RemoveAt(i);
            }
        }
    }

    public void CancelAll()
    {
        for (int i = 0; i < _ctsList.Count; i++)
        {
            _ctsList[i].Cancel();
            _ctsList.RemoveAt(i);
        }
    }
}