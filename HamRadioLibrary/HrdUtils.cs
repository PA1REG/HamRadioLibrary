using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.IO;
using System.Collections;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Xml;
using System.Runtime.InteropServices;

namespace HrdLibrary
{
    public class HrdUtils
    {
        public static object ConvertToDate(String DateToConvert)
        {
            try
            {
                Int16 sYear = Int16.Parse(DateToConvert.Substring(0, 4));
                Int16 sMonth = Int16.Parse(DateToConvert.Substring(4, 2));
                Int16 sDay = Int16.Parse(DateToConvert.Substring(6, 2));
                DateTime ConvertDate = new DateTime(sYear, sMonth, sDay);
                return ConvertDate;
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }

        public static MySqlConnection HrdLogbookConnection = new MySql.Data.MySqlClient.MySqlConnection();

        public static Boolean ConnectToDatabase(string Server, Int16 Port, string User, string Password, string Database)
        {
            Boolean IsOpen = false;
            int Timeout = 7200;
            //string myconnectstring = "server=192.168.64.200;port=3307;userid=PA1REG;password=dC7K68zUdGx6FKEu;database=PA1REG;";
            string DatabaseConnectString = "server=" + Server + ";port=" + Port + ";userid=" + User + ";password=" + Password + ";database=" + Database + ";connection timeout=" + Timeout + ";";


            //  MySqlConnection Connect = new MySql.Data.MySqlClient.MySqlConnection();
            HrdLogbookConnection.ConnectionString = DatabaseConnectString;
            HrdLogbookConnection.Open();
            NetTimeout();

            if (HrdLogbookConnection.State == System.Data.ConnectionState.Open)
            {
                //Console.WriteLine("Open");
                //Console.WriteLine(HrdLogbookConnection.State.ToString());
                //Console.WriteLine(HrdLogbookConnection.ServerVersion);
                IsOpen = true;
            }
            return IsOpen;
        }

