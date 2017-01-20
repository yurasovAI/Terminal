using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Windows;
using System.Threading;

namespace TaskWPF
{
    class SerialPortClass
    {

        SerialPort _serialPort;
        string inputSerial;

        static string data;


        public event Action<string> CardCodeEvent;


        public SerialPortClass()
        {

            //string[] ports = SerialPort.GetPortNames();

            //for (int i = 0; i < ports.Length; i++)
            //{


            //    Console.WriteLine(ports[i]);

            //}


            SerialPortInitialize();
        }


        public void SerialPortInitialize()
        {

            _serialPort = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
            _serialPort.DataReceived += _serialPort_DataReceived;

        }



        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string s = Read();

            
            String[] arrstr = s.Split(new Char[] { '[', ']' });

            data = arrstr[1];

            CardCodeEvent(data);
            //foreach (string a in arrstr)
            //{
            //    Console.WriteLine(a);
            //}

            _serialPort.Close();



        }



        public void OpenPort(/*string portName*/)
        {

            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }

            else
            {
                _serialPort.Open();
            }

        }

        public string Read()
        {
            inputSerial = _serialPort.ReadExisting();
            return inputSerial;
        }


        public string GetData()
        {
            return data;
        }

    }
}
