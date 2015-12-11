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

        private StringWriter _writer;

        private FileStream _file;

        private Logger()
        {
            string logFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFile");
            _file = new FileStream(logFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            var writer = new BinaryWriter(_file);
        }

        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Logger();

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

                _writer.Write(string.Format("{0} | {1} | {2} | {3}", DateTime.Now.ToLongTimeString(), level, title, message));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
