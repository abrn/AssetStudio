using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssetStudioGUI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
	        foreach (var arg in args)
	        {
		        if (arg == "--headless")
		        {
			        CLI(string.Join(",", args));
			        return;
		        }
	        }
#if !NETFRAMEWORK
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AssetStudioGUIForm());
        }

        private static void CLI(string args)
        {
	        var resourcePath = Before(After(args, "-resourcepath:"), "\"");
	        var outputPath = Before(After(args, "-outputpath:"), "\"");

	        if (string.IsNullOrEmpty(resourcePath) || string.IsNullOrEmpty(outputPath) || !File.Exists(resourcePath))
	        {
		        Console.WriteLine($"Invalid input, format:\nAssetStudioGUI.exe --cli -resourcepath:\"C:\\Realm Of The Mad God\\resources.assets\" -outputpath:\"C:\\RotMGDump\\\"");
		        return;
	        }

	        if (!Directory.Exists(outputPath))
	        {
		        Directory.CreateDirectory(outputPath);
	        }
	        
	        Studio.assetsManager.LoadFiles(new string[] {resourcePath});
        }

        public static string Before(string full, string partial) {
	        var index = full.IndexOf(partial);
	        if (index == -1) {
		        return full;
	        }

	        return full.Substring(0, index);
        }


        public static string After(string full, string partial) {
	        var index = full.IndexOf(partial);
	        if (index == -1) {
		        return full;
	        }

	        return full.Substring(index + partial.Length);
        }
    }
}
