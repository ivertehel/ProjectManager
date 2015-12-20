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
                using (StreamWriter _writer = File.AppendText(_logFileName))
                {
                    _writer.Write(string.Format("{0} | {1}:{2} | {3} | {4}", DateTime.Now.Date, DateTime.Now.TimeOfDay, level, title, message) + System.Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
