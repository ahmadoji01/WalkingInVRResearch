using UnityEngine;
using System;
using System.Collections;
using System.Threading;
using System.Net;
using System.Net.Sockets;

public class HT_FlockOfBird : MonoBehaviour {

    private Transform _eyes;

    private bool _run;
    private TcpClient _client;

    private byte[] _recvbuf = new byte[1024];

    //FOBセンサとメガネの位置関係補正
    //_glassPos * _glassRot * Vtxの順で影響する
    //UnityのQuaternionは、Q1*Q2*Vtxの順に積算される
    private Vector3 _glassPos = new Vector3(-0.07f, 0.0f, 0.0f);
    private Quaternion _glassRot =
        Quaternion.AngleAxis(90.0f, new Vector3(0.0f, 0.0f, 1.0f)) *
        Quaternion.AngleAxis(90.0f, new Vector3(1.0f, 0.0f, 0.0f));


	// Use this for initialization
	void Start ()
    {
        _eyes = transform.FindChild("Eyes");

        _client = null;

        _run = true;
        Thread thread = new Thread(new ThreadStart(this.Connect));
        thread.IsBackground = true;
        thread.Start();

	}
	
	// Update is called once per frame
	void Update ()
    {
       
        if (null == _client)
        {
            return;
        }

        try
        {
            NetworkStream ns = _client.GetStream();

            //空送り
            if (ns.DataAvailable)
            {
                ns.Read(_recvbuf, 0, _recvbuf.Length);
            }

            //送信要求
            ns.WriteByte((byte)'\n');

            //データ受信
            int readCount = ns.Read(_recvbuf, 0, 29);


            if (readCount < 29)
            {
                return;
            }

            float x = BitConverter.ToSingle(_recvbuf, 1);
            float y = BitConverter.ToSingle(_recvbuf, 5);
            float z = BitConverter.ToSingle(_recvbuf, 9);
            float qx = BitConverter.ToSingle(_recvbuf, 13);
            float qy = BitConverter.ToSingle(_recvbuf, 17);
            float qz = BitConverter.ToSingle(_recvbuf, 21);
            float qw = BitConverter.ToSingle(_recvbuf, 25);

            _eyes.localRotation = new Quaternion(qx, qy, qz, qw) * _glassRot;

            Matrix4x4 m = Matrix4x4.TRS(new Vector3(0.0f, 0.0f, 0.0f), _eyes.localRotation, new Vector3(1.0f, 1.0f, 1.0f));
            _eyes.localPosition = new Vector3(x, y, z) + m.MultiplyVector(_glassPos);
        }
        catch (Exception)
        {
            _client = null;

            _run = true;
            Thread thread = new Thread(new ThreadStart(this.Connect));
            thread.IsBackground = true;
            thread.Start();
        }
    }

    void OnApplicationQuit()
    {
        _run = false;
    }

    void Connect()
    {
        while (_run)
        {
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(IPAddress.Loopback, 8876);
                if (client.Connected)
                {
                    _client = client;
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }

            Thread.Sleep(1000);
        }

    }




}
