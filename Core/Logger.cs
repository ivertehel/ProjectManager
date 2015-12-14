using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core
{
    public class Logger
    {
        private static Logger _instance;

        private StreamWriter _writer;

        private Logger()
        {
            string logFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.txt");
            _writer = File.AppendText(logFileName);
        }

        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Logger();
                }

                return _instance;
            }
        }

        public static void Error(string title, string message)
        {
            Instance.WriteMessage("ERROR", title, message);
        }

        public static void Warning(string title, string message)
        {
            Instance.WriteMessage("Warning", title, message);
        }

        public static void Info(string title, string message)
        {
            Instance.WriteMessage("Info", title, message);
        }

        private void WriteMessage(string level, string title, string message)
        {
            try
            {
                if (_writer == null)
                    return;
                _writer.Write(string.Format("{0} | {1}:{2} | {3} | {4} | {5}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString(), DateTime.Now.Millisecond, level, title, message) + System.Environment.NewLine);
                _writer.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
