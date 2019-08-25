using System;
using System.IO;
using System.Linq;
using System.IO.Compression;

namespace PakujPliki
{
    internal static class Program
    {
        static void Main()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');

            string operat = baseDirectory.Split(Path.DirectorySeparatorChar).Last();

            operat = "P" + operat.Split('P').Last() + "_p-dan";

            if (Directory.Exists(Path.Combine(baseDirectory, "Pliki")))
            {
                DirectoryInfo source = new DirectoryInfo(Path.Combine(baseDirectory, "Pliki"));
                DirectoryInfo dest = new DirectoryInfo(Path.Combine(baseDirectory, operat));

                Directory.CreateDirectory(Path.Combine(baseDirectory, operat));

                Console.WriteLine($"Kopiowanie plików do folderu {Path.Combine(baseDirectory, operat)}...\n");

                CopyFilesRecursively(source, dest);

                ZipFile.CreateFromDirectory(Path.Combine(baseDirectory, operat), Path.Combine(baseDirectory, operat + ".zip"));

                Directory.Delete(Path.Combine(baseDirectory, operat), true);

                Console.WriteLine("\nWciśnij dowolny klawisz.");
                Console.ReadKey();
            }
        }

        private static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                Console.WriteLine("[K]: " + dir.FullName);
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            }
    
            foreach (FileInfo file in source.GetFiles())
            {
                Console.WriteLine("[P]: " + file.FullName);
                file.CopyTo(Path.Combine(target.FullName, file.Name));
            }
                
        }
    }
}
