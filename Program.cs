using AdoFrameWork.Services;
using static AdoFrameWork.Core.RegisterService;
using AdoFrameWork.Core;
internal class Program
{
    private static void Main(string[] args)
    {
        RegisterServices();
        InputReader input = new InputReaderImpl();

        input.StartScreen();
        DisposeServices();
    }
}