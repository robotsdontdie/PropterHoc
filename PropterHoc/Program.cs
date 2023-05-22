using System.Reflection;
using Serilog;

namespace PropterHoc;
internal class Program
{
    internal static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        string local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        var session = new Initter().Init();
        var inputLoop = new InputLoop(session);
        inputLoop.DoLoop();
    }
}
