using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PythonCaller
{
    class Program
    {
        static void Main(string[] args)
        {

            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var data = new
            {
                Orders = DataGenerator.GetOrders(10000),
                HistoricalOrders = DataGenerator.GetHistoricalOrders(5000)
            };

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            var startInfo = new ProcessStartInfo
            {
                FileName = @"C:\python3\python.exe",
                Arguments = @"C:\Code\PythonCaller\PythonCaller\bin\Debug\netcoreapp3.0\test1.py",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.OutputDataReceived += Process_OutputDataReceived;
                process.ErrorDataReceived += Process_ErrorDataReceived;

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                using (var jsonWriter = new JsonTextWriter(process.StandardInput))
                {
                    serializer.Serialize(jsonWriter, data);

                    jsonWriter.Flush();
                }
                process.WaitForExit();
            }

            sw.Stop();
            Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}ms");
        }

        private static void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.Error.WriteLine(e.Data);
        }

        private static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
    }
}
