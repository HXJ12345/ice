//
// Copyright (c) ZeroC, Inc. All rights reserved.
//

using Test;
using System.Threading;

public class TestIntf : ITestIntf
{
    private static void test(bool b)
    {
        if (!b)
        {
            throw new System.Exception();
        }
    }

    public void
    op(Ice.Current current) => test(Dispatcher.isDispatcherThread());

    public void
    opWithPayload(byte[] seq, Ice.Current current) => test(Dispatcher.isDispatcherThread());

    public void sleep(int to, Ice.Current current) => Thread.Sleep(to);

    public void
    shutdown(Ice.Current current)
    {
        test(Dispatcher.isDispatcherThread());
        current.Adapter.Communicator.Shutdown();
    }
}

public class TestController : ITestIntfController
{
    private static void test(bool b)
    {
        if (!b)
        {
            throw new System.Exception();
        }
    }

    public void
    holdAdapter(Ice.Current current)
    {
        test(Dispatcher.isDispatcherThread());
        _adapter.Hold();
    }

    public void
    resumeAdapter(Ice.Current current)
    {
        test(Dispatcher.isDispatcherThread());
        _adapter.Activate();
    }

    public TestController(Ice.ObjectAdapter adapter) => _adapter = adapter;

    private Ice.ObjectAdapter _adapter;
};
