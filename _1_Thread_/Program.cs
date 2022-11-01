using LibData;
using LibData.Delegates;
using LibData.Entities;
using System.Diagnostics;

namespace _1_Thread
{
    //public delegate void ConnectionCompleteDelegate(MyDataContext context);
    internal class Program
    {
        public static event ConnectionCompleteDelegate _connectionComplete;                           //створюємо подію щодо приєднання до бази даних
        public static void Main(string[] args)
        {
            _connectionComplete += Program__СonnectionComplete;                                       //підписуємо на івент метод
            var idThread = Thread.CurrentThread.ManagedThreadId;                                      // виводемо номер потоку та присвоюємо його змінній 
            Console.WriteLine("Main thread: {0}", idThread);                                          // виводемо змінну в консолі про номер потоку


            //Thread queue = new Thread(Queue);
            //queue.Start();
            ////Queue();
            //SendMessendg();
            //queue.Join();  //чекає завершення потоку який додатуово запущений

            Thread connect = new Thread(ConnectionDatabase);                                           //створюємо додатковий потік та передаємо в нього метод ConnectionDatabase
            connect.Start();                                                                           //запускаємо його
            //var countUsers = myDataContext.Users.Count();            


        }

        private static void Program__СonnectionComplete(MyDataContext context)
        {

            Console.WriteLine("Even completed connection {0}", Thread.CurrentThread.ManagedThreadId); //виводемо номер потоку 
            Stopwatch stopWatch = new Stopwatch();                                                    // створюємо для обрахунку часу виконання дії
            stopWatch.Start();
            for (int i = 0; i < 1000; i++)                                                            // цикл для додавання юзерів
            {                                                                                        
                context.Users.Add(new UserEntitie                                                     //додаємо одного юзера в циклі
                {                                                                                    
                    Name = "Іван",                                                                   
                    Phone = "097 56 56 765",                                                         
                    Password = "123456"                                                              
                });                                                                                  
                context.SaveChanges();                                                               //зберігаємо зміни в базі даних
                ShowMessenge($"Insert usrer {i}", ConsoleColor.Yellow);                              //виводемо номер доданого юзера
            }


            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);                                             //виводемо час виконання додавання юзерів до бази даних
        }

        private static void ConnectionDatabase()                                                    //метод з'єднання з базою даних
        {                                                   
            ShowMessenge("Begin connection database", ConsoleColor.Red) ;                          //виводемо в консоль інформацію                     
            MyDataContext myDataContext = new MyDataContext();                                     //підключаємося через  class MyDataContext
            ShowMessenge("Connection database complited",ConsoleColor.Red) ;                       //виводемо в консоль інформацію
            if (_connectionComplete!= null)                                                        
            {
                _connectionComplete(myDataContext);                                               //передаємо в івент данні про підключення до БД
            }
        }
        private static void ShowMessenge(string text, ConsoleColor color)                         // метод кольорового виводу в консоль
        {
            var comsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = comsoleColor;
        }

        private static void SendMessendg()                                                      // метод виводу повідомлень з затримкою в 3 с.
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(300);
                Console.WriteLine("Send messendg {0}", i + 1);
            }
        }

        private static void Queue()
        {
            var idThread = Thread.CurrentThread.ManagedThreadId;                              //вивод номеру потоку та присвоюємо його змінній
            var comsoleColor = Console.ForegroundColor;                                       //колір консолі присвоюємо його змінній 
            Console.WriteLine("Other thread: {0}", idThread);                                 //виводемо номер потоку в консолі
            for (int i = 0; i < 10; i++)                                                      
            {                                                                                 
                Thread.Sleep(200);                                                            //затримка 2 с.
                Console.ForegroundColor = ConsoleColor.Green;                                 //змінюємо колір консолі
                Console.WriteLine("Обробка пасажира {0}", i + 1);                             //вивод в консолі інформації
                Console.ForegroundColor = comsoleColor;                                       //повертаємо колір консолі 
            }
        }
    }
}