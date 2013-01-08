using System.Collections.Generic;

namespace YahooImporter
{
    internal sealed class YahooTags
    {
        public static readonly YahooTags BookValue;
        public static readonly YahooTags Change;
//        public static readonly YahooTags AfterHoursChange;
        public static readonly YahooTags TradeDate;
        public static readonly YahooTags EpsEstimateCurrentYear;
        public static readonly YahooTags FloatShares;
        public static readonly YahooTags FiftyTwoWeekLow;
//        public static readonly YahooTags AnnualizedGain;
        public static readonly YahooTags MarketCap;
//        public static readonly YahooTags ChangeFromFiftyTwoWkLow;
//        public static readonly YahooTags ChangePercent;
//        public static readonly YahooTags PercentChangeFromFiftyTwoWeekHigh;
        public static readonly YahooTags HighLimit;
        public static readonly YahooTags DaysRange;
//        public static readonly YahooTags ChangeFromTwoHundredDayMovingAverage;
//        public static readonly YahooTags PercentChangeFromFiftyDayMovingAverage;
        public static readonly YahooTags Open;
//        public static readonly YahooTags ChangeInPercent;
        public static readonly YahooTags ExDividendDate;
        public static readonly YahooTags PeRatio;
        public static readonly YahooTags PriceOverEpsEstimateNexyYr;
        public static readonly YahooTags ShortRatio;
        public static readonly YahooTags TickerTrend;
//        public static readonly YahooTags HoldingsValue;
        public static readonly YahooTags DaysValueChange;
        public static readonly YahooTags DividendYield;
        public static readonly YahooTags AverageDailyVolume;
//        public static readonly YahooTags BidSize;
        public static readonly YahooTags DividendPerShare;
        public static readonly YahooTags EarningsPerShare;
        public static readonly YahooTags EpsEstimateNextYear;
        public static readonly YahooTags DaysLow;
//        public static readonly YahooTags FiftyTwoWeekHigh;
//        public static readonly YahooTags HoldingGain;
//        public static readonly YahooTags MoreInfo;
//        public static readonly YahooTags PercentChangeFromFiftyTwoWeekLow;
        public static readonly YahooTags LastTradeSize;
        public static readonly YahooTags LastTradeAndTime;
        public static readonly YahooTags LowLimit;
        public static readonly YahooTags FiftyDayMovingAverage;
//        public static readonly YahooTags PercentChangeFromTwoHundredDayMovingAverage;
        public static readonly YahooTags Name;
        public static readonly YahooTags PreviousClose;
        public static readonly YahooTags PriceOverSales;
        public static readonly YahooTags PegRatio;
        public static readonly YahooTags Symbol;
        public static readonly YahooTags LastTradeTime;
        public static readonly YahooTags OneYearTargetPrice;
        public static readonly YahooTags ChangeAndPercentChange;
        public static readonly YahooTags LastTradeDate;
        public static readonly YahooTags ErrorIndication;
        public static readonly YahooTags EpsEstimateNextQuarter;
        public static readonly YahooTags DaysHigh;
        public static readonly YahooTags Ebitda;
//        public static readonly YahooTags ChangeFromFiftyTwoWeekhigh;
//        public static readonly YahooTags Range;
        public static readonly YahooTags TwoHundreadDayMovingAverage;
//        public static readonly YahooTags ChangeFrom50DayMovingAverage;
//        public static readonly YahooTags Notes;
        public static readonly YahooTags PriceOverBook;
        public static readonly YahooTags DividendPayDate;
//        public static readonly YahooTags PriceOverEpsEstimateCurrentYear;
//        public static readonly YahooTags PriceOverEpsEstimateCurrentYearVolume;
        public static readonly YahooTags FiftyTwoWeekRange;
        public static readonly YahooTags StockExchange;
        public static readonly YahooTags Volume;
        public static readonly List<YahooTags> TagsByName;

