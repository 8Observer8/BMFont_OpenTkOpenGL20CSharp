using System;
using System.IO;

public class Logger
{
    private string _logFileName = "info.txt";
    private static Logger _instance;
    private Logger() { }

    public static Logger Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Logger();
                _instance.Clear();
            }
            return _instance;
        }
    }

    /// <summary>
    /// Write a message to a console and to a log file
    /// </summary>
    /// <param name="message">a message that will print to a log file and to the console</param>
    public void Print(string message)
    {
        Console.WriteLine(message);
        File.AppendAllText(_logFileName, message + Environment.NewLine);
    }

    // Clear a file
    public void Clear()
    {
        if (File.Exists(_logFileName))
        {
            File.WriteAllText(_logFileName, "");
        }
    }
}