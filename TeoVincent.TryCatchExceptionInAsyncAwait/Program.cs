using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeoVincent.TryCatchExceptionInAsyncAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            FuncCatchExAsync("1000");
            WrongTryCatchException();
            Console.ReadLine();
        }

        static Task<int> StrToIntAsync(string integer)
        {
            Func<object, int> strToInt = (object value) =>
            {
                return int.Parse((string)value);
            };
            return Task<int>.Factory.StartNew(strToInt, integer);
        }

        static async Task<int> ExceptionFuncAsync(string integer)
        {
            int result = await StrToIntAsync(integer);
            throw new Exception("My exception.");
            return result;
        }

        static async void FuncCatchExAsync(string integer)
        {
            try
            {
                int result = await ExceptionFuncAsync(integer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static Task GetTaskWithException()
        {
            Action action = () => { throw new Exception("My uncatched ex"); };
            return Task.Factory.StartNew(action);
        }

        static async Task WrongTryCatchException()
        {
            try
            {
                await GetTaskWithException();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
