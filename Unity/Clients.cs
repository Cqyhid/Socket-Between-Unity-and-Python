using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Clients : MonoBehaviour
{
    private int port = 10086;
    private string host = "127.0.0.1";
    private bool changeTexture = false;
    public RawImage displayTexture;
    private Socket client;
    private byte[] messTmp;
    private byte[] tempTmp;

    // Use this for initialization
    void Start()
    {
        // initialize a socket
        client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            client.Connect(new IPEndPoint(IPAddress.Parse(host), port));
            Thread th = new Thread(new ThreadStart(GetMessage));
            th.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }
        //client.Close();
    }
    private void Update()
    {
        // texture change can not be done in thread.
        if (changeTexture)
        {
            displayTexture.texture = bytesToTexture2D(tempTmp);
            changeTexture = false;
        }
    }
    //This function is for button
    public void SendMessage(int img_num)
    {
        try
            {
                if (client == null || !client.Connected)
                {
                    Debug.Log("socket not connected!");
                    return;
                }
            //send data
                client.Send(Encoding.UTF8.GetBytes(img_num.ToString()));
            }
            catch (Exception e)
            {
                Debug.Log("Data Sending errors：" + e);
            }
        //close the socket when you no longer need it
        if (img_num == 4)
        {
            client.Close();
        }
    }
    // The thread will handle the coming message
    void GetMessage()
    {
        // use while(true) to keep looping. Otherwise it will stop after first message receviced.
        while (true)
        {
            //create a space for image (1024 * 1024 means maximum 1M)
            messTmp = new byte[1024 * 1024];
            var count = client.Receive(messTmp);

            if (count != 0)
            {
                Debug.Log("New Message Coming in");
                // messTmp changes from time to time, we need another variable to carry the message
                tempTmp = messTmp;
                changeTexture = true;
            }
        }
    }
    //bytes to texture
    public Texture2D bytesToTexture2D(byte[] imageBytes)
    {
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(imageBytes);
        return tex;
    }
}
