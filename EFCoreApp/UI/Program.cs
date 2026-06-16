using Infrastructure;

namespace UI
{
    internal static class Program
    {
        static async Task Main()
        {
            var di = new DependencyInjection();

            await di.InitializeDatabaseAsync();

            using var app = di.GetConsoleApplication();

            await app.RunAsync();
        }
    }
}
