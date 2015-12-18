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

        string _logFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.txt");

        private Logger()
        {
            _writer = File.AppendText(_logFileName);

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
        StreamWriter _writer;
        private void WriteMessage(string level, string title, string message)
        {
            string s = Guid.NewGuid().ToString();
            try
            {
                // DateTime.Now.ToString();
                _writer.Write(string.Format("{0} | {1}:{2} | {3} | {4} | {5}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString(), DateTime.Now.Millisecond, level, title, message) + System.Environment.NewLine);
                _writer.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
