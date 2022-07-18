using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class NetManager : ManagerBase
{
    public static NetManager Instance = null;

    private void Awake()
    {
        Instance = this;
        Add(0,this);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case 0:
                _client.Send((SocketMsg)message);
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        Connected();
    }

    private ClientPeer _client = new ClientPeer("127.0.0.1", 6666);

    public void Connected()
    {
        _client.Connect();
    }

    private void Update()
    {
        if (_client == null)
            return;

        while (_client.SocketMsgQueue.Count > 0)
        {
            SocketMsg msg = _client.SocketMsgQueue.Dequeue();
            //TODO 操作这个msg
        }
    }
}

