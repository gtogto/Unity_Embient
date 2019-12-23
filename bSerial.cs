using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class bSerial : MonoBehaviour
{
    SerialPort stream = new SerialPort("COM4");
    // Start is called before the first frame update

    private void Awake()
    {
        stream.BaudRate = 115200;
        stream.Parity = Parity.None;
        stream.StopBits = StopBits.One;
        stream.DataBits = 8;
        stream.Handshake = Handshake.None;
       
    }

    void Start()
    {
        stream.Open();
    }


    public void SendMessageToUSB(string msg)
    {
        stream.WriteLine(msg);
    }

    public void ClosePort()
    {
        stream.Close();
    }
    
}
