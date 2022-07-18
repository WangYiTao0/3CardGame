using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

/// <summary>
/// 客户端socket的封装
/// </summary>
public class ClientPeer
{
    private Socket _socket;
    private string _ip;
    private int _port;

    /// <summary>
    /// 构造连接对象
    /// </summary>
    /// <param name="ip">IP地址</param>
    /// <param name="port">端口号</param>
    public ClientPeer(string ip, int port)
    {
        try
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _ip = ip;
            _port = port;

        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void Connect()
    {
        try
        {
            _socket.Connect(_ip, _port);
            Debug.Log("连接服务器成功！");

            StartReceive();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            throw;
        }

    }
    
    #region 接受数据

    //接受的数据缓冲区
    private byte[] _receiveBuffer = new byte[1024];

    /// <summary>
    /// 一旦接收到数据 就存到缓存区里面
    /// </summary>
    private List<byte> _dataCache = new List<byte>();

    private bool _isProcessReceive = false;

    public Queue<SocketMsg> SocketMsgQueue= new Queue<SocketMsg>();

    /// <summary>
    /// 开始异步接受数据
    /// </summary>
    private void StartReceive()
    {
        if (_socket == null && _socket.Connected)
        {
            Debug.LogError("没有连接成功，无法发送数据");
            return;
        }
        //收到消息后调用回调
        _socket.BeginReceive(_receiveBuffer, 0, 1024, SocketFlags.None, ReceiveCallBack, _socket);
    }

    /// <summary>
    /// 收到消息的回调
    /// </summary>
    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            int length = _socket.EndReceive(ar);
            byte[] tmpByteArray = new byte[length];
            Buffer.BlockCopy(_receiveBuffer, 0, tmpByteArray, 0, length);

            //处理收到的数据
            _dataCache.AddRange(tmpByteArray);
            if (_isProcessReceive == false)
                ProcessReceive();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    /// <summary>
    /// 处理收到的数据
    /// </summary>
    private void ProcessReceive()
    {
        _isProcessReceive = true;
        //解析数据包
        byte[] data = EncodeTool.DecodePacket(ref _dataCache);

        if (data == null)
        {
            _isProcessReceive = false;
            return;
        }

        SocketMsg msg = EncodeTool.DecodeMsg(data);
        //存储消息 等待处理
        SocketMsgQueue.Enqueue(msg);

        //尾递归
        ProcessReceive();
    }

    #endregion

    #region 发送数据

    public void Send(int opCode, int subCode, object value)
    {
        SocketMsg msg = new SocketMsg(opCode, subCode, value);
        Send(msg);
    }

    public void Send(SocketMsg msg)
    {
        byte[] data = EncodeTool.EncodeMsg(msg);
        byte[] packet = EncodeTool.EncodePacket(data);

        try
        {
            _socket.Send(packet);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    #endregion

}
