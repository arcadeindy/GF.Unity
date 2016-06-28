using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using GF.Unity.Common;

public class ClientSampleNetwork<TDef> : Component<TDef> where TDef : DefSampleNetwork, new()
{
    //-------------------------------------------------------------------------
    public override void init()
    {
        EbLog.Note("ClientSampleNetwork.init()");

        EntityMgr.getDefaultEventPublisher().addHandler(Entity);

        DefaultRpcSession.defRpcMethod<int, string>(1, _onRpcMethod1);

        connectBase("192.168.0.10", 5882);
    }

    //-------------------------------------------------------------------------
    public override void release()
    {
        disconnect();

        EbLog.Note("ClientSampleNetwork.release()");
    }

    //-------------------------------------------------------------------------
    public override void update(float elapsed_tm)
    {
    }

    //-------------------------------------------------------------------------
    public override void handleEvent(object sender, EntityEvent e)
    {
    }

    //-------------------------------------------------------------------------
    public void connectBase(string base_ip, int base_port)
    {
        DefaultRpcSession.close();

        DefaultRpcSession.OnSocketConnected = _onSocketConnected;
        DefaultRpcSession.OnSocketClosed = _onSocketClosed;
        DefaultRpcSession.OnSocketError = _onSocketError;
        DefaultRpcSession.connect(base_ip, base_port);
    }

    //-------------------------------------------------------------------------
    public void disconnect()
    {
        EbLog.Note("ClientSampleNetwork.disconnect()");

        DefaultRpcSession.close();
    }

    //-------------------------------------------------------------------------
    void _onSocketConnected(object client, EventArgs args)
    {
        EbLog.Note("ClientSampleNetwork._onSocketConnected()");

        DefaultRpcSession.rpc(2, 2, "Hello World!");
    }

    //-------------------------------------------------------------------------
    void _onSocketClosed(object client, EventArgs args)
    {
        EbLog.Note("ClientSampleNetwork._onSocketClosed()");
    }

    //-------------------------------------------------------------------------
    void _onSocketError(object rec, SocketErrorEventArgs args)
    {
        EbLog.Note("ClientSampleNetwork._onSocketError()");
    }

    void _onRpcMethod1(int v, string info)
    {
        EbLog.Note(v.ToString());
        EbLog.Note(info);
    }
}
