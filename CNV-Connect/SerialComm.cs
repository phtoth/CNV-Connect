using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNV_Connect
{
    static class SerialComm
    {
        // vetor contendo as portas seriais disponiveis
        // vector containing the available serial ports 
        public static string[] ports = SerialPort.GetPortNames();
        public static string Board_Version = "";
       

        // Testa a Conexão com a Porta Serial
        // Test the Serial Port Connection
        public static void TestConnection(string Port)
        {
            int LogicControl = 0;
            const int MaxLC = 50;

            SerialPort _serialPortConnectionTest;

            _serialPortConnectionTest = new SerialPort();
            _serialPortConnectionTest.PortName = Port;
            _serialPortConnectionTest.BaudRate = 115200;
            _serialPortConnectionTest.Open();

            Thread.Sleep(50);

            _serialPortConnectionTest.Write("BOARD_VERSION");

            while (LogicControl != MaxLC)
            {
                string RText = _serialPortConnectionTest.ReadExisting();

                if (RText == null)
                {
                    Thread.Sleep(100);
                    LogicControl = LogicControl + 1;
                }
                else
                {
                    if (RText.Contains("BV="))
                    {
                        LogicControl = MaxLC;
                        int index = RText.IndexOf("=") + 1;
                        Board_Version = RText.Substring(index);
                    }
                    else
                    {
                        Thread.Sleep(100);
                        LogicControl = LogicControl + 1;
                    }
                }
            }
            // Encerra a conexão do teste serial
            // Close the serial test connection

            _serialPortConnectionTest.Close();
        }
    


    }
}
