using Application.Common.Error;

namespace Infrastructure.LogCapture
{
    internal class ErrorMessageLog : IErrorMessageLog
    {
        public bool LogError(string layerName, string className, string methodName, string msg)
        {
            try
            {
                string fullPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                DateTime dtNow = DateTime.Now;
                string date = dtNow.Year + "-" + dtNow.Month + "-" + dtNow.Day;

                fullPath = fullPath + "Logs";
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }

                string filePath = fullPath + "\\" + date + "_ErrorLog.txt";
                if (!File.Exists(filePath))
                {
                    TextWriter sw = new StreamWriter(filePath);

                    sw.WriteLine("Layer Name :-" + layerName);
                    sw.WriteLine("Class Name :-" + className);
                    sw.WriteLine("Method Name :-" + methodName);
                    sw.WriteLine("Date Time :-" + DateTime.Now);
                    sw.WriteLine("Error Message :-" + msg);

                    sw.Close();
                }
                else
                {
                    string oldLine = File.ReadAllText(filePath);
                    TextWriter tw = new StreamWriter(filePath);

                    tw.WriteLine(oldLine);

                    tw.WriteLine(tw.NewLine);

                    tw.WriteLine("Layer Name :-" + layerName);
                    tw.WriteLine("Class Name :-" + className);
                    tw.WriteLine("Method Name :-" + methodName);
                    tw.WriteLine("Date Time :-" + DateTime.Now);
                    tw.WriteLine("Error Message :-" + msg);
                    tw.Close();
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
