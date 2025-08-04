using System.IO.Ports;

namespace CNV_Connect
{
    static public class SerialComm
    {
        // vetor contendo as portas seriais disponiveis
        // vector containing the available serial ports 
        public static string[] ports = SerialPort.GetPortNames();
        public static string Board_Version = "";

        // Variável que armazena a conexão serial
        // Variable that stores the serial connection
        public static SerialPort _serialPortConnection;

        // Token de cancelamento da Thread
        // Cancellation token for the thread
        public static CancellationTokenSource GladosToken = new();

        // Thread que recebe os dados da porta serial
        // Thread that receives data from the serial port
        public static Thread Glados = new Thread(() => SerialReceive(GladosToken.Token));

        // Variável que indica se a conexão serial está ativa
        // Variable that indicates if the serial connection is active
        public static bool SAlive = false;

        // Método que testa a Conexão com a Porta Serial
        // Method that tests the connection with the serial port
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

            // Envia o comando para obter a versão da placa
            // Sends the command to get the board version
            _serialPortConnectionTest.Write("BOARD_VERSION");

            // Aguarda a resposta da placa
            // Waits for the board's response
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

        // Um teste de conexão serial a ser executado periodicamente
        // A serial connection test to be executed periodically
        public static void StillAlive(string Port)
        {

        }

        // Conexão Serial com a placa
        // Serial connection with the board

        public static void SerialConnet(string Port)
        {

            SerialComm._serialPortConnection = new SerialPort();
            SerialComm._serialPortConnection.PortName = Port;
            SerialComm._serialPortConnection.BaudRate = 115200;
            SerialComm._serialPortConnection.Open();

            Thread.Sleep(50);
        }

        // Metodo de Envio de Dados
        // Method for sending data
        public static void SerialSend(string Data)
        {
            SerialComm._serialPortConnection.Write(Data);
        }

        // Metodo de Recebimento dos Dados
        // Method for receiving data
        // ToDo: Implementar o recebimento de dados de forma assíncrona
        // ToDo: Implement receiving data asynchronously
        // Controlar os erros recebidos
        // Control the received errors
        public static void SerialReceive(CancellationToken token)
        {
            string data = "";
            while (!token.IsCancellationRequested)
            {
                try
                {
                    data = _serialPortConnection.ReadLine().Split('\r')[0];
                }
                catch (System.IO.IOException error)
                {
                    Console.WriteLine($"Serial port error: {error.Message}");
                }
                catch (System.InvalidOperationException error)
                {
                    Console.WriteLine($"Serial port error: {error.Message}");
                }

                if (data != null && data != "")
                {
                    DataQueue.ReceivedData.Enqueue(data);
                    Thread.Sleep(50);
                }
            }
        }

        // Método que inicia a Thread de Recebimento Serial
        // Method that starts the Serial Receive Thread
        public static void StartSerialReceiveThread()
        {
            Glados.Start();
        }

        // Método que para a Thread de Recebimento Serial
        // Method that stops the Serial Receive Thread
        public static void StopSerialReceiveThread()
        {
            GladosToken.Cancel();
            Glados.Join();
            GladosToken.Dispose();
            Thread.Sleep(1500);
        }
    }
}
