using System;
using System.Collections.Generic;
using System.IO;


namespace LogLibrary
{
    /// <summary>
    /// A Logging class implementing the Singleton pattern and an internal Queue to be flushed perdiodically
    ///http://www.bondigeek.com/blog/2011/09/08/a-simple-c-thread-safe-logging-class/
    /// </summary>
    public class LogWriter
    {
        private static LogWriter instance;
        private static Queue<Log> logQueue;
        private static readonly string logDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        private static string logFile = "";


        /// <summary>
        /// Private constructor to prevent instance creation
        /// </summary>
        private LogWriter() { }


        static public bool Enable = true;


        /// <summary>
        /// An LogWriter instance that exposes a single instance
        /// </summary>
        public static LogWriter Instance
        {
            get
            {
                // If the instance is null then create one and init the Queue
                if (instance == null)
                {
                    instance = new LogWriter();
                    logQueue = new Queue<Log>();
                }
                return instance;
            }
        }

        /// <summary>
        /// The single instance method that writes to the log file
        /// </summary>
        /// <param name="message">The message to write to the log</param>
        public void WriteToLog(string message)
        {
            // Lock the queue while writing to prevent contention for the log file
            lock (logQueue)
            {
                // Create the entry and push to the Queue
                Log logEntry = new Log(message);
                logQueue.Enqueue(logEntry);

                // If we have reached the Queue Size then flush the Queue
                //if (logQueue.Count >= queueSize || DoPeriodicFlush())
                // {
                FlushLog();
                // }
            }
        }

        public void WriteToLogNow(string message)
        {
            // Lock the queue while writing to prevent contention for the log file
            lock (logQueue)
            {
                // Create the entry and push to the Queue
                Log logEntry = new Log(message);
                logQueue.Enqueue(logEntry);

                FlushLog();
            }
        }

        /// <summary>
        /// Flushes the Queue to the physical log file
        /// </summary>
        private void FlushLog()
        {
            if (Enable == false) return;
            while (logQueue.Count > 0)
            {
                Log entry = logQueue.Dequeue();

                logFile = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

                string logPath = logDir + "\\Logs\\" + logFile;

                if (Directory.Exists(logDir + "\\Logs") == false)
                {
                    Directory.CreateDirectory(logDir + "\\Logs");
                }

                try
                {
                    // This could be optimised to prevent opening and closing the file for each write

                    using (StreamWriter log = new StreamWriter(logPath, true))
                    {
                        log.WriteLine(string.Format("{0}:\t{1}", entry.LogTime, entry.Message));
                    }

                }
                catch (Exception)
                {

                }
            }
        }
    }//end class

    /// <summary>
    /// A Log class to store the message and the Date and Time the log entry was created
    /// </summary>
    public class Log
    {
        public string Message { get; set; }
        public string LogTime { get; set; }
        public string LogDate { get; set; }

        public Log(string message)
        {
            Message = message;
            // LogDate = DateTime.Now.ToString("yyyy-MM-dd");
            LogTime = DateTime.Now.ToString("HH:mm:ss.fff");
        }
    }//end cla
}