        public static Boolean CheckDatabaseOpen()
        {
            if (HrdLogbookConnection.State == System.Data.ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void DisconnectFromDatabase()
        {
            HrdLogbookConnection.Close();
            HrdLogbookConnection.Dispose();
        }

        public static Boolean ExistsTableHrdContacts()
        {
            Boolean TableIsFound = false;
            string MyDatabase = CurrentDatabase();
            string SqlCommand = @"SELECT table_name 
                                  FROM information_schema.tables 
                                  WHERE table_type = 'BASE TABLE'
                                  AND TABLE_SCHEMA = '" + MyDatabase + @"'
                                  AND TABLE_NAME ='TABLE_HRD_CONTACTS_V01'";

            MySqlCommand Command = new MySqlCommand(SqlCommand, HrdLogbookConnection);
            using (MySqlDataReader reader = Command.ExecuteReader())
            {
                while (reader.Read())
                {
                    // access your record colums by using reader
                    //Console.WriteLine(reader["TABLE_NAME"]);
                    TableIsFound = true;
                }
            }
            return TableIsFound;
        }

        public static void DatabaseInfo()
        {
            Console.WriteLine("MariaDB version   : {0}", HrdLogbookConnection.ServerVersion);
            Console.WriteLine("MariaDB status    : {0}", HrdLogbookConnection.State);
            Console.WriteLine("MariaDB timeout   : {0}", HrdLogbookConnection.ConnectionTimeout);

            string SqlCommand = @"select @@global.net_write_timeout as write_timeout,@@global.net_read_timeout as read_timeout;";

            MySqlCommand Command = new MySqlCommand(SqlCommand, HrdLogbookConnection);
            using (MySqlDataReader reader = Command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine("MariaDB write     : {0}", reader["write_timeout"]);
                    Console.WriteLine("MariaDB read      : {0}", reader["read_timeout"]);
                }
            }

            //string SqlCommand = @"SELECT table_name 
            //                      FROM information_schema.tables 
            //                      WHERE table_type = 'BASE TABLE'
            //                      AND TABLE_SCHEMA = 'PA1REG'
            //                      AND TABLE_NAME ='TABLE_HRD_CONTACTS_V01'";

            //MySqlCommand command = new MySqlCommand(SqlCommand, Connect);
            //using (MySqlDataReader reader = command.ExecuteReader())
            //{
            //    while (reader.Read())
            //    {
            //        // access your record colums by using reader
            //        Console.WriteLine("MariaDB version : {0}",HrdLogbookConnection.ServerVersion);
            //       // Console.WriteLine(HrdLogbookConnection.Site);
            //        Console.WriteLine("MariaDB status  : {0}",HrdLogbookConnection.State);
            //        //Console.WriteLine("Current table   : ",reader["TABLE_NAME"]);
            //    }
            //}
        }

        public static string CurrentDatabase()
        {

            string SqlCommand = @"SELECT DATABASE() as current_database";
            string MyDatabase = "";

            MySqlCommand Command = new MySqlCommand(SqlCommand, HrdLogbookConnection);
            using (MySqlDataReader reader = Command.ExecuteReader())
            {
                while (reader.Read())
                {
                    MyDatabase = reader["current_database"].ToString();
                }
            }
            return MyDatabase;
        }

        public static void OptimizeHrdTable()
        {
            try
            {
                string SqlCommand = "OPTIMIZE TABLE TABLE_HRD_CONTACTS_V01;";
                MySqlCommand Command = new MySqlCommand(SqlCommand, HrdLogbookConnection);
                Command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("opti error");
            }
        }

        public static void AnalyzeHrdTable()
        {
            string SqlCommand = "ANALYZE TABLE TABLE_HRD_CONTACTS_V01;";
            MySqlCommand Command = new MySqlCommand(SqlCommand, HrdLogbookConnection);
            Command.ExecuteNonQuery();
        }
        public static void NetTimeout()
        {
            string SqlCommand = "set @@global.net_write_timeout = 7200;";
            MySqlCommand Command = new MySqlCommand(SqlCommand, HrdLogbookConnection);
            Command.ExecuteNonQuery();
            SqlCommand = "set @@global.net_read_timeout = 7200;";
            Command = new MySqlCommand(SqlCommand, HrdLogbookConnection);
            Command.ExecuteNonQuery();
        }


        public static List<HrdFieldsObjects> HrdFieldsList = new List<HrdFieldsObjects>();

        public static dynamic SearchDatabase(string strCall, string strBand, string strMode, object objDate)
        {


            string SqlCommand = "";
            if (strBand == null && strMode == null)
            {
                if (strCall.IndexOf("*") >= 0)
                {
                    strCall = strCall.Replace("*", "%");
                }
                if (strCall.IndexOf("?") >= 0)
                {
                    strCall = strCall.Replace("?", "_");
                }
                SqlCommand = "SELECT COL_CALL, DATE_FORMAT(COL_TIME_ON, '%Y%m%d') AS COL_QSO_DATE, DATE_FORMAT(COL_TIME_ON, '%H%i%s') AS COL_TIME_ON, DATE_FORMAT(COL_TIME_OFF, '%H%i%s') AS COL_TIME_OFF," +
                             " COL_BAND, COL_MODE, COL_RST_SENT, COL_RST_RCVD, COL_QSL_SENT, COL_QSL_SENT_VIA, COL_GRIDSQUARE, COL_STATION_CALLSIGN, COL_FREQ, COL_CONTEST_ID, COL_OPERATOR, COL_CQZ, COL_STX, COL_SWL" +
                             " FROM TABLE_HRD_CONTACTS_V01" +
                             " WHERE COL_CALL LIKE '" + strCall + "'" +
                             " and COL_SWL = 0";

            }
            else if (strBand == null)
            {
                if (strMode.ToUpper() == "SSB")
                {
                    SqlCommand = "SELECT COL_CALL, DATE_FORMAT(COL_TIME_ON, '%Y%m%d') AS COL_QSO_DATE, DATE_FORMAT(COL_TIME_ON, '%H%i%s') AS COL_TIME_ON, DATE_FORMAT(COL_TIME_OFF, '%H%i%s') AS COL_TIME_OFF," +
                                 " COL_BAND, COL_MODE, COL_RST_SENT, COL_RST_RCVD, COL_QSL_SENT, COL_QSL_SENT_VIA, COL_GRIDSQUARE, COL_STATION_CALLSIGN, COL_FREQ, COL_CONTEST_ID, COL_OPERATOR, COL_CQZ, COL_STX, COL_SWL" +
                                 " FROM TABLE_HRD_CONTACTS_V01" +
                                 " WHERE COL_CALL = '" + strCall + "'" +
                                 " AND (COL_MODE = 'LSB'" +
                                 " OR COL_MODE = 'USB'" +
                                 " OR COL_MODE = 'SSB')" +
                                 " and COL_SWL = 0";
                }
                else
                {

                    SqlCommand = "SELECT COL_CALL, DATE_FORMAT(COL_TIME_ON, '%Y%m%d') AS COL_QSO_DATE, DATE_FORMAT(COL_TIME_ON, '%H%i%s') AS COL_TIME_ON, DATE_FORMAT(COL_TIME_OFF, '%H%i%s') AS COL_TIME_OFF," +
                             " COL_BAND, COL_MODE, COL_RST_SENT, COL_RST_RCVD, COL_QSL_SENT, COL_QSL_SENT_VIA, COL_GRIDSQUARE, COL_STATION_CALLSIGN, COL_FREQ, COL_CONTEST_ID, COL_OPERATOR, COL_CQZ, COL_STX, COL_SWL" +
                             " FROM TABLE_HRD_CONTACTS_V01" +
                             " WHERE COL_CALL = '" + strCall + "'" +
                             " AND COL_MODE = '" + strMode + "'" +
                             " and COL_SWL = 0";
                }
            }
            else if (strMode == null)
            {
                SqlCommand = "SELECT COL_CALL, DATE_FORMAT(COL_TIME_ON, '%Y%m%d') AS COL_QSO_DATE, DATE_FORMAT(COL_TIME_ON, '%H%i%s') AS COL_TIME_ON, DATE_FORMAT(COL_TIME_OFF, '%H%i%s') AS COL_TIME_OFF," +
                             " COL_BAND, COL_MODE, COL_RST_SENT, COL_RST_RCVD, COL_QSL_SENT, COL_QSL_SENT_VIA, COL_GRIDSQUARE, COL_STATION_CALLSIGN, COL_FREQ, COL_CONTEST_ID, COL_OPERATOR, COL_CQZ, COL_STX, COL_SWL" +
                             " FROM TABLE_HRD_CONTACTS_V01" +
                             " WHERE COL_CALL = '" + strCall + "'" +
                             " AND COL_BAND = '" + strBand + "'" +
                             " and COL_SWL = 0";
            }
            else
            {
                if (strMode.ToUpper() == "SSB")
                {
                    SqlCommand = "SELECT COL_CALL, DATE_FORMAT(COL_TIME_ON, '%Y%m%d') AS COL_QSO_DATE, DATE_FORMAT(COL_TIME_ON, '%H%i%s') AS COL_TIME_ON, DATE_FORMAT(COL_TIME_OFF, '%H%i%s') AS COL_TIME_OFF," +
                                 " COL_BAND, COL_MODE, COL_RST_SENT, COL_RST_RCVD, COL_QSL_SENT, COL_QSL_SENT_VIA, COL_GRIDSQUARE, COL_STATION_CALLSIGN, COL_FREQ, COL_CONTEST_ID, COL_OPERATOR, COL_CQZ, COL_STX, COL_SWL" +
                                 " FROM TABLE_HRD_CONTACTS_V01" +
                                 " WHERE COL_CALL = '" + strCall + "'" +
                                 " AND COL_BAND = '" + strBand + "'" +
                                 " AND (COL_MODE = 'LSB'" +
                                 " OR COL_MODE = 'USB'" +
                                 " OR COL_MODE = 'SSB')" +
                                 " and COL_SWL = 0";
                }
                else
                {
                    SqlCommand = "SELECT COL_CALL, DATE_FORMAT(COL_TIME_ON, '%Y%m%d') AS COL_QSO_DATE, DATE_FORMAT(COL_TIME_ON, '%H%i%s') AS COL_TIME_ON, DATE_FORMAT(COL_TIME_OFF, '%H%i%s') AS COL_TIME_OFF," +
                                 " COL_BAND, COL_MODE, COL_RST_SENT, COL_RST_RCVD, COL_QSL_SENT, COL_QSL_SENT_VIA, COL_GRIDSQUARE, COL_STATION_CALLSIGN, COL_FREQ, COL_CONTEST_ID, COL_OPERATOR, COL_CQZ, COL_STX, COL_SWL" +
                                 " FROM TABLE_HRD_CONTACTS_V01" +
                                 " WHERE COL_CALL = '" + strCall + "'" +
                                 " AND COL_BAND = '" + strBand + "'" +
                                 " AND COL_MODE = '" + strMode + "'" +
                                 " and COL_SWL = 0";

                }
            }

            string DateSql = "1900-01-01";
            if (objDate != null && objDate.ToString() != string.Empty)
            {
                {
                    DateTime postingDate = Convert.ToDateTime(objDate);
                    DateSql = postingDate.ToString("yyyy-MM-dd");
                    SqlCommand += " and DATE(COL_TIME_ON) = '" + DateSql + "'";
                }

            }

            MySqlCommand Command = new MySqlCommand(SqlCommand, HrdLogbookConnection);
            using (MySqlDataReader reader = Command.ExecuteReader())
            {
                Boolean FoundCall = false;
                HrdFieldsList.Clear();
                while (reader.Read())
                {
                    // access your record colums by using reader
                    //Console.WriteLine(reader["COL_CALL"]);
                    //HrdFields.CALL = reader["COL_CALL"].ToString();
                    //HrdFields.BAND = reader["COL_BAND"].ToString();
                    //HrdFields.CONTEST_ID = reader["COL_CONTEST_ID"].ToString();
                    //HrdFields.CQZ = reader["COL_CQZ"].ToString();
                    //HrdFields.FREQ = reader["COL_FREQ"].ToString();
                    //HrdFields.GRIDSQUARE = reader["COL_GRIDSQUARE"].ToString();
                    //HrdFields.MODE = reader["COL_MODE"].ToString();
                    //HrdFields.OPERATOR = reader["COL_OPERATOR"].ToString();
                    //HrdFields.QSL_SENT = reader["COL_QSL_SENT"].ToString();
                    //HrdFields.QSL_SENT_VIA = reader["COL_QSL_SENT_VIA"].ToString();
                    //HrdFields.RST_RCVD = reader["COL_RST_RCVD"].ToString();
                    //HrdFields.RST_SENT = reader["COL_RST_SENT"].ToString();
                    //HrdFields.STATION_CALLSIGN = reader["COL_STATION_CALLSIGN"].ToString();
                    //HrdFields.STX = reader["COL_STX"].ToString();
                    //HrdFields.SWL = reader["COL_SWL"].ToString();
                    //HrdFields.QSO_DATE = reader["COL_QSO_DATE"].ToString();
                    //HrdFields.TIME_ON = reader["COL_TIME_ON"].ToString();
                    //HrdFields.TIME_OFF = reader["COL_TIME_OFF"].ToString();

                    FoundCall = true;
                    HrdFieldsObjects FieldList = new HrdFieldsObjects();
                    FieldList.CALL = reader["COL_CALL"].ToString();
                    FieldList.BAND = reader["COL_BAND"].ToString();
                    FieldList.CONTEST_ID = reader["COL_CONTEST_ID"].ToString();
                    FieldList.CQZ = reader["COL_CQZ"].ToString();
                    FieldList.FREQ = reader["COL_FREQ"].ToString();
                    FieldList.GRIDSQUARE = reader["COL_GRIDSQUARE"].ToString();
                    FieldList.MODE = reader["COL_MODE"].ToString();
                    FieldList.OPERATOR = reader["COL_OPERATOR"].ToString();
                    FieldList.QSL_SENT = reader["COL_QSL_SENT"].ToString();
                    FieldList.QSL_SENT_VIA = reader["COL_QSL_SENT_VIA"].ToString();
                    FieldList.RST_RCVD = reader["COL_RST_RCVD"].ToString();
                    FieldList.RST_SENT = reader["COL_RST_SENT"].ToString();
                    FieldList.STATION_CALLSIGN = reader["COL_STATION_CALLSIGN"].ToString();
                    FieldList.STX = reader["COL_STX"].ToString();
                    FieldList.SWL = reader["COL_SWL"].ToString();
                    FieldList.QSO_DATE = reader["COL_QSO_DATE"].ToString();
                    FieldList.TIME_ON = reader["COL_TIME_ON"].ToString();
                    FieldList.TIME_OFF = reader["COL_TIME_OFF"].ToString();
                    HrdFieldsList.Add(FieldList);
                }
                return FoundCall;
            }
        }

        public static List<HrdIndexObjects> HrdIndexList = new List<HrdIndexObjects>();

        public static void IndexDatabaseStatistics()
        {
            //table.Dispose();
            //table.Columns.Add(new DataColumn("index_name", typeof(char)));
            //table.Columns.Add(new DataColumn("index_columns", typeof(char)));
            //table.Columns.Add(new DataColumn("CARDINALITY", typeof(char)));
            string MyDatabase = CurrentDatabase();
            string SqlCommand = @"select index_name,
                                  group_concat(column_name order by seq_in_index) as index_columns,
                                  CARDINALITY
                                  from information_schema.statistics
                                  where table_schema not in ('information_schema', 'mysql',
                                  'performance_schema', 'sys')
                                  and      table_schema = ('" + MyDatabase + @"')   
                                  and      table_name = 'TABLE_HRD_CONTACTS_V01'
                                  group by index_schema,
                                  index_name,
                                  index_type,
                                  non_unique,
                                  table_name
                                  order by index_schema,
                                  index_name";
            MySqlCommand Command = new MySqlCommand(SqlCommand, HrdLogbookConnection);
            using (MySqlDataReader reader = Command.ExecuteReader())
            {
                HrdIndexList.Clear();
                while (reader.Read())
                {
                    HrdIndexObjects IndexList = new HrdIndexObjects();
                    IndexList.INDEX_NAME = reader["index_name"].ToString();
                    IndexList.INDEX_COLUMNS = reader["index_columns"].ToString();
                    //   IndexList.CARDINALITY = reader["CARDINALITY"].ToString();
                    IndexList.CARDINALITY = Int32.Parse(reader["cardinality"].ToString());
                    HrdIndexList.Add(IndexList);

                }
            }
        }

        public static List<ReportModeObjects> HrdModeList = new List<ReportModeObjects>();

        public static void ReportMode()
        {
            string MyDatabase = CurrentDatabase();
            string SqlCommand = @"select COL_MODE as MODE, COL_SUBMODE as SUBMODE, count(COL_MODE) as WORKED
                                  from `TABLE_HRD_CONTACTS_V01`
                                  where COL_SWL = 0
                                  group by COL_MODE, COL_SUBMODE
                                  order by COL_MODE;";
            MySqlCommand Command = new MySqlCommand(SqlCommand, HrdLogbookConnection);
            using (MySqlDataReader reader = Command.ExecuteReader())
            {
                HrdModeList.Clear();
                while (reader.Read())
                {
                    ReportModeObjects ModeList = new ReportModeObjects();
                    ModeList.MODE = reader["mode"].ToString();
                    ModeList.SUBMODE = reader["submode"].ToString();
                    ModeList.WORKED = reader["worked"].ToString();
                    HrdModeList.Add(ModeList);
                }
            }
        }

        public static List<ReportBandObjects> HrdBandList = new List<ReportBandObjects>();

        public static void ReportBand()
        {
            string MyDatabase = CurrentDatabase();
            string SqlCommand = @"select COL_BAND as BAND, count(COL_BAND) as WORKED
                                  from `TABLE_HRD_CONTACTS_V01`
                                  where COL_SWL = 0
                                  group by COL_BAND
                                  order by regexp_replace (COL_BAND, '\\d', ''),
                                  cast(regexp_replace (COL_BAND, '\\D', '') as INTEGER);";
            MySqlCommand Command = new MySqlCommand(SqlCommand, HrdLogbookConnection);
            using (MySqlDataReader reader = Command.ExecuteReader())
            {
                HrdBandList.Clear();
                while (reader.Read())
                {
                    ReportBandObjects BandList = new ReportBandObjects();
                    BandList.BAND = reader["band"].ToString();
                    BandList.WORKED = reader["worked"].ToString();
                    HrdBandList.Add(BandList);
                }
            }
        }


        public static List<ReportBandModeObjects> HrdBandModeList = new List<ReportBandModeObjects>();

        public static void ReportModeBand()
        {
            string MyDatabase = CurrentDatabase();
            string SqlCommand = @"select COL_BAND as BAND,COL_MODE as MODE, COL_SUBMODE as SUBMODE, count(COL_MODE) as WORKED
                                  from `TABLE_HRD_CONTACTS_V01`
                                  where COL_SWL = 0
                                  group by COL_BAND, COL_MODE, COL_SUBMODE
                                  order by regexp_replace (COL_BAND, '\\d', ''),
                                  cast(regexp_replace (COL_BAND, '\\D', '') as INTEGER),
                                  COL_MODE, COL_SUBMODE;";
            MySqlCommand Command = new MySqlCommand(SqlCommand, HrdLogbookConnection);
            using (MySqlDataReader reader = Command.ExecuteReader())
            {
                HrdBandModeList.Clear();
                while (reader.Read())
                {
                    ReportBandModeObjects BandModeList = new ReportBandModeObjects();
                    BandModeList.BAND = reader["band"].ToString();
                    BandModeList.MODE = reader["mode"].ToString();
                    BandModeList.SUBMODE = reader["submode"].ToString();
                    BandModeList.WORKED = reader["worked"].ToString();
                    HrdBandModeList.Add(BandModeList);
                }
            }
        }

        public static List<ReportYearBandModeObjects> HrdYearBandModeList = new List<ReportYearBandModeObjects>();

        // public static void ReportYearBandMode()
        public static void ReportYearBandMode([Optional] object StartDate, [Optional] object EndDate)
        {
            //object StartDate = null;
            //object EndDate = null;
            string StartDateSql = "1900-01-01";
            if (StartDate != null && StartDate.ToString() != string.Empty)
            {
                DateTime postingDate = Convert.ToDateTime(StartDate);
                //StartDateSql = string.Format("yyyy-MM-dd", postingDate);
                StartDateSql = postingDate.ToString("yyyy-MM-dd");
            }
            string EndDateSql = DateTime.Now.ToString("yyyy-MM-dd");
            if (EndDate != null && EndDate.ToString() != string.Empty)
            {
                DateTime postingDate = Convert.ToDateTime(EndDate);
                // EndDateSql = string.Format("yyyy-MM-dd", postingDate);
                EndDateSql = postingDate.ToString("yyyy-MM-dd");
            }

            // todo afmaken betwen datum selectie
            string MyDatabase = CurrentDatabase();
            string SqlCommand = @"select DATE_FORMAT(COL_TIME_ON, '%Y') as YEAR, COL_BAND as BAND,COL_MODE as MODE, COL_SUBMODE as SUBMODE, count(COL_MODE) as WORKED
                                  from `TABLE_HRD_CONTACTS_V01`
                                  where COL_SWL = 0
                                  and DATE(COL_TIME_ON) BETWEEN '" + StartDateSql + @"' AND '" + EndDateSql + @"' 
                                  group by DATE_FORMAT(COL_TIME_ON, '%Y'), COL_BAND, COL_MODE, COL_SUBMODE
                                  order by DATE_FORMAT(COL_TIME_ON, '%Y'), regexp_replace (COL_BAND, '\\d', ''),
                                  cast(regexp_replace (COL_BAND, '\\D', '') as INTEGER),
                                  COL_MODE, COL_SUBMODE";
            MySqlCommand Command = new MySqlCommand(SqlCommand, HrdLogbookConnection);
            using (MySqlDataReader reader = Command.ExecuteReader())
            {
                HrdYearBandModeList.Clear();
                while (reader.Read())
                {
                    ReportYearBandModeObjects YearBandModeList = new ReportYearBandModeObjects();
                    YearBandModeList.YEAR = reader["year"].ToString();
                    YearBandModeList.BAND = reader["band"].ToString();
                    YearBandModeList.MODE = reader["mode"].ToString();
                    YearBandModeList.SUBMODE = reader["submode"].ToString();
                    YearBandModeList.WORKED = reader["worked"].ToString();
                    HrdYearBandModeList.Add(YearBandModeList);
                }
            }
        }

        public static List<ReportQsoObjects> HrdQsoList = new List<ReportQsoObjects>();

        public static void ReportQso()
        {
            string MyDatabase = CurrentDatabase();
            string SqlCommand = @"select count(*) as WORKED,
                                  (select count(*) as SWL from `TABLE_HRD_CONTACTS_V01` where col_swl=1) as SWL,
                                  (select count(*) as OM from `TABLE_HRD_CONTACTS_V01` where col_swl=0) as OM
                                  from `TABLE_HRD_CONTACTS_V01`
                                  where COL_SWL = 0;";
            //string SqlCommand = @"select count(*) as QSO,
            //                      (select count(*) from `TABLE_HRD_CONTACTS_V01` where col_swl=1) as SWL,
            //                      (select count(*) from `TABLE_HRD_CONTACTS_V01` where col_swl=0) as OM
            //                      from `TABLE_HRD_CONTACTS_V01`;";
            MySqlCommand Command = new MySqlCommand(SqlCommand, HrdLogbookConnection);
            using (MySqlDataReader reader = Command.ExecuteReader())
            {
                HrdQsoList.Clear();
                while (reader.Read())
                {
                    ReportQsoObjects QsoList = new ReportQsoObjects();
                    QsoList.WORKED = reader["worked"].ToString();
                    QsoList.OM = reader["om"].ToString();
                    QsoList.SWL = reader["swl"].ToString();
                    HrdQsoList.Add(QsoList);
                }
            }
        }

        public static List<ReportQsoByYearObjects> HrdQsoByYearList = new List<ReportQsoByYearObjects>();

        public static void ReportQsoPerYear()
        {
            string MyDatabase = CurrentDatabase();
            string SqlCommand = @"SELECT DATE_FORMAT(COL_TIME_ON,'%Y') AS YEAR,
                                  COUNT(COL_CALL) AS WORKED,
                                  ROUND( COUNT(COL_CALL) / (SELECT COUNT(*) as PERCENT from `TABLE_HRD_CONTACTS_V01`) * 100,2) AS PERCENT
                                  FROM `TABLE_HRD_CONTACTS_V01` 
                                  where COL_SWL = 0
                                  GROUP BY DATE_FORMAT(COL_TIME_ON, '%Y')
                                  ORDER BY DATE_FORMAT(COL_TIME_ON, '%Y')";
            MySqlCommand Command = new MySqlCommand(SqlCommand, HrdLogbookConnection);
            using (MySqlDataReader reader = Command.ExecuteReader())
            {
                HrdQsoByYearList.Clear();
                while (reader.Read())
                {
                    ReportQsoByYearObjects QsoPerYearList = new ReportQsoByYearObjects();
                    QsoPerYearList.YEAR = reader["year"].ToString();
                    QsoPerYearList.WORKED = reader["worked"].ToString();
                    QsoPerYearList.PERCENT = reader["percent"].ToString();
                    HrdQsoByYearList.Add(QsoPerYearList);
                }
            }
        }

        //        SELECT COL_DXCC,count(*) AS Total
        //        FROM `TABLE_HRD_CONTACTS_V01` 
        //group by COL_DXCC

        public static List<DxccObjects> HrdDxccList = new List<DxccObjects>();

        public static Boolean ReadCountryList()
        {
            string filename = @"C:\Users\pa1re\AppData\Roaming\HRDLLC\HRD Logbook\LogbookCountryDataEx.xml";
            XmlTextReader reader = new XmlTextReader(filename);
            HrdDxccList.Clear();

            while (reader.Read())
            {
                Console.WriteLine("---> {0}", reader.NodeType);
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        Console.Write("<" + reader.Name);
                        Console.WriteLine(">");
                        Console.WriteLine("no el   : {0}", reader.AttributeCount);
                        Console.WriteLine("Country : {0}", reader.GetAttribute("Country"));
                        Console.WriteLine("DXCC    : {0}", reader.GetAttribute("DXCC"));
                        DxccObjects DxccList = new DxccObjects();
                        DxccList.DXCC = reader["dxcc"].ToString();
                        DxccList.COUNTRY = reader["country"].ToString();
                        HrdDxccList.Add(DxccList);

                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        Console.WriteLine(reader.Value);
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        Console.Write("</" + reader.Name);
                        Console.WriteLine(">");
                        break;
                }
            }
            Console.ReadLine();
            return true;
        }

