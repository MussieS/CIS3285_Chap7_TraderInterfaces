
using System;
using System.IO;
using System.Xml.Linq;
using Unit8_SRP_F24.Contracts;

namespace Unit8_SRP_F24;

public class ConsoleLogger : ILogger
{
    public void LogMessage(string message, params object[] args)
    {
        Console.WriteLine(message, args);

        string formattedMessage = string.Format(message, args);

        string type = "INFO";

        if (formattedMessage.StartsWith("WARN"))
        {
            type = "WARN";
        }
        else if (formattedMessage.StartsWith("ERROR"))
        {
            type = "ERROR";
        }

        XElement log = new XElement("log",
            new XElement("type", type),
            new XElement("message", formattedMessage)
        );

        File.AppendAllText("tradelog.xml", log.ToString() + Environment.NewLine);
    }
}
