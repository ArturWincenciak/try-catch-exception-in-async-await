using System;
using System.Threading;
using System.Threading.Tasks;

namespace TeoVincent.TryCatchExceptionInAsyncAwait
{
    class Program
    {
        private static void Main(string[] args)
        {
            DoSafeThingAsync();
            Console.ReadLine();
        }

        private static async void DoSafeThingAsync()
        {
            try
            {
                //if you want to see wrongly safe method uncomment this line below
                //await DoDangerousThingInNewTask();
                await DoDangerousThingAsync();
                Console.WriteLine("next calculations and logic");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static Task DoDangerousThingInNewTask()
        {
            return Task<int>.Factory.StartNew(() =>
            {
                SimpulateLongRunningAsync();
                throw new Exception("the intentional exception");
            });
        }

        private static async Task DoDangerousThingAsync()
        {
            await SimpulateLongRunningAsync();
            throw new Exception("the intentional exception");
        }

        private static async Task SimpulateLongRunningAsync()
        {
            Console.WriteLine("simpulate long running operation");
            Thread.Sleep(1000);
        }
    }
}