        public static HrdFieldsObjects HrdFields = new HrdFieldsObjects();
        public class HrdFieldsObjects
        {
            public string CALL { get; set; }
            public string QSO_DATE { get; set; }
            public string TIME_ON { get; set; }
            public string TIME_OFF { get; set; }
            public string BAND { get; set; }
            public string FREQ { get; set; }
            public string MODE { get; set; }
            public string RST_SENT { get; set; }
            public string RST_RCVD { get; set; }
            public string QSL_SENT { get; set; }
            public string QSL_SENT_VIA { get; set; }
            public string GRIDSQUARE { get; set; }
            public string STATION_CALLSIGN { get; set; }
            public string CONTEST_ID { get; set; }
            public string OPERATOR { get; set; }
            public string CQZ { get; set; }
            public string STX { get; set; }
            public string SWL { get; set; }
        }

        public static HrdIndexObjects HrdIndexen = new HrdIndexObjects();
        public class HrdIndexObjects
        {
            public string INDEX_NAME { get; set; }
            public string INDEX_COLUMNS { get; set; }
            public Int32 CARDINALITY { get; set; }
        }

        public static ReportBandObjects HrdReportBand = new ReportBandObjects();
        public class ReportBandObjects
        {
            public string BAND { get; set; }
            public string WORKED { get; set; }
        }