        static YahooTags()
        {
            TagsByName = new List<YahooTags>();
            Name = new YahooTags("Name", "n");
            BookValue = new YahooTags("BookValue", "b4");
            Change = new YahooTags("Change", "c1");
            EpsEstimateCurrentYear = new YahooTags("EPSEstimateCurrentYear", "e7");
            //            FiftyTwoWeekLow = new YahooTags("52WeekLow","j");
            MarketCap = new YahooTags("MarketCapitalization", "j1");
//            ChangeFromFiftyTwoWkLow = new YahooTags("Change from 52 Week Low","j5");
//            ChangePercent = new YahooTags("Change %","k2");
//            PercentChangeFromFiftyTwoWeekHigh = new YahooTags("% Change from 52 Week High","k5");
//            HighLimit = new YahooTags("HighLimit","l2");
            DaysRange = new YahooTags("DaysRange", "m");
//            ChangeFromTwoHundredDayMovingAverage = new YahooTags("Change from 200 Day Moving Average","m5");
//            PercentChangeFromFiftyDayMovingAverage = new YahooTags("% Change from 50 Day Moving Average","m8");
            Open = new YahooTags("Open", "o");
//            ChangeInPercent = new YahooTags("% Change","p2");
            ExDividendDate = new YahooTags("ExDividendDate", "q");
            PeRatio = new YahooTags("PE Ratio", "r");
//            PriceOverEpsEstimateNexyYr = new YahooTags("PEPSEstimateNextYear","r7");
            ShortRatio = new YahooTags("ShortRatio", "s7");
//            TickerTrend = new YahooTags("TickerTrend","t7");
//            DaysValueChange = new YahooTags("DaysValueRange","w1");
            DividendYield = new YahooTags("DividendYield", "y");
            AverageDailyVolume = new YahooTags("AverageDailyVolume", "a2");
//            BidSize = new YahooTags("Bid Size","b6");
            DividendPerShare = new YahooTags("DividendPerShare", "d");
            EarningsPerShare = new YahooTags("EarningsPerShare", "e");
//            EpsEstimateNextYear = new YahooTags("EPSEstimateNextYear","e8");
            DaysLow = new YahooTags("DaysLow", "g");
//            MoreInfo = new YahooTags("More Info","i");
//            PercentChangeFromFiftyTwoWeekLow = new YahooTags("% Change From 52 Week Low","j6");
//            LastTradeSize = new YahooTags("LastTradeSize","k3");
//            LastTradeAndTime = new YahooTags("LastTradeAndTime","l");
//            LowLimit = new YahooTags("LowLimit","l3");
            FiftyDayMovingAverage = new YahooTags("FiftyDayMovingAverage", "m3");
//            PercentChangeFromTwoHundredDayMovingAverage = new YahooTags("% Change from 200 Day Moving Average","m6");
            PreviousClose = new YahooTags("PreviousClose", "p");
            PriceOverSales = new YahooTags("PricePerSales", "p5");
            PegRatio = new YahooTags("PEGRatio", "r6");
//            LastTradeTime = new YahooTags("LastTradeTime","t1");
            OneYearTargetPrice = new YahooTags("OneYearTargetPrice", "t8");
            ChangeAndPercentChange = new YahooTags("ChangeAndPercentChange", "c");
//            LastTradeDate = new YahooTags("LastTradeDate","d1");
//            ErrorIndication = new YahooTags("ErrorIndication","e1");
            EpsEstimateNextQuarter = new YahooTags("EPSEstimateNextQuarter", "e9");
            DaysHigh = new YahooTags("DaysHigh", "h");
            Ebitda = new YahooTags("EBITDA", "j4");
//            ChangeFromFiftyTwoWeekhigh = new YahooTags("Change from 52 Week High","k4");
//            DaysRange = new YahooTags("DaysRange","m");
            TwoHundreadDayMovingAverage = new YahooTags("200DayMovingAverage", "m4");
            PriceOverBook = new YahooTags("PricePerBook", "p6");
            DividendPayDate = new YahooTags("DividendPayDate", "r1");
            Volume = new YahooTags("Volume", "v");
            FiftyTwoWeekRange = new YahooTags("52WeekRange", "w");
            StockExchange = new YahooTags("StockExchange", "x");
        }

        public YahooTags(string columnName, string tag)
        {
            Tag = tag;
            ColumnName = columnName;
            TagsByName.Add(this);
        }

        public string Tag { get; private set; }

        public string ColumnName { get; private set; }
    }
}