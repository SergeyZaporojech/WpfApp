using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1_Thread
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var idThread = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("Main thread: {0}", idThread);
            Thread queue = new Thread(Queue);
            queue.Start();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            //Queue();
            SendMessendg();
            queue.Join();  //чекає завершення потоку який додатуово запущений

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);

        }

        private static void SendMessendg()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(300);
                Console.WriteLine("Send messendg {0}", i + 1);
            }
        }


        private static void Queue()
        {
            var idThread = Thread.CurrentThread.ManagedThreadId;
            var comsoleColor = Console.ForegroundColor;
            Console.WriteLine("Other thread: {0}", idThread);
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(200);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Обробка пасажира {0}", i + 1);
                Console.ForegroundColor = comsoleColor;
            }
        }


    }
}