        public static ReportModeObjects HrdReportMode = new ReportModeObjects();
        public class ReportModeObjects
        {
            public string MODE { get; set; }
            public string SUBMODE { get; set; }
            public string WORKED { get; set; }
        }

        public static ReportBandModeObjects HrdReportBandMode = new ReportBandModeObjects();
        public class ReportBandModeObjects
        {
            public string BAND { get; set; }
            public string MODE { get; set; }
            public string SUBMODE { get; set; }
            public string WORKED { get; set; }
        }

        public static ReportYearBandModeObjects HrdReportYearBandMode = new ReportYearBandModeObjects();
        public class ReportYearBandModeObjects
        {
            public string YEAR { get; set; }
            public string BAND { get; set; }
            public string MODE { get; set; }
            public string SUBMODE { get; set; }
            public string WORKED { get; set; }
        }

        public static ReportQsoObjects HrdReportQso = new ReportQsoObjects();
        public class ReportQsoObjects
        {
            public string WORKED { get; set; }
            public string OM { get; set; }
            public string SWL { get; set; }
        }

        public static DxccObjects HrdDxcc = new DxccObjects();
        public class DxccObjects
        {
            public string DXCC { get; set; }
            public string COUNTRY { get; set; }
        }
        public class ReportQsoByYearObjects
        {
            public string YEAR { get; set; }
            public string WORKED { get; set; }
            public string PERCENT { get; set; }
        }
    }
}
