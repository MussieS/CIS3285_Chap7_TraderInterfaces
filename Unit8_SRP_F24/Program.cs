
using System.Reflection;
using Unit8_SRP_F24.Contracts;

namespace Unit8_SRP_F24;


class Program
{
    static void Main(string[] args)
    {
        string tradeURL = "https://raw.githubusercontent.com/tgibbons-css/CIS3285_Unit9_F24/refs/heads/master/SingleResponsibilityPrinciple/trades.txt";

        ILogger logger = new ConsoleLogger();
        ITradeValidator tradeValidator = new SimpleTradeValidator(logger);
        ITradeDataProvider tradeDataProvider = new URLTradeDataProvider(tradeURL, logger);
        ITradeMapper tradeMapper = new SimpleTradeMapper();
        ITradeParser tradeParser = new SimpleTradeParser(tradeValidator, tradeMapper);
        ITradeStorage tradeStorage = new AdoNetTradeStorage(logger);

        var tradeProcessor = new TradeProcessor(tradeDataProvider, tradeParser, tradeStorage);
        tradeProcessor.ProcessTrades();

        //Console.ReadKey();
    }
}
