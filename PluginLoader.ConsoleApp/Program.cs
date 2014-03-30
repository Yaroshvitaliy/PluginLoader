using System;

namespace PluginLoader.ConsoleApp
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("1 - Type imports");
            Console.WriteLine("2 - Assembly imports");
            Console.WriteLine("3 - Directory imports");
            Console.WriteLine();
            Console.Write("Choose import type (1, 2 or 3): ");

            var choise = Console.ReadLine();
            Console.WriteLine("");

            var programKernel = new ProgramKernel();

            switch (choise)
            {
                case "1":
                    programKernel.TypeImports();
                    break;
                case "2":
                    programKernel.AssemblyImports();
                    break;
                case "3":
                    programKernel.DirectoryImports();
                    break;
            }

            programKernel.StartGame();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
