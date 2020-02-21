using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Text;

public class bSerial : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM4", 115200, Parity.None, 8, StopBits.One);
 
    public string message;
    public int packetLength;    

    //TODO : mgc Data format global val    
    public String[] mgc_header;
    public String[] mgc_X, mgc_Y, mgc_Z;
    public String[] mgc_touch;
    public String[] mgc_gesture;
    public String[] mgc_cic_s, mgc_cic_w, mgc_cic_n, mgc_cic_e, mgc_cic_c;
    public String[] mgc_sd_s, mgc_sd_w, mgc_sd_n, mgc_sd_e, mgc_sd_c;
    public String mgc_tail;

    //TODO : Color global val    
    public String[] rgb64 = new String[64];
    public String string_R, string_G, string_B;    
    public float x_int, y_int, z_int;    
    public float x_percent_formula, y_percent_formula, z_percent_formula; 
        
    public String magNetic_Packet;
    public SpectrumHandler spectrum;
    public float colorWheel_BarPosition;

    public Color[] colors = new Color[64];

    // Use this for initialization
    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 50;        

    }

    // Update is called once per frame    
    void Update()
    {
        if (sp.IsOpen)
        {          
            message = "";
            byte tempB;

            try
            {                
                tempB = (byte)sp.ReadByte();
                while (tempB != 255)       
                {
                    message += ((byte)tempB + " ");
                    tempB = (byte)sp.ReadByte();                    
                }
            }
            catch (System.Exception) {
            }
            
            /*check for STX&ETX*/
            if(st == true && ed == true)
            {
                magNetic_Packet = message;
            }
        }

        if (magNetic_Packet != "")
        {
            //print("********" + magNetic_Packet);
            char[] delimiterChars = { ' ' };
            string[] message_split = magNetic_Packet.Split(delimiterChars);

            mgc_header = new String[2];
            Array.Copy(message_split, 0, mgc_header, 0, 2);

            mgc_X = new String[2];
            Array.Copy(message_split, 2, mgc_X, 0, 2);

            mgc_Y = new String[2];
            Array.Copy(message_split, 4, mgc_Y, 0, 2);

            mgc_Z = new String[2];
            Array.Copy(message_split, 6, mgc_Z, 0, 2);

            int header_num1 = Convert.ToInt32(mgc_header[0]);
            int header_num2 = Convert.ToInt32(mgc_header[1]);

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

            x_percent_formula = ((x_int / 65534) * 100);
            y_percent_formula = ((y_int / 65534) * 100);
            z_percent_formula = ((z_int / 65534) * 100);

            if (header_num1 == 254 && header_num2 == 253)
            {
                if (x_percent_formula > 45)
                {
                    colorWheel_BarPosition = colorWheel_BarPosition + 1;
                }
                else if (x_percent_formula > 1 && x_percent_formula < 20)
                {
                    colorWheel_BarPosition = colorWheel_BarPosition - 1;
                }
                else if (y_percent_formula > 0 && x_percent_formula == 0)
                {
                    colorWheel_BarPosition = colorWheel_BarPosition - 1;
                }
                else if (z_percent_formula > 0 && x_percent_formula == 0)
                {
                    colorWheel_BarPosition = colorWheel_BarPosition - 1;
                }
                
            }
            else
            {
                print("PACKET SIZE ERR");
            }            
                        
            if (colorWheel_BarPosition < 0)
            {
                colorWheel_BarPosition = 0;
            }
            if (colorWheel_BarPosition > 100)
            {
                colorWheel_BarPosition = 100;
            }

            /*send to UNITY Color Wheel Bar*/
            spectrum.changValue(colorWheel_BarPosition);

            /* Read to UNITY Engine Color.Elements*/
            float R_red, G_green, B_blue;
            R_red = spectrum.CurrentColor.r * 255;
            G_green = spectrum.CurrentColor.g * 255;
            B_blue = spectrum.CurrentColor.b * 255;

            /* Float to String*/
            String string_R = R_red.ToString();
            String string_G = G_green.ToString();
            String string_B = B_blue.ToString();

            /* String to int*/
            int string_R_int = Convert.ToInt32(string_R);
            int string_G_int = Convert.ToInt32(string_G);
            int string_B_int = Convert.ToInt32(string_B);
                       
            /*int to byte*/
            byte R_result = Convert.ToByte(string_R_int);
            byte G_result = Convert.ToByte(string_G_int);
            byte B_result = Convert.ToByte(string_B_int);
            
            byte[] writeToLED = new byte[4] {0x41, R_result, G_result, B_result };

            /*write to LED Colors*/
            sp.Write(writeToLED, 0 ,4);
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

    public string str2hex(string strData)
    {
        string resultHex = string.Empty;
        byte[] arr_byteStr = Encoding.Default.GetBytes(strData);

        foreach (byte byteStr in arr_byteStr)
            resultHex += string.Format("{0:X2}", byteStr);

        return resultHex;
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

