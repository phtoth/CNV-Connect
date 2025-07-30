namespace CNV_Connect
{
    static class DataQueue
    {
        public static Action<int, int>? SendToSimDelegate;

        public static Queue<string> ReceivedData = new Queue<string>();

        private static string Data = "";

        public static CancellationTokenSource FimDaFila = new();

        public static Thread Turret = new Thread(() => ProcessEvents(FimDaFila.Token));

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

        public static void StopProcessEvents()
        {
            FimDaFila.Cancel();
            Turret.Join();
            FimDaFila.Dispose();
            Thread.Sleep(1500);
        }

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
