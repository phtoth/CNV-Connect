// Classe que controla a fila de dados recebidos do CNV-Connect
// Class that controls the queue of data received from CNV-Connect

namespace CNV_Connect
{
    static class DataQueue
    {
        // Link com a função que envia os dados para o simulador
        // Link with the function that sends data to the simulator
        public static Action<int, int>? SendToSimDelegate;

        // Fila de dados recebidos do CNV-Connect
        // Queue of data received from CNV-Connect
        public static Queue<string> ReceivedData = new Queue<string>();

        private static string Data = "";

        // Token de cancelamento da Trhead
        // Cancellation token for the thread
        public static CancellationTokenSource FimDaFila = new();

        // Thread que processa os eventos da fila
        // Thread that processes the events of the queue
        public static Thread Turret = new Thread(() => ProcessEvents(FimDaFila.Token));

        // Método que processa os eventos da fila enquanto a mesma tem mais de 1 item
        // Method that processes the events of the queue while it has more than 1 item

        static public void ProcessEvents(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (ReceivedData.Count > 0)
                {
                    Data = ReceivedData.Dequeue();
                    ProcessMessage(Data);
                }
            }
        }

        //Método que para o processamento dos eventos da fila
        // Method that Stops the processing of the queue events
        public static void StopProcessEvents()
        {
            FimDaFila.Cancel();
            Turret.Join();
            FimDaFila.Dispose();
            Thread.Sleep(1500);
        }

        // Método que processa a mensagem recebida, para ser enviada ao simulador
        // Method that processes the received message to be sent to the simulator
        public static void ProcessMessage(string message)
        {
            int Pos = message.IndexOf(",");
            if (Pos > 0)
            {
                string teste = message.Substring(0, Pos);
                int Code = int.Parse(message.Substring(0, Pos));
                int Value = int.Parse(message.Substring(Pos + 1));
                SendToSimDelegate?.Invoke(Code, Value);
            }
        }

    }
}
