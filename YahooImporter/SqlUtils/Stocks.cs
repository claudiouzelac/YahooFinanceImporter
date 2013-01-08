using SQLite;

namespace SqlUtils
{
    public class Stocks : SQLiteConnection
    {
        public Stocks(string databasePath, bool storeDateTimeAsTicks = false) : base(databasePath, storeDateTimeAsTicks)
        {
        }

        public Stocks(string databasePath, SQLiteOpenFlags openFlags, bool storeDateTimeAsTicks = false) : base(databasePath, openFlags, storeDateTimeAsTicks)
        {
        }
    }
}
