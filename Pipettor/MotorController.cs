﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipettor
{
    class MotorController
    {

        SerialPort serialPort;
        static MotorController instance;
        Dictionary<int, double> eachMotorDegree;
        static public MotorController Instance
        {
            get
            {
                if (instance == null)
                    instance = new MotorController();
                return instance;
            }
        }

        private MotorController()
        {
            serialPort = new SerialPort(GlobalVars.Instance.ComPort, 115200);
            serialPort.DataReceived += SerialPort_DataReceived;
            eachMotorDegree = new Dictionary<int, double>();
            eachMotorDegree[1] = 0;
            eachMotorDegree[2] = 0;

        }

        public void OpenSerialPort()
        {
            serialPort.Open();
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            int bytesToRead = sp.BytesToRead;
            byte[] BRecieve = new byte[bytesToRead];
            int bytesRead = 0;
            bytesRead = sp.Read(BRecieve, 0, bytesToRead);
            string str = ToHexString(BRecieve);
            Debug.WriteLine(str);
        }



         string ToHexString(byte[] bytes)
        {
            string hexString = string.Empty;

            if (bytes != null)

            {

                StringBuilder strB = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)

                {

                    strB.Append(bytes[i].ToString("X2"));
                    strB.Append(' ');
                }

                hexString = strB.ToString();

            }
            return hexString;

        }
        private void JudgeSmallerThan10(int x, List<int> smallerThan10)
        {
            if (x < 10)
                smallerThan10.Add(x);
        }

        private void ReadEncoder()
        {
            byte[] buffer = new byte[] { 0x3E, 0x90, 0x01, 0x00, 0xCF };
            serialPort.Write(buffer, 0, buffer.Count());
        }

        public void SetSpeed(double speedDouble)
        {

            List<int> array = new List<int>() { 1, 2, 5, 6, 7, 89, 55 };
            List<int> smallerThan10 = new List<int>();
            //array.ForEach(x => JudgeSmallerThan10(x,smallerThan10));
            array.ForEach((x) =>
            {
                if (x < 10)
                    smallerThan10.Add(x);
            });

            int angleSpeed = (int)(speedDouble * 36000);

            List<byte> buffer = new List<byte>();
            buffer.Add(0x3E);
            buffer.Add(0xA2);
            buffer.Add(0x01);
            buffer.Add(0x04);
            buffer.Add(0xE5);

            byte[] angleSpeedBuff = BitConverter.GetBytes(angleSpeed);
            buffer.Add(angleSpeedBuff[0]);
            buffer.Add(angleSpeedBuff[1]);
            buffer.Add(angleSpeedBuff[2]);
            buffer.Add(angleSpeedBuff[3]);
            buffer.Add((byte)(buffer[5] + buffer[6] + buffer[7] + buffer[8]));

            serialPort.Write(buffer.ToArray(), 0, buffer.Count);
        }


        public void RotateAtSpeed(byte motorID, double angleDouble)
        {
            double currentDegree = eachMotorDegree[motorID];
            byte[] buffer = new byte[10];//{0x3E, 0xA4, 0x01, 0x0C, 0xEF, 0x50, 0x46, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x28, 0x23, 0x00, 0x00, 0xE1 };
            //List<byte> buffer = new List<byte>();
            //buffer.Add(0x3E);
            buffer[0] = 0x3E;
            buffer[1] = 0xA5;
            buffer[2] = motorID;
            buffer[3] = 0x04;
            buffer[4] = (byte)(buffer[0] + buffer[1] + buffer[2] + buffer[3]);
            buffer[5] = angleDouble > currentDegree ? (byte)0x0 : (byte)0x1;
            int angle = (int)(angleDouble * 100);
            byte[] angleBuff = BitConverter.GetBytes(angle);
            buffer[6] = angleBuff[0];
            buffer[7] = angleBuff[1];
            buffer[8] = angleBuff[2];
            buffer[9] = (byte)(buffer[5] + buffer[6] + buffer[7] + buffer[8]);
            List<Byte> bytes = new List<byte>(buffer);

            //serialPort.Write(bytes.ToArray(), 0, buffer.Count);
            serialPort.Write(buffer, 0, buffer.Length);
            eachMotorDegree[motorID] = angleDouble;

        }
        public void Rotate(double angleDouble) //0.01
        {
            int angle = (int)(angleDouble * 100);


            byte[] input = new byte[18];//{0x3E, 0xA4, 0x01, 0x0C, 0xEF, 0x50, 0x46, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x28, 0x23, 0x00, 0x00, 0xE1 };
            //List<byte> buffer = new List<byte>();
            //buffer.Add(0x3E);
            input[0] = 0x3E;
            input[1] = 0xA4;
            input[2] = 0x01;
            input[3] = 0x0C;
            input[4] = (byte)(input[0] + input[1] + input[2] + input[3]);

            byte[] angleBuff = BitConverter.GetBytes(angle);
            input[5] = angleBuff[0];
            input[6] = angleBuff[1];
            input[7] = angleBuff[2];
            input[8] = angleBuff[3];
            input[9] = 0x0;
            input[10] = 0x0;
            input[11] = 0x0;
            input[12] = 0x0;
            input[13] = 0x28;//angelspeed 9000
            input[14] = 0x23;
            input[15] = 0x0;
            input[16] = 0x0;
            input[17] = (Byte)(input[5] + input[6] + input[7] + input[8] + input[9] + input[10] + input[11] + input[12] + input[13] + input[14] + input[15] + input[16]);
            serialPort.Write(input,0,input.Length);
        }
    }
}
