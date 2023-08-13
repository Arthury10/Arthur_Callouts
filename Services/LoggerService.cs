using System;
using System.IO;

namespace ArthurCallouts.Services
{
    public class LoggerService
    {
        private void Message(string message)
        {
            string logPath = "plugins/lspdfr/ArthurCallouts/logs/";
            string logFile = logPath + "ArthurCallouts.log";

            // Verifique se o diretório existe, e crie se não existir
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            using (StreamWriter writer = File.AppendText(logFile))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }

        public void Error(string message)
        {
            Message($"ERROR: {message}");
        }

        public void Info(string message)
        {
            Message($"INFO: {message}");
        }

        public void Debug(string message)
        {
            Message($"DEBUG: {message}");
        }

        public void Warning(string message)
        {
            Message($"WARNING: {message}");
        }

        public void Exception(Exception ex)
        {
            Message($"EXCEPTION: {ex.Message}");
        }

    }
}
