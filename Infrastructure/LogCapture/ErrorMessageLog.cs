using Application.Common.Error;

namespace Infrastructure.LogCapture
{
    internal class ErrorMessageLog : IErrorMessageLog
    {
        private readonly string _logsPath;

        public ErrorMessageLog()
        {
            string rootPath = AppDomain.CurrentDomain.BaseDirectory;
            _logsPath = Path.Combine(rootPath, "Logs");
        }

        public bool LogError(string layerName, string className, string methodName, string msg)
        {
            try
            {
                if (!Directory.Exists(_logsPath))
                    Directory.CreateDirectory(_logsPath);

                var dtNow = DateTime.Now.ToString("yyyy-MM-dd");

                string errLogs = Path.Combine(_logsPath, dtNow + "_ErrorLogs.txt");

                lock(this)
                {
                    using var sw = File.AppendText(errLogs);

                    sw.WriteLine("Layer Name :- " + layerName);
                    sw.WriteLine("Class Name :- " + className);
                    sw.WriteLine("Method Name :- " + methodName);
                    sw.WriteLine("Date Time :- " + DateTime.Now);
                    sw.WriteLine("Error Message :- " + msg);
                    sw.WriteLine(new string('-', 50));
                    sw.WriteLine(sw.NewLine);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
