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
using HamRadioDeluxeProperties;
using System.Diagnostics;
using HamRadioLibrary.Common;

namespace HamRadioDeluxeDatabaseLibrary
{
    public class HamRadioDeluxeDatabase
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
                Command.CommandTimeout = 3600;
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
            Command.CommandTimeout = 3600;
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
            //mysqlCommand.CommandTimeout = 600;
            //SqlCommand = "set @@global.CommandTimeout = 7200;";
            //Command = new MySqlCommand(SqlCommand, HrdLogbookConnection);
            //Command.ExecuteNonQuery();
        }


        public static List<HRDProperties.HrdFieldsObjects> HrdFieldsList = new List<HRDProperties.HrdFieldsObjects>();

        public static dynamic SearchDatabase(string strCall, string strBand, string strMode, object objDate, bool strSwl)
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
                             " COL_BAND, COL_MODE, COL_RST_SENT, COL_RST_RCVD, COL_QSL_SENT, COL_QSL_SENT_VIA, COL_GRIDSQUARE, COL_STATION_CALLSIGN, COL_FREQ, COL_CONTEST_ID, COL_OPERATOR, COL_CQZ, COL_STX, COL_SWL, COL_PRIMARY_KEY" +
                             " FROM TABLE_HRD_CONTACTS_V01" +
                             " WHERE COL_CALL LIKE '" + strCall + "'";

            }
            else if (strBand == null)
            {
                if (strMode.ToUpper() == "SSB")
                {
                    SqlCommand = "SELECT COL_CALL, DATE_FORMAT(COL_TIME_ON, '%Y%m%d') AS COL_QSO_DATE, DATE_FORMAT(COL_TIME_ON, '%H%i%s') AS COL_TIME_ON, DATE_FORMAT(COL_TIME_OFF, '%H%i%s') AS COL_TIME_OFF," +
                                 " COL_BAND, COL_MODE, COL_RST_SENT, COL_RST_RCVD, COL_QSL_SENT, COL_QSL_SENT_VIA, COL_GRIDSQUARE, COL_STATION_CALLSIGN, COL_FREQ, COL_CONTEST_ID, COL_OPERATOR, COL_CQZ, COL_STX, COL_SWL, COL_PRIMARY_KEY" +
                                 " FROM TABLE_HRD_CONTACTS_V01" +
                                 " WHERE COL_CALL = '" + strCall + "'" +
                                 " AND COL_MODE IN ('LSB', 'USB', 'SSB')";
                    //SqlCommand = "SELECT COL_CALL, DATE_FORMAT(COL_TIME_ON, '%Y%m%d') AS COL_QSO_DATE, DATE_FORMAT(COL_TIME_ON, '%H%i%s') AS COL_TIME_ON, DATE_FORMAT(COL_TIME_OFF, '%H%i%s') AS COL_TIME_OFF," +
                    //             " COL_BAND, COL_MODE, COL_RST_SENT, COL_RST_RCVD, COL_QSL_SENT, COL_QSL_SENT_VIA, COL_GRIDSQUARE, COL_STATION_CALLSIGN, COL_FREQ, COL_CONTEST_ID, COL_OPERATOR, COL_CQZ, COL_STX, COL_SWL" +
                    //             " FROM TABLE_HRD_CONTACTS_V01" +
                    //             " WHERE COL_CALL = '" + strCall + "'" +
                    //             " AND (COL_MODE = 'LSB'" +
                    //             " OR COL_MODE = 'USB'" +
                    //             " OR COL_MODE = 'SSB')" +
                    //             " and COL_SWL = 0";
                }
                else
                {

                    SqlCommand = "SELECT COL_CALL, DATE_FORMAT(COL_TIME_ON, '%Y%m%d') AS COL_QSO_DATE, DATE_FORMAT(COL_TIME_ON, '%H%i%s') AS COL_TIME_ON, DATE_FORMAT(COL_TIME_OFF, '%H%i%s') AS COL_TIME_OFF," +
                             " COL_BAND, COL_MODE, COL_RST_SENT, COL_RST_RCVD, COL_QSL_SENT, COL_QSL_SENT_VIA, COL_GRIDSQUARE, COL_STATION_CALLSIGN, COL_FREQ, COL_CONTEST_ID, COL_OPERATOR, COL_CQZ, COL_STX, COL_SWL, COL_PRIMARY_KEY" +
                             " FROM TABLE_HRD_CONTACTS_V01" +
                             " WHERE COL_CALL = '" + strCall + "'" +
                             " AND COL_MODE = '" + strMode + "'";
                }
            }
            else if (strMode == null)
            {
                SqlCommand = "SELECT COL_CALL, DATE_FORMAT(COL_TIME_ON, '%Y%m%d') AS COL_QSO_DATE, DATE_FORMAT(COL_TIME_ON, '%H%i%s') AS COL_TIME_ON, DATE_FORMAT(COL_TIME_OFF, '%H%i%s') AS COL_TIME_OFF," +
                             " COL_BAND, COL_MODE, COL_RST_SENT, COL_RST_RCVD, COL_QSL_SENT, COL_QSL_SENT_VIA, COL_GRIDSQUARE, COL_STATION_CALLSIGN, COL_FREQ, COL_CONTEST_ID, COL_OPERATOR, COL_CQZ, COL_STX, COL_SWL, COL_PRIMARY_KEY" +
                             " FROM TABLE_HRD_CONTACTS_V01" +
                             " WHERE COL_CALL = '" + strCall + "'" +
                             " AND COL_BAND = '" + strBand + "'";
            }
            else
            {
                if (strMode.ToUpper() == "SSB")
                {
                    // todo or --> in
                    SqlCommand = "SELECT COL_CALL, DATE_FORMAT(COL_TIME_ON, '%Y%m%d') AS COL_QSO_DATE, DATE_FORMAT(COL_TIME_ON, '%H%i%s') AS COL_TIME_ON, DATE_FORMAT(COL_TIME_OFF, '%H%i%s') AS COL_TIME_OFF," +
                                 " COL_BAND, COL_MODE, COL_RST_SENT, COL_RST_RCVD, COL_QSL_SENT, COL_QSL_SENT_VIA, COL_GRIDSQUARE, COL_STATION_CALLSIGN, COL_FREQ, COL_CONTEST_ID, COL_OPERATOR, COL_CQZ, COL_STX, COL_SWL, COL_PRIMARY_KEY" +
                                 " FROM TABLE_HRD_CONTACTS_V01" +
                                 " WHERE COL_CALL = '" + strCall + "'" +
                                 " AND COL_BAND = '" + strBand + "'" +
                                 " AND COL_MODE IN ('LSB', 'USB', 'SSB')";
                }
                //    SqlCommand = "SELECT COL_CALL, DATE_FORMAT(COL_TIME_ON, '%Y%m%d') AS COL_QSO_DATE, DATE_FORMAT(COL_TIME_ON, '%H%i%s') AS COL_TIME_ON, DATE_FORMAT(COL_TIME_OFF, '%H%i%s') AS COL_TIME_OFF," +
                //            " COL_BAND, COL_MODE, COL_RST_SENT, COL_RST_RCVD, COL_QSL_SENT, COL_QSL_SENT_VIA, COL_GRIDSQUARE, COL_STATION_CALLSIGN, COL_FREQ, COL_CONTEST_ID, COL_OPERATOR, COL_CQZ, COL_STX, COL_SWL" +
                //            " FROM TABLE_HRD_CONTACTS_V01" +
                //            " WHERE COL_CALL = '" + strCall + "'" +
                //            " AND COL_BAND = '" + strBand + "'" +
                //            " AND (COL_MODE = 'LSB'" +
                //            " OR COL_MODE = 'USB'" +
                //            " OR COL_MODE = 'SSB')" +
                //            " and COL_SWL = 0";
                //}
                else
                {
                    SqlCommand = "SELECT COL_CALL, DATE_FORMAT(COL_TIME_ON, '%Y%m%d') AS COL_QSO_DATE, DATE_FORMAT(COL_TIME_ON, '%H%i%s') AS COL_TIME_ON, DATE_FORMAT(COL_TIME_OFF, '%H%i%s') AS COL_TIME_OFF," +
                                 " COL_BAND, COL_MODE, COL_RST_SENT, COL_RST_RCVD, COL_QSL_SENT, COL_QSL_SENT_VIA, COL_GRIDSQUARE, COL_STATION_CALLSIGN, COL_FREQ, COL_CONTEST_ID, COL_OPERATOR, COL_CQZ, COL_STX, COL_SWL, COL_PRIMARY_KEY" +
                                 " FROM TABLE_HRD_CONTACTS_V01" +
                                 " WHERE COL_CALL = '" + strCall + "'" +
                                 " AND COL_BAND = '" + strBand + "'" +
                                 " AND COL_MODE = '" + strMode + "'";

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

            if (!strSwl)
            {
                SqlCommand += " and COL_SWL = 0";
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
                    HRDProperties.HrdFieldsObjects FieldList = new HRDProperties.HrdFieldsObjects();
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
                    FieldList.KEY = reader["COL_PRIMARY_KEY"].ToString();
                    HrdFieldsList.Add(FieldList);
                }
                return FoundCall;
            }
        }

        public static List<HRDProperties.HrdIndexObjects> HrdIndexList = new List<HRDProperties.HrdIndexObjects>();

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
                    HRDProperties.HrdIndexObjects IndexList = new HRDProperties.HrdIndexObjects();
                    IndexList.INDEX_NAME = reader["index_name"].ToString();
                    IndexList.INDEX_COLUMNS = reader["index_columns"].ToString();
                    //   IndexList.CARDINALITY = reader["CARDINALITY"].ToString();
                    IndexList.CARDINALITY = Int32.Parse(reader["cardinality"].ToString());
                    HrdIndexList.Add(IndexList);

                }
            }
        }

        public static List<HRDProperties.ReportModeObjects> HrdModeList = new List<HRDProperties.ReportModeObjects>();

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
                    HRDProperties.ReportModeObjects ModeList = new HRDProperties.ReportModeObjects();
                    ModeList.MODE = reader["mode"].ToString();
                    ModeList.SUBMODE = reader["submode"].ToString();
                    ModeList.WORKED = reader["worked"].ToString();
                    HrdModeList.Add(ModeList);
                }
            }
        }

        public static List<HRDProperties.ReportBandObjects> HrdBandList = new List<HRDProperties.ReportBandObjects>();

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
                    HRDProperties.ReportBandObjects BandList = new HRDProperties.ReportBandObjects();
                    BandList.BAND = reader["band"].ToString();
                    BandList.WORKED = reader["worked"].ToString();
                    HrdBandList.Add(BandList);
                }
            }
        }


        public static List<HRDProperties.ReportBandModeObjects> HrdBandModeList = new List<HRDProperties.ReportBandModeObjects>();

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
                    HRDProperties.ReportBandModeObjects BandModeList = new HRDProperties.ReportBandModeObjects();
                    BandModeList.BAND = reader["band"].ToString();
                    BandModeList.MODE = reader["mode"].ToString();
                    BandModeList.SUBMODE = reader["submode"].ToString();
                    BandModeList.WORKED = reader["worked"].ToString();
                    HrdBandModeList.Add(BandModeList);
                }
            }
        }

        public static List<HRDProperties.ReportYearBandModeObjects> HrdYearBandModeList = new List<HRDProperties.ReportYearBandModeObjects>();

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
                    HRDProperties.ReportYearBandModeObjects YearBandModeList = new HRDProperties.ReportYearBandModeObjects();
                    YearBandModeList.YEAR = reader["year"].ToString();
                    YearBandModeList.BAND = reader["band"].ToString();
                    YearBandModeList.MODE = reader["mode"].ToString();
                    YearBandModeList.SUBMODE = reader["submode"].ToString();
                    YearBandModeList.WORKED = reader["worked"].ToString();
                    HrdYearBandModeList.Add(YearBandModeList);
                }
            }
        }

        public static List<HRDProperties.ReportQsoObjects> HrdQsoList = new List<HRDProperties.ReportQsoObjects>();

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
                    HRDProperties.ReportQsoObjects QsoList = new HRDProperties.ReportQsoObjects();
                    QsoList.WORKED = reader["worked"].ToString();
                    QsoList.OM = reader["om"].ToString();
                    QsoList.SWL = reader["swl"].ToString();
                    HrdQsoList.Add(QsoList);
                }
            }
        }

        public static List<HRDProperties.ReportQsoByYearObjects> HrdQsoByYearList = new List<HRDProperties.ReportQsoByYearObjects>();

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
                    HRDProperties.ReportQsoByYearObjects QsoPerYearList = new HRDProperties.ReportQsoByYearObjects();
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


        public static List<HRDProperties.ReportYearModeProperty> HrdQsoYearModeList = new List<HRDProperties.ReportYearModeProperty>();

        public static void ReportQsoYearMode()
        {
            string MyDatabase = CurrentDatabase();
            //                      (select count(*) as SWL from `TABLE_HRD_CONTACTS_V01` where col_swl=1) as SWL,
            string SqlCommand = @"SELECT DATE_FORMAT(COL_TIME_ON, '%Y') as YEAR,
                                SUM(CASE WHEN COL_MODE = 'AM' THEN 1 ELSE 0 END) AM,
                                SUM(CASE WHEN COL_MODE = 'ARDOP' THEN 1 ELSE 0 END) ARDOP,
                                SUM(CASE WHEN COL_MODE = 'ATV' THEN 1 ELSE 0 END) ATV,
                                SUM(CASE WHEN COL_MODE = 'C4FM' THEN 1 ELSE 0 END) C4FM,
                                SUM(CASE WHEN COL_MODE = 'CHIP' THEN 1 ELSE 0 END) CHIP,
                                SUM(CASE WHEN COL_MODE = 'CLO' THEN 1 ELSE 0 END) CLO,
                                SUM(CASE WHEN COL_MODE = 'CONTESTI' THEN 1 ELSE 0 END) CONTESTI,
                                SUM(CASE WHEN COL_MODE = 'CW' THEN 1 ELSE 0 END) CW,
                                SUM(CASE WHEN COL_MODE = 'DIGITALVOICE' THEN 1 ELSE 0 END) DIGITALVOICE,
                                SUM(CASE WHEN COL_MODE = 'DOMINO' THEN 1 ELSE 0 END) DOMINO,
                                SUM(CASE WHEN COL_MODE = 'DSTAR' THEN 1 ELSE 0 END) DSTAR,
                                SUM(CASE WHEN COL_MODE = 'FAX' THEN 1 ELSE 0 END) FAX,
                                SUM(CASE WHEN COL_MODE = 'FM' THEN 1 ELSE 0 END) FM,
                                SUM(CASE WHEN COL_MODE = 'FSK441' THEN 1 ELSE 0 END) FSK441,
                                SUM(CASE WHEN COL_MODE = 'FT8' THEN 1 ELSE 0 END) FT8,
                                SUM(CASE WHEN COL_MODE = 'HELL' THEN 1 ELSE 0 END) HELL,
                                SUM(CASE WHEN COL_MODE = 'ISCAT' THEN 1 ELSE 0 END) ISCAT,
                                SUM(CASE WHEN COL_MODE = 'JT4' THEN 1 ELSE 0 END) JT4,
                                SUM(CASE WHEN COL_MODE = 'JT6M' THEN 1 ELSE 0 END) JT6M,
                                SUM(CASE WHEN COL_MODE = 'JT9' THEN 1 ELSE 0 END) JT9,
                                SUM(CASE WHEN COL_MODE = 'JT44' THEN 1 ELSE 0 END) JT44,
                                SUM(CASE WHEN COL_MODE = 'JT65' THEN 1 ELSE 0 END) JT65,
                                SUM(CASE WHEN COL_MODE = 'MFSK' THEN 1 ELSE 0 END) MFSK,
                                SUM(CASE WHEN COL_MODE = 'MSK144' THEN 1 ELSE 0 END) MSK144,
                                SUM(CASE WHEN COL_MODE = 'MT63' THEN 1 ELSE 0 END) MT63,
                                SUM(CASE WHEN COL_MODE = 'OLIVIA' THEN 1 ELSE 0 END) OLIVIA,
                                SUM(CASE WHEN COL_MODE = 'OPERA' THEN 1 ELSE 0 END) OPERA,
                                SUM(CASE WHEN COL_MODE = 'PAC' THEN 1 ELSE 0 END) PAC,
                                SUM(CASE WHEN COL_MODE = 'PAX' THEN 1 ELSE 0 END) PAX,
                                SUM(CASE WHEN COL_MODE = 'PKT' THEN 1 ELSE 0 END) PKT,
                                SUM(CASE WHEN COL_MODE = 'PSK' THEN 1 ELSE 0 END) PSK,
                                SUM(CASE WHEN COL_MODE = 'PSK2K' THEN 1 ELSE 0 END) PSK2K,
                                SUM(CASE WHEN COL_MODE = 'Q15' THEN 1 ELSE 0 END) Q15,
                                SUM(CASE WHEN COL_MODE = 'QRA64' THEN 1 ELSE 0 END) QRA64,
                                SUM(CASE WHEN COL_MODE = 'ROS' THEN 1 ELSE 0 END) ROS,
                                SUM(CASE WHEN COL_MODE = 'RTTY' THEN 1 ELSE 0 END) RTTY,
                                SUM(CASE WHEN COL_MODE = 'RTTYM' THEN 1 ELSE 0 END) RTTYM,
                                SUM(CASE WHEN COL_MODE IN ('SSB','LSB','USB') THEN 1 ELSE 0 END) SSB,
                                SUM(CASE WHEN COL_MODE = 'SSTV' THEN 1 ELSE 0 END) SSTV,
                                SUM(CASE WHEN COL_MODE = 'T10' THEN 1 ELSE 0 END) T10,
                                SUM(CASE WHEN COL_MODE = 'THOR' THEN 1 ELSE 0 END) THOR,
                                SUM(CASE WHEN COL_MODE = 'THRB' THEN 1 ELSE 0 END) THRB,
                                SUM(CASE WHEN COL_MODE = 'TOR' THEN 1 ELSE 0 END) TOR,
                                SUM(CASE WHEN COL_MODE = 'V4' THEN 1 ELSE 0 END) V4,
                                SUM(CASE WHEN COL_MODE = 'VOI' THEN 1 ELSE 0 END) VOI,
                                SUM(CASE WHEN COL_MODE = 'WINMOR' THEN 1 ELSE 0 END) WINMOR,
                                SUM(CASE WHEN COL_MODE = 'WSPR' THEN 1 ELSE 0 END) WSPR,
                                SUM(CASE WHEN COL_MODE = 'AMTORFEC' THEN 1 ELSE 0 END) AMTORFEC,
                                SUM(CASE WHEN COL_MODE = 'ASCI' THEN 1 ELSE 0 END) ASCI,
                                SUM(CASE WHEN COL_MODE = 'CHIP64' THEN 1 ELSE 0 END) CHIP64,
                                SUM(CASE WHEN COL_MODE = 'CHIP128' THEN 1 ELSE 0 END) CHIP128,
                                SUM(CASE WHEN COL_MODE = 'DOMINOF' THEN 1 ELSE 0 END) DOMINOF,
                                SUM(CASE WHEN COL_MODE = 'FMHELL' THEN 1 ELSE 0 END) FMHELL,
                                SUM(CASE WHEN COL_MODE = 'FSK31' THEN 1 ELSE 0 END) FSK31,
                                SUM(CASE WHEN COL_MODE = 'GTOR' THEN 1 ELSE 0 END) GTOR,
                                SUM(CASE WHEN COL_MODE = 'HELL80' THEN 1 ELSE 0 END) HELL80,
                                SUM(CASE WHEN COL_MODE = 'HFSK' THEN 1 ELSE 0 END) HFSK,
                                SUM(CASE WHEN COL_MODE = 'JT4A' THEN 1 ELSE 0 END) JT4A,
                                SUM(CASE WHEN COL_MODE = 'JT4B' THEN 1 ELSE 0 END) JT4B,
                                SUM(CASE WHEN COL_MODE = 'JT4C' THEN 1 ELSE 0 END) JT4C,
                                SUM(CASE WHEN COL_MODE = 'JT4D' THEN 1 ELSE 0 END) JT4D,
                                SUM(CASE WHEN COL_MODE = 'JT4E' THEN 1 ELSE 0 END) JT4E,
                                SUM(CASE WHEN COL_MODE = 'JT4F' THEN 1 ELSE 0 END) JT4F,
                                SUM(CASE WHEN COL_MODE = 'JT4G' THEN 1 ELSE 0 END) JT4G,
                                SUM(CASE WHEN COL_MODE = 'JT65A' THEN 1 ELSE 0 END) JT65A,
                                SUM(CASE WHEN COL_MODE = 'JT65B' THEN 1 ELSE 0 END) JT65B,
                                SUM(CASE WHEN COL_MODE = 'JT65C' THEN 1 ELSE 0 END) JT65C,
                                SUM(CASE WHEN COL_MODE = 'MFSK8' THEN 1 ELSE 0 END) MFSK8,
                                SUM(CASE WHEN COL_MODE = 'MFSK16' THEN 1 ELSE 0 END) MFSK16,
                                SUM(CASE WHEN COL_MODE = 'PAC2' THEN 1 ELSE 0 END) PAC2,
                                SUM(CASE WHEN COL_MODE = 'PAC3' THEN 1 ELSE 0 END) PAC3,
                                SUM(CASE WHEN COL_MODE = 'PAX2' THEN 1 ELSE 0 END) PAX2,
                                SUM(CASE WHEN COL_MODE = 'PCW' THEN 1 ELSE 0 END) PCW,
                                SUM(CASE WHEN COL_MODE = 'PSK10' THEN 1 ELSE 0 END) PSK10,
                                SUM(CASE WHEN COL_MODE = 'PSK31' THEN 1 ELSE 0 END) PSK31,
                                SUM(CASE WHEN COL_MODE = 'PSK63' THEN 1 ELSE 0 END) PSK63,
                                SUM(CASE WHEN COL_MODE = 'PSK63F' THEN 1 ELSE 0 END) PSK63F,
                                SUM(CASE WHEN COL_MODE = 'PSK125' THEN 1 ELSE 0 END) PSK125,
                                SUM(CASE WHEN COL_MODE = 'PSKAM10' THEN 1 ELSE 0 END) PSKAM10,
                                SUM(CASE WHEN COL_MODE = 'PSKAM31' THEN 1 ELSE 0 END) PSKAM31,
                                SUM(CASE WHEN COL_MODE = 'PSKAM50' THEN 1 ELSE 0 END) PSKAM50,
                                SUM(CASE WHEN COL_MODE = 'PSKFEC31' THEN 1 ELSE 0 END) PSKFEC31,
                                SUM(CASE WHEN COL_MODE = 'PSKHELL' THEN 1 ELSE 0 END) PSKHELL,
                                SUM(CASE WHEN COL_MODE = 'QPSK31' THEN 1 ELSE 0 END) QPSK31,
                                SUM(CASE WHEN COL_MODE = 'QPSK63' THEN 1 ELSE 0 END) QPSK63,
                                SUM(CASE WHEN COL_MODE = 'QPSK125' THEN 1 ELSE 0 END) QPSK125,
                                SUM(CASE WHEN COL_MODE = 'THRBX' THEN 1 ELSE 0 END) THRBX
                                FROM    `TABLE_HRD_CONTACTS_V01` 
                                WHERE COL_SWL = 0
                                GROUP BY DATE_FORMAT(COL_TIME_ON, '%Y')
                                ORDER BY DATE_FORMAT(COL_TIME_ON, '%Y');";
            MySqlCommand Command = new MySqlCommand(SqlCommand, HrdLogbookConnection);
            Command.CommandTimeout = 3600;
            using (MySqlDataReader reader = Command.ExecuteReader())
            {
                HrdQsoYearModeList.Clear();
                while (reader.Read())
                {
                    HRDProperties.ReportYearModeProperty QsoYearModeList = new HRDProperties.ReportYearModeProperty();
                    QsoYearModeList.YEAR = reader["year"].ToString();
                    QsoYearModeList.AM = reader["am"].ToString();
                    QsoYearModeList.ARDOP = reader["ardop"].ToString();
                    QsoYearModeList.ATV = reader["atv"].ToString();
                    QsoYearModeList.C4FM = reader["c4fm"].ToString();
                    QsoYearModeList.CHIP = reader["chip"].ToString();
                    QsoYearModeList.CLO = reader["clo"].ToString();
                    QsoYearModeList.CONTESTI = reader["contesti"].ToString();
                    QsoYearModeList.CW = reader["cw"].ToString();
                    QsoYearModeList.DIGITALVOICE = reader["digitalvoice"].ToString();
                    QsoYearModeList.DOMINO = reader["domino"].ToString();
                    QsoYearModeList.DSTAR = reader["dstar"].ToString();
                    QsoYearModeList.FAX = reader["fax"].ToString();
                    QsoYearModeList.FM = reader["fm"].ToString();
                    QsoYearModeList.FSK441 = reader["fsk441"].ToString();
                    QsoYearModeList.FT8 = reader["ft8"].ToString();
                    QsoYearModeList.HELL = reader["hell"].ToString();
                    QsoYearModeList.ISCAT = reader["iscat"].ToString();
                    QsoYearModeList.JT4 = reader["jt4"].ToString();
                    QsoYearModeList.JT6M = reader["jt6m"].ToString();
                    QsoYearModeList.JT9 = reader["jt9"].ToString();
                    QsoYearModeList.JT44 = reader["jt44"].ToString();
                    QsoYearModeList.JT65 = reader["jt65"].ToString();
                    QsoYearModeList.MFSK = reader["mfsk"].ToString();
                    QsoYearModeList.MSK144 = reader["msk144"].ToString();
                    QsoYearModeList.MT63 = reader["mt63"].ToString();
                    QsoYearModeList.OLIVIA = reader["olivia"].ToString();
                    QsoYearModeList.OPERA = reader["opera"].ToString();
                    QsoYearModeList.PAC = reader["pac"].ToString();
                    QsoYearModeList.PAX = reader["pax"].ToString();
                    QsoYearModeList.PKT = reader["pkt"].ToString();
                    QsoYearModeList.PSK = reader["psk"].ToString();
                    QsoYearModeList.PSK2K = reader["psk2k"].ToString();
                    QsoYearModeList.Q15 = reader["q15"].ToString();
                    QsoYearModeList.QRA64 = reader["qra64"].ToString();
                    QsoYearModeList.ROS = reader["ros"].ToString();
                    QsoYearModeList.RTTY = reader["rtty"].ToString();
                    QsoYearModeList.RTTYM = reader["rttym"].ToString();
                    QsoYearModeList.SSB = reader["ssb"].ToString();
                    QsoYearModeList.SSTV = reader["sstv"].ToString();
                    QsoYearModeList.T10 = reader["t10"].ToString();
                    QsoYearModeList.THOR = reader["thor"].ToString();
                    QsoYearModeList.THRB = reader["thrb"].ToString();
                    QsoYearModeList.TOR = reader["tor"].ToString();
                    QsoYearModeList.V4 = reader["v4"].ToString();
                    QsoYearModeList.VOI = reader["voi"].ToString();
                    QsoYearModeList.WINMOR = reader["winmor"].ToString();
                    QsoYearModeList.WSPR = reader["wspr"].ToString();
                    QsoYearModeList.AMTORFEC = reader["amtorfec"].ToString();
                    QsoYearModeList.ASCI = reader["asci"].ToString();
                    QsoYearModeList.CHIP64 = reader["chip64"].ToString();
                    QsoYearModeList.CHIP128 = reader["chip128"].ToString();
                    QsoYearModeList.DOMINOF = reader["dominof"].ToString();
                    QsoYearModeList.FMHELL = reader["fmhell"].ToString();
                    QsoYearModeList.FSK31 = reader["fsk31"].ToString();
                    QsoYearModeList.GTOR = reader["gtor"].ToString();
                    QsoYearModeList.HELL80 = reader["hell80"].ToString();
                    QsoYearModeList.HFSK = reader["hfsk"].ToString();
                    QsoYearModeList.JT4A = reader["jt4a"].ToString();
                    QsoYearModeList.JT4B = reader["jt4b"].ToString();
                    QsoYearModeList.JT4C = reader["jt4c"].ToString();
                    QsoYearModeList.JT4D = reader["jt4d"].ToString();
                    QsoYearModeList.JT4E = reader["jt4e"].ToString();
                    QsoYearModeList.JT4F = reader["jt4f"].ToString();
                    QsoYearModeList.JT4G = reader["jt4g"].ToString();
                    QsoYearModeList.JT65A = reader["jt65a"].ToString();
                    QsoYearModeList.JT65B = reader["jt65b"].ToString();
                    QsoYearModeList.JT65C = reader["jt65c"].ToString();
                    QsoYearModeList.MFSK8 = reader["mfsk8"].ToString();
                    QsoYearModeList.MFSK16 = reader["mfsk16"].ToString();
                    QsoYearModeList.PAC2 = reader["pac2"].ToString();
                    QsoYearModeList.PAC3 = reader["pac3"].ToString();
                    QsoYearModeList.PAX2 = reader["pax2"].ToString();
                    QsoYearModeList.PCW = reader["pcw"].ToString();
                    QsoYearModeList.PSK10 = reader["psk10"].ToString();
                    QsoYearModeList.PSK31 = reader["psk31"].ToString();
                    QsoYearModeList.PSK63 = reader["psk63"].ToString();
                    QsoYearModeList.PSK63F = reader["psk63f"].ToString();
                    QsoYearModeList.PSK125 = reader["psk125"].ToString();
                    QsoYearModeList.PSKAM10 = reader["pskam10"].ToString();
                    QsoYearModeList.PSKAM31 = reader["pskam31"].ToString();
                    QsoYearModeList.PSKAM50 = reader["pskam50"].ToString();
                    QsoYearModeList.PSKFEC31 = reader["pskfec31"].ToString();
                    QsoYearModeList.PSKHELL = reader["pskhell"].ToString();
                    QsoYearModeList.QPSK31 = reader["qpsk31"].ToString();
                    QsoYearModeList.QPSK63 = reader["qpsk63"].ToString();
                    QsoYearModeList.QPSK125 = reader["qpsk125"].ToString();
                    QsoYearModeList.THRBX = reader["thrbx"].ToString();
                    HrdQsoYearModeList.Add(QsoYearModeList);
                }
            }
        }








        public static List<HRDProperties.DxccObjects> HrdDxccList = new List<HRDProperties.DxccObjects>();

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
                        HRDProperties.DxccObjects DxccList = new HRDProperties.DxccObjects();
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


        public static Int32 UpdateHrdQso(string PrimaryKey, string Operator, Boolean TitleCase)
        {
            // https://stackoverflow.com/questions/20492019/update-statement-in-mysql-using-c-sharp
 
            if (TitleCase)
            {
                Operator = CommonUtilities.ToTitleCase(Operator);
            }

            //Console.WriteLine("Key = {0}, Operator = {1}", PrimaryKey, Operator);

            string SqlCommand = "UPDATE TABLE_HRD_CONTACTS_V01 SET COL_OPERATOR=@Operator WHERE COL_PRIMARY_KEY=@Primary_key;";
            MySqlCommand Command = new MySqlCommand();
            Command.CommandText = SqlCommand;
            Command.Parameters.AddWithValue("@Operator", Operator);
            Command.Parameters.AddWithValue("@Primary_key", PrimaryKey);
            Command.Connection = HrdLogbookConnection;
            Int32 Affectedrows = Command.ExecuteNonQuery();
            //if (Affectedrows != 0)
            //{
            //    //con.Close();
            //    //return true;
            //    Console.WriteLine("Effected rows : {0}",Affectedrows.ToString());
            //}
            return Affectedrows;

        }

        public static Int32 UpdateHrdOperator(string Call, string Operator, Boolean TitleCase)
        {
            // https://stackoverflow.com/questions/20492019/update-statement-in-mysql-using-c-sharp

            if (TitleCase)
            {
                Operator = CommonUtilities.ToTitleCase(Operator);
            }

            //Console.WriteLine("Key = {0}, Operator = {1}", PrimaryKey, Operator);

            string SqlCommand = "UPDATE TABLE_HRD_CONTACTS_V01 SET COL_OPERATOR=@Operator WHERE COL_CALL=@Call;";
            MySqlCommand Command = new MySqlCommand();
            Command.CommandText = SqlCommand;
            Command.Parameters.AddWithValue("@Operator", Operator);
            Command.Parameters.AddWithValue("@Call", Call);
            Command.Connection = HrdLogbookConnection;
            Int32 Affectedrows = Command.ExecuteNonQuery();
            return Affectedrows;
        }



        //public static HRDProperties.HrdFieldsObjects HrdFields = new HRDProperties.HrdFieldsObjects();

        // public static HrdIndexObjects HrdIndexen = new HrdIndexObjects();
        //    public static ReportBandObjects HrdReportBand = new ReportBandObjects();

        //      public static ReportModeObjects HrdReportMode = new ReportModeObjects();
        //       public static ReportBandModeObjects HrdReportBandMode = new ReportBandModeObjects();

        //      public static ReportYearBandModeObjects HrdReportYearBandMode = new ReportYearBandModeObjects();

        //       public static ReportQsoObjects HrdReportQso = new ReportQsoObjects();

        //      public static DxccObjects HrdDxcc = new DxccObjects();

    }
}
