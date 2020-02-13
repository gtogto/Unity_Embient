using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Text;


public class bSerial : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM26", 115200, Parity.None, 8, StopBits.One);
    // Start is called before the first frame update

    public string message;
    
    public int val;
    public byte read_B;

    //TODO : mgc Data format global val
    //TODO ===============================================
    public String[] mgc_header;

    public String[] mgc_X;
    public String[] mgc_Y;
    public String[] mgc_Z;

    public String[] mgc_touch;
    public String[] mgc_gesture;

    public String[] mgc_cic_s;
    public String[] mgc_cic_w;
    public String[] mgc_cic_n;
    public String[] mgc_cic_e;
    public String[] mgc_cic_c;

    public String[] mgc_sd_s;
    public String[] mgc_sd_w;
    public String[] mgc_sd_n;
    public String[] mgc_sd_e;
    public String[] mgc_sd_c;

    public String mgc_tail;
    //TODO ===============================================

    public float x_int;
    public float y_int;
    public float z_int;

    public float x_percent_formula;
    public float y_percent_formula;
    public float z_percent_formula;
    //TODO ===============================================
    public float xx;
       


    // Use this for initialization
    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 50;
        Debug.Log("Connection started");
        sp.Write("test");
    }

    // Update is called once per frame    
    void Update()
    {
        //byte[] bytesToSend = new byte[4] { 0x41, 0xFF, 0xFF, 0xFF };  //$D0 $F2 $FF $00 $06 $C7
        byte[] rgb_Color_1 = new byte[4] { 0x41, 0xFF, 0x00, 0x00 };  //$D0 $F2 $FF $00 $06 $C7
        byte[] rgb_Color_2 = new byte[4] { 0x41, 0x80, 0x00, 0x00 };  //$D0 $F2 $FF $00 $06 $C7
        byte[] rgb_Color_3 = new byte[4] { 0x41, 0xFF, 0xBF, 0x00 };  //$D0 $F2 $FF $00 $06 $C7
        byte[] rgb_Color_4 = new byte[4] { 0x41, 0xFF, 0xFF, 0xFF };  //$D0 $F2 $FF $00 $06 $C7
        byte[] rgb_Color_5 = new byte[4] { 0x41, 0x00, 0xFF, 0x00 };  //$D0 $F2 $FF $00 $06 $C7
        byte[] rgb_Color_6 = new byte[4] { 0x41, 0x00, 0x00, 0xFF };  //$D0 $F2 $FF $00 $06 $C7
        byte[] rgb_Color_7 = new byte[4] { 0x41, 0x00, 0x00, 0xBB };  //$D0 $F2 $FF $00 $06 $C7
        byte[] rgb_Color_8 = new byte[4] { 0x41, 0xFF, 0xFF, 0xFF };  //$D0 $F2 $FF $00 $06 $C7
        byte[] rgb_Color_9 = new byte[4] { 0x41, 0xAA, 0xAA, 0xAA };  //$D0 $F2 $FF $00 $06 $C7
        byte[] rgb_Color_10 = new byte[4] { 0x41, 0x00, 0x00, 0x00 };  //$D0 $F2 $FF $00 $06 $C7


        if (sp.IsOpen)
        {
            message = "";            
            
            try
            {
                /*
                int bytes = sp.BytesToRead;
                byte[] buffer = new byte[bytes];

                print(sp.Read(buffer, 0, bytes));   // read a packet Length
                */

                //read_B = (byte)sp.ReadByte();

                //message = sp.read();        // output is '??'
                
                
                byte tempB = (byte)sp.ReadByte();
                
                while (tempB != 255)        //
                {
                    message += ((byte)tempB + " ");
                    tempB = (byte)sp.ReadByte();                    

                }

            }
            catch (System.Exception) { }

            //Debug.Log(message);
            //sp.Write(bytesToSend, 0, bytesToSend.Length);
            if (Math.Round((x_percent_formula * 100 / 100.0)) < 10 )
            {
                sp.Write(rgb_Color_1, 0, rgb_Color_1.Length);
            }

            else if (Math.Round((x_percent_formula * 100 / 100.0)) < 20)
            {
                sp.Write(rgb_Color_2, 0, rgb_Color_2.Length);
            }

            else if (Math.Round((x_percent_formula * 100 / 100.0)) < 30)
            {
                sp.Write(rgb_Color_3, 0, rgb_Color_3.Length);
            }

            else if (Math.Round((x_percent_formula * 100 / 100.0)) < 40)
            {
                sp.Write(rgb_Color_4, 0, rgb_Color_4.Length);
            }

            else if (Math.Round((x_percent_formula * 100 / 100.0)) < 50)
            {
                sp.Write(rgb_Color_5, 0, rgb_Color_5.Length);
            }

            else if (Math.Round((x_percent_formula * 100 / 100.0)) < 60)
            {
                sp.Write(rgb_Color_6, 0, rgb_Color_6.Length);
            }

            else if (Math.Round((x_percent_formula * 100 / 100.0)) < 70)
            {
                sp.Write(rgb_Color_7, 0, rgb_Color_7.Length);
            }

            else if (Math.Round((x_percent_formula * 100 / 100.0)) < 80)
            {
                sp.Write(rgb_Color_8, 0, rgb_Color_8.Length);
            }

            else if (Math.Round((x_percent_formula * 100 / 100.0)) < 90)
            {
                sp.Write(rgb_Color_9, 0, rgb_Color_9.Length);
            }

            else if (Math.Round((x_percent_formula * 100 / 100.0)) < 100)
            {
                sp.Write(rgb_Color_10, 0, rgb_Color_10.Length);
            }



        }



        if (message != "")
        {
            int messageSize = Encoding.Default.GetBytes(message).Length;
            //Debug.Log("[" + messageSize + "]"+ "receive int: " + message);
            print(message);

            //mgc_header = message.Substring(0, 7);
            //mgc_header = ByteSubstring(message, 0, 3);
            //print(mgc_header);

            /*
            mgc_X = message.Substring(8, 8);
            mgc_Y = message.Substring(9, 9);
            mgc_Z = message.Substring(10, 10);
                        
            String[] header = new String[2];
            header[0] = mgc_header.Substring(0, 3);
            header[1] = mgc_header.Substring(4, 7);            
            if (messageSize > 100)
            {
                print("header is -> " + mgc_header + " " + "X Y Z is " + "[" + mgc_X + "]" + "[" + mgc_Y + "]" + "[" + mgc_Z + "]");
            }
            */

            char[] delimiterChars = { ' ' };
            string[] message_split = message.Split(delimiterChars);

            /*
            for (int i = 0; i < message_split.Length; i++)
            {
                print("split  : " + message_split[0] + " " + message_split[1] + " " + message_split[2] + " " + message_split[3] + " " + message_split[4]);                
            }*/

            //print("X = " + "[" + message_split[2] + "]" + "[" + message_split[3] + "]");
            //print("Y = " + "[" + message_split[4] + "]" + "[" + message_split[5] + "]");
            //print("Z = " + "[" + message_split[6] + "]" + "[" + message_split[7] + "]");

            mgc_X = new String[2];
            Array.Copy(message_split, 2, mgc_X, 0, 2);

            mgc_Y = new String[2];
            Array.Copy(message_split, 4, mgc_Y, 0, 2);

            mgc_Z = new String[2];
            Array.Copy(message_split, 6, mgc_Z, 0, 2);
            
            int x_num1 = Convert.ToInt32(mgc_X[0]);
            int x_num2 = Convert.ToInt32(mgc_X[1]);

            int y_num1 = Convert.ToInt32(mgc_Y[0]);
            int y_num2 = Convert.ToInt32(mgc_Y[1]);

            int z_num1 = Convert.ToInt32(mgc_Z[0]);
            int z_num2 = Convert.ToInt32(mgc_Z[1]);

            String x_1_hex = x_num1.ToString("X");
            String x_2_hex = x_num2.ToString("X");

            String y_1_hex = y_num1.ToString("X");
            String y_2_hex = y_num2.ToString("X");

            String z_1_hex = z_num1.ToString("X");
            String z_2_hex = z_num2.ToString("X");

            String x_value = x_num1.ToString("X") + x_num2.ToString("X");
            String y_value = y_num1.ToString("X") + y_num2.ToString("X");
            String z_value = z_num1.ToString("X") + z_num2.ToString("X");

            x_int = int.Parse(x_value, System.Globalization.NumberStyles.HexNumber);
            y_int = int.Parse(y_value, System.Globalization.NumberStyles.HexNumber);
            z_int = int.Parse(z_value, System.Globalization.NumberStyles.HexNumber);


            /*
            print("X string = " + mgc_X[0] + mgc_X[1]);
            print("X int = " + x_num1 + x_num2);
            print("X hex = " + x_1_hex + x_2_hex);
            print("X sum hex = " + x_value);
            print("X final int " + x_int);
            */
            x_percent_formula = ((x_int / 65534) * 100);
            y_percent_formula = ((y_int / 65534) * 100);
            z_percent_formula = ((z_int / 65534) * 100);


            print("XYZ float :" + "[" + x_int + "]" + "[" + y_int + "]" + "[" + z_int + "]");
            print("XYZ percent :" + "[" + x_percent_formula + "]" + "[" + y_percent_formula + "]" + "[" + z_percent_formula + "]");
            print("XYZ MATH.Round :" +
                                        "[" + Math.Round((x_percent_formula * 100 / 100.0)) + "]" +
                                        "[" + Math.Round((y_percent_formula * 100 / 100.0)) + "]" +
                                        "[" + Math.Round((z_percent_formula * 100 / 100.0)) + "]");
            


            //print("X = " + mgc_X[0] + mgc_X[1]);


            /*
                String[] mgc_header = new String[2];
                mgc_header[0] = message_split[0];
                mgc_header[1] = message_split[1];
                print(mgc_header);

                print("----------------------------------------------------------------");

                String[] mgc_X = new String[3];
                mgc_X[0] = message_split[2];
                mgc_X[1] = message_split[3];
                mgc_X[2] = message_split[4];
                print(mgc_X);
             * /


            /*            
            if (messageSize > 100)
            {
                print("X Y Z is " + "[" + mgc_X + "]" + "[" + mgc_Y + "]" + "[" + mgc_Z + "]" + " ****** " + "[" + mgc_gesture + "]" + "[" + mgc_touch + "]");
            }*/

        }

    }

    public string ConvertStringToHex(string asciiString)
    {
        string hex = "";
        foreach (char c in asciiString)
        {
            int tmp = c;
            hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
        }

        return hex;
    }

    public void SendMessage(string msg)
    {           
        sp.WriteLine(msg);          
    }

    public void ClosePort()
    {
        sp.Close();
        Debug.Log("Closing port, because it was already open!");
    }

}