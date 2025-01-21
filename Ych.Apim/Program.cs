using System;
using System.Threading.Tasks;

namespace Ych.Apim
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new ApimConsole().Run();
        }
    }
}
