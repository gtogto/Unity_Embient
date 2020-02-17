using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Text;


public class bSerial : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM4", 115200, Parity.None, 8, StopBits.One);
    // Start is called before the first frame update

    //TODO : mgc Data format global val
    //public GameObject light1;
    
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

    public Light light1;
        

    // Use this for initialization
    void Start()
    {        
        sp.Open();
        sp.ReadTimeout = 50;        

        light1 = GetComponent<Light>();

    }

    // Update is called once per frame    
    void Update()
    {
        gto_color.color_array cHex = new gto_color.color_array();
        gto_color.color_array_int cInt = new gto_color.color_array_int();

        //byte[] bytesToSend = new byte[4] { 0x41, 0xFF, 0xFF, 0xFF };  //$D0 $F2 $FF $00 $06 $C7
        byte[] rgb_Color_1 = new byte[4] { 0x41, 0xFF, 0x00, 0x00 };  //$D0 $F2 $FF $00 $06 $C7
        byte[] rgb_Color_2 = new byte[4] { 0x41, 0xFF, 0x80, 0x00 };  //$D0 $F2 $FF $00 $06 $C7
        byte[] rgb_Color_3 = new byte[4] { 0x41, 0xFF, 0xBF, 0x00 };  //$D0 $F2 $FF $00 $06 $C7
        byte[] rgb_Color_4 = new byte[4] { 0x41, 0x00, 0xFF, 0x00 };  //$D0 $F2 $FF $00 $06 $C7
        byte[] rgb_Color_5 = new byte[4] { 0x41, 0x00, 0x00, 0xFF };  //$D0 $F2 $FF $00 $06 $C7
        byte[] rgb_Color_6 = new byte[4] { 0x41, 0x00, 0x00, 0xBB };  //$D0 $F2 $FF $00 $06 $C7
        byte[] rgb_Color_7 = new byte[4] { 0x41, 0x99, 0x00, 0xCC };  //$D0 $F2 $FF $00 $06 $C7
        byte[] rgb_Color_8 = new byte[4] { 0x41, 0xFF, 0xFF, 0xFF };  //$D0 $F2 $FF $00 $06 $C7
        byte[] rgb_Color_9 = new byte[4] { 0x41, 0xAA, 0xAA, 0xAA };  //$D0 $F2 $FF $00 $06 $C7
        byte[] rgb_Color_10 = new byte[4] { 0x41, 0x00, 0x00, 0x00 };  //$D0 $F2 $FF $00 $06 $C7

        Color redColor = new Color(0xFF, 0x00, 0x00);        
        Color orangeColor = new Color(0xFF, 0x80, 0x00);
        Color yellowColor = new Color(0xFF, 0xBF, 0x00);
        Color greenColor = new Color(0x00, 0xFF, 0x00);
        Color blueColor = new Color(0x00, 0x00, 0xFF);
        Color navyColor = new Color(0x00, 0x00, 0xBB);
        Color purpleColor = new Color(0x99, 0x00, 0xCC);


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
            
            
            if (Math.Round((x_percent_formula * 100 / 100.0)) != 0)
            {
                if (Math.Round((x_percent_formula * 100 / 100.0)) < 10)
                {
                    //sp.Write(rgb_Color_1, 0, rgb_Color_1.Length);
                    sp.Write(byte_merge(cHex.header,cHex.rgb_1), 0, rgb_Color_1.Length);                    
                    light1.color = redColor;
                }

                else if (Math.Round((x_percent_formula * 100 / 100.0)) < 20)
                {
                    //sp.Write(rgb_Color_2, 0, rgb_Color_2.Length);
                    sp.Write(byte_merge(cHex.header,cHex.rgb_2), 0, rgb_Color_1.Length);
                    
                    //sp.Write(cHex.header + cHex.rgb_2, 0, rgb_Color_1.Length);
                    light1.color = orangeColor;
                }

                else if (Math.Round((x_percent_formula * 100 / 100.0)) < 30)
                {
                    sp.Write(byte_merge(cHex.header, cHex.rgb_3), 0, rgb_Color_1.Length);
                    //light1.color = cInt.rgb_3;
                    light1.color = yellowColor;
                }

                else if (Math.Round((x_percent_formula * 100 / 100.0)) < 40)
                {
                    sp.Write(byte_merge(cHex.header, cHex.rgb_4), 0, rgb_Color_1.Length);
                    //light1.color = cInt.rgb_4;
                    light1.color = greenColor;
                }

                else if (Math.Round((x_percent_formula * 100 / 100.0)) < 50)
                {
                    sp.Write(byte_merge(cHex.header, cHex.rgb_5), 0, rgb_Color_1.Length);
                    //light1.color = cInt.rgb_5;
                    light1.color = blueColor;
                }

                else if (Math.Round((x_percent_formula * 100 / 100.0)) < 60)
                {
                    sp.Write(byte_merge(cHex.header, cHex.rgb_6), 0, rgb_Color_1.Length);
                    //light1.color = cInt.rgb_6;
                    light1.color = navyColor;
                }

                else if (Math.Round((x_percent_formula * 100 / 100.0)) < 70)
                {
                    sp.Write(byte_merge(cHex.header, cHex.rgb_7), 0, rgb_Color_1.Length);
                    //light1.color = cInt.rgb_7;
                    light1.color = purpleColor;
                }

                else if (Math.Round((x_percent_formula * 100 / 100.0)) < 80)
                {
                    sp.Write(byte_merge(cHex.header, cHex.rgb_8), 0, rgb_Color_1.Length);
                    //light1.color = color_array_int.rgb_8;
                }

                else if (Math.Round((x_percent_formula * 100 / 100.0)) < 90)
                {
                    sp.Write(byte_merge(cHex.header, cHex.rgb_9), 0, rgb_Color_1.Length);
                    //light1.color = color_array_int.rgb_9;
                }

                else if (Math.Round((x_percent_formula * 100 / 100.0)) < 100)
                {
                    sp.Write(byte_merge(cHex.header, cHex.rgb_10), 0, rgb_Color_1.Length);
                    //light1.color = color_array_int.rgb_10;
                }
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

    public static byte[] byte_merge(byte[] arg1, byte[] arg2)
    {
        byte[] tmp = new byte[arg1.Length + arg2.Length];
        for (int i = 0; i < arg1.Length; i++)
        {
            tmp[i] = arg1[i];
        }
        for (int j = 0; j < arg2.Length; j++)
        {
            tmp[arg1.Length + j] = arg2[j];
        }
        return tmp;
    }

}

namespace gto_color
{
    public class color_array
    {
        public byte[] header = new byte[1] { 0x41 };

        public byte[] rgb_1 = new byte[3] { 0xFF, 0x00, 0x00 };
        public byte[] rgb_2 = new byte[3] { 0xFF, 0x80, 0x00 };
        public byte[] rgb_3 = new byte[3] { 0xFF, 0xBF, 0x00 };
        public byte[] rgb_4 = new byte[3] { 0x00, 0xFF, 0x00 };
        public byte[] rgb_5 = new byte[3] { 0x00, 0x00, 0xFF };
        public byte[] rgb_6 = new byte[3] { 0x00, 0x00, 0xBB };
        public byte[] rgb_7 = new byte[3] { 0x99, 0x00, 0xCC };
        public byte[] rgb_8 = new byte[3] { 0xFF, 0xFF, 0xFF };
        public byte[] rgb_9 = new byte[3] { 0xAA, 0xAA, 0xAA };
        public byte[] rgb_10 = new byte[3] { 0x00, 0x00, 0x00 };
            
    }

    public class color_array_int
    {
        public Color rgb_1 = new Color(0xFF, 0x00, 0x00);
        public Color rgb_2 = new Color(0xFF, 0x80, 0x00);
        public Color rgb_3 = new Color(0xFF, 0xBF, 0x00);
        public Color rgb_4 = new Color(0x00, 0xFF, 0x00);
        public Color rgb_5 = new Color(0x00, 0x00, 0xFF);
        public Color rgb_6 = new Color(0x00, 0x00, 0xBB);
        public Color rgb_7 = new Color(0x99, 0x00, 0xCC);
    }
}