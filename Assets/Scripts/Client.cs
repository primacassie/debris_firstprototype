using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


class RegisterHostMessage : MessageBase { public float message; }

public class Client : MonoBehaviour {

    NetworkClient myclient;
    public UnityEngine.UI.Text conText;
    public static short clientID = 123;
    // Use this for initialization
    void Start()
    {
        SetUpclient();
    }

    // Update is called once per frame
    void SetUpclient()
    {
        myclient = new NetworkClient();
        myclient.RegisterHandler(MsgType.Connect, OnConnected);
        myclient.Connect("10.244.144.201", 4444);
    }
    public void OnConnected(NetworkMessage netMsg)
    {
        RegisterHostMessage msg = new RegisterHostMessage();
        msg.message = 12.45f;
        conText.text = "Connected to server";
        myclient.Send(clientID, msg);
    }
}
