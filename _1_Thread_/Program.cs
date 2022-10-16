using LibData;
using LibData.Delegates;
using LibData.Entities;
using System.Diagnostics;

namespace _1_Thread
{
    //public delegate void ConnectionCompleteDelegate(MyDataContext context);
    internal class Program
    {

        public static event ConnectionCompleteDelegate _connectionComplete;
        public static void Main(string[] args)
        {

            _connectionComplete += Program__onnectionComplete;
            var idThread = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("Main thread: {0}", idThread);


            //Thread queue = new Thread(Queue);
            //queue.Start();
            ////Queue();
            //SendMessendg();
            //queue.Join();  //чекає завершення потоку який додатуово запущений

            Thread connect = new Thread(ConnectionDatabase);
            connect.Start();
            //var countUsers = myDataContext.Users.Count();
            


        }

        private static void Program__onnectionComplete(MyDataContext context)
        {

            Console.WriteLine("Even completed connection {0}", Thread.CurrentThread.ManagedThreadId);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                context.Users.Add(new UserEntitie
                {
                    Name = "Іван",
                    Phone = "097 56 56 765",
                    Password = "123456"
                });
                context.SaveChanges();
                ShowMessenge($"Insert usrer {i}", ConsoleColor.Yellow);
            }


            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }

        private static void ConnectionDatabase()
        {           
            ShowMessenge("Begin connection database", ConsoleColor.Red) ;
            MyDataContext myDataContext = new MyDataContext();
            ShowMessenge("Connection database complited",ConsoleColor.Red) ;
            if (_connectionComplete!= null)
            {
                _connectionComplete(myDataContext);
            }
        }
        private static void ShowMessenge(string text, ConsoleColor color)
        {
            var comsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = comsoleColor;
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