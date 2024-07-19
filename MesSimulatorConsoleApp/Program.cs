using MessageSimulator.Core.Models;
using MessageSimulator.Core.Models.Config;
using Newtonsoft.Json;

namespace MesSimulatorConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("请在启动命令中输入要选择的配置文件名称 xxx，配置文件位置: Configs/xxx.json，例如：MesSimulatorConsoleApp.exe xxx");
            }
            else
            {
                Console.Write("配置文件名称:");
                foreach (var arg in args)
                {
                    Console.Write($" {arg}");
                    Console.WriteLine();
                }

                try
                {
                    ConnectionCenter.Instance.LoadConfigAndCreateConnection(args[0]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR OCCURED: {ex.Message}{Environment.NewLine}{ex.StackTrace}");
                }
            }

            Console.ReadLine();
        }
    }
}
