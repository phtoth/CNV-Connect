using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNV_Connect
{
    static class DataQueue
    {
        public static Queue<string> ReceivedData = new Queue<string>();

        private static string Data = "";

        public static Thread Turret = new Thread(ProcessEvents);

        static public void ProcessEvents()
        {
            while (true)
            {
                if (ReceivedData.Count > 0)
                {
                    Data = ReceivedData.Dequeue();

                    if (Data == "Hello!")
                    {
                        MessageBox.Show("Hello!", "HiS");
                    }
                }
            }

        }

    }
}
