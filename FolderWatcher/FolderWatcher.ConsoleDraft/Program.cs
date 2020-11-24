using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FolderWatcher.ConsoleDraft
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            Task task1 = new Task(() =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                    Console.WriteLine("task1");
                }

            }, token);

            task1.Start();

            Thread.Sleep(10000);
            source.Cancel();


            Task task2 = new Task(() =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                    Console.WriteLine("task2");
                }
            }, token);



            source = new CancellationTokenSource();
            token = source.Token;


            task2.Start();

            Thread.Sleep(10000);
            source.Cancel();


            Console.ReadKey();
        }
    }
}
