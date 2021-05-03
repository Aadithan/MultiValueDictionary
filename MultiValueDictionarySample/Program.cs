using System;
using MultiValueDictionarySample.Helpers;
using MultiValueDictionarySample.Interfaces;

namespace MultiValueDictionarySample
{ 
    public class Program
    {
        static void Main(string[] arg)
        { 
            try
            {
                ProgramHelper.Initialize();
                var runner = ProgramHelper.GetInstance<IRunner>(); 
                runner.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.ReadKey();
            } 
        } 
    }
}
