using System.Collections.Generic;
using System.Threading;

public abstract class TickingThread
{
    public TickingThread()
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        ThreadFactory.Instance.Add(cts);
        Thread t = new Thread(Start);
        t.Start(cts.Token);
    }

    private void Start(object ctObj)
    {
        CancellationToken ct = (CancellationToken)ctObj;

        while (true)
        {
            if (ct.IsCancellationRequested) return;

            Tick();
        }
    }

    protected abstract void Tick();
}