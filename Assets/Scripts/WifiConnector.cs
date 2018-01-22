using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;

public class WifiConnector : MonoBehaviour {

    // Use this for initialization

    int myChannelId;
    int socketId;
    int socketPort = 7777;
    int connectionId;
    HostTopology topology;
    float distance, threshold, angle;

	void Start ()
    {
        distance = 0f;
        angle = 0f;
        NetworkTransport.Init();

        ConnectionConfig config = new ConnectionConfig();
        myChannelId = config.AddChannel(QosType.Reliable);

        int maxConnections = 10;
        topology = new HostTopology(config, maxConnections);
        socketId = NetworkTransport.AddHost(topology, socketPort);
        Debug.Log("Socket Open. SocketId is: " + socketId);
    }

    bool hostStarted, hostConnected;
    bool isClient = false;

    string buttonHostText = "Start Socket";
    string buttonClientText = "Connect To Host";
    public string hostIPText;

    private bool isValidIP(string ipText)
    {
        string[] strArray;
        strArray = ipText.Split('.');
        
        if (strArray.Length != 4)
            return false;

        for (int i = 0; i < strArray.Length; i++)
        {
            if (strArray[i].Length <= 3)
            {
                foreach (char c in strArray[i])
                {
                    if (c < '0' || c > '9')
                        return false;
                }
            }
            else
                return false;
        }
        return true;
    }

    public float getDistance()
    {
        return this.distance;
    }

    public float getAngle()
    {
        return this.angle;
    }

    public float getThreshold()
    {
        return this.threshold;
    }

    int type = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        receiveMessage();
    }

    void receiveMessage()
    {
        int recHostId;
        int recConnectionId;
        int recChannelId;
        byte[] recBuffer = new byte[1024];
        int bufferSize = 1024;
        int dataSize;
        byte error;
        NetworkEventType recNetworkEvent = NetworkTransport.Receive(out recHostId, out recConnectionId, out recChannelId, recBuffer, bufferSize, out dataSize, out error);

        switch (recNetworkEvent)
        {
            case NetworkEventType.Nothing:
                break;
            case NetworkEventType.ConnectEvent:
                Debug.Log("incoming connection event received");
                break;
            case NetworkEventType.DataEvent:
                Stream stream = new MemoryStream(recBuffer);
                BinaryFormatter formatter = new BinaryFormatter();
                string message = formatter.Deserialize(stream) as string;
                //Debug.Log("incoming message event received: " + message);
                string value = message.Split(',')[0];
                string mode = message.Split(',')[1];
                type = int.Parse(mode);
                if (type == 1)
                    threshold = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                else if (type == 2)
                    distance = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                else if (type == 3)
                    angle = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                break;
            case NetworkEventType.DisconnectEvent:
                Debug.Log("remote client event disconnected");
                break;
        }
    }

    public void Connect()
    {
        byte error;
        connectionId = NetworkTransport.Connect(socketId, "192.168.1.167", socketPort, 0, out error);
        Debug.Log("Connected to server. ConnectionId: " + connectionId);
    }
}
