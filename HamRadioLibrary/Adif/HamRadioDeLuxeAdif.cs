using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.IO;
using System.Collections;
using AdifProperties;

namespace AdifLibrary
{
    public class HamRadioDeLuxeAdif

    {
        public static dynamic GetAllAdifElements(string AdifLine)
        {
           ADIFProperties.AdifFieldObjects    FieldList = new ADIFProperties.AdifFieldObjects();
            FieldList.ADDRESS = GetAdifElement("ADDRESS", AdifLine);
            FieldList.ADDRESS_INTL = GetAdifElement("ADDRESS_INTL", AdifLine);
            FieldList.AGE = GetAdifElement("AGE", AdifLine);
            FieldList.A_INDEX = GetAdifElement("A_INDEX", AdifLine);
            FieldList.ANT_AZ = GetAdifElement("ANT_AZ", AdifLine);
            FieldList.ANT_EL = GetAdifElement("ANT_EL", AdifLine);
            FieldList.ANT_PATH = GetAdifElement("ANT_PATH", AdifLine);
            FieldList.ARRL_SECT = GetAdifElement("ARRL_SECT", AdifLine);
            FieldList.AWARD_SUBMITTED = GetAdifElement("AWARD_SUBMITTED", AdifLine);
            FieldList.AWARD_GRANTED = GetAdifElement("AWARD_GRANTED", AdifLine);
            FieldList.BAND = GetAdifElement("BAND", AdifLine);
            FieldList.BAND_RX = GetAdifElement("BAND_RX", AdifLine);
            FieldList.CALL = GetAdifElement("CALL", AdifLine);
            FieldList.CHECK = GetAdifElement("CHECK", AdifLine);
            FieldList.CLASS = GetAdifElement("CLASS", AdifLine);
            FieldList.CLUBLOG_QSO_UPLOAD_DATE = GetAdifElement("CLUBLOG_QSO_UPLOAD_DATE", AdifLine);
            FieldList.CLUBLOG_QSO_UPLOAD_STATUS = GetAdifElement("CLUBLOG_QSO_UPLOAD_STATUS", AdifLine);
            FieldList.CNTY = GetAdifElement("CNTY", AdifLine);
            FieldList.COMMENT = GetAdifElement("COMMENT", AdifLine);
            FieldList.COMMENT_INTL = GetAdifElement("COMMENT_INTL", AdifLine);
            FieldList.CONT = GetAdifElement("CONT", AdifLine);
            FieldList.CONTACTED_OP = GetAdifElement("CONTACTED_OP", AdifLine);
            FieldList.CONTEST_ID = GetAdifElement("CONTEST_ID", AdifLine);
            FieldList.COUNTRY = GetAdifElement("COUNTRY", AdifLine);
            FieldList.COUNTRY_INTL = GetAdifElement("COUNTRY_INTL", AdifLine);
            FieldList.CQZ = GetAdifElement("CQZ", AdifLine);
            FieldList.CREDIT_SUBMITTED = GetAdifElement("CREDIT_SUBMITTED", AdifLine);
            FieldList.CREDIT_GRANTED = GetAdifElement("CREDIT_GRANTED", AdifLine);
            FieldList.DARC_DOK = GetAdifElement("DARC_DOK", AdifLine);
            FieldList.DISTANCE = GetAdifElement("DISTANCE", AdifLine);
            FieldList.DXCC = GetAdifElement("DXCC", AdifLine);
            FieldList.EMAIL = GetAdifElement("EMAIL", AdifLine);
            FieldList.EQ_CALL = GetAdifElement("EQ_CALL", AdifLine);
            FieldList.EQSL_QSLRDATE = GetAdifElement("EQSL_QSLRDATE", AdifLine);
            FieldList.EQSL_QSLSDATE = GetAdifElement("EQSL_QSLSDATE", AdifLine);
            FieldList.EQSL_QSL_RCVD = GetAdifElement("EQSL_QSL_RCVD", AdifLine);
            FieldList.EQSL_QSL_SENT = GetAdifElement("EQSL_QSL_SENT", AdifLine);
            FieldList.FISTS = GetAdifElement("FISTS", AdifLine);
            FieldList.FISTS_CC = GetAdifElement("FISTS_CC", AdifLine);
            FieldList.FORCE_INIT = GetAdifElement("FORCE_INIT", AdifLine);
            FieldList.FREQ = GetAdifElement("FREQ", AdifLine);
            FieldList.FREQ_RX = GetAdifElement("FREQ_RX", AdifLine);
            FieldList.GRIDSQUARE = GetAdifElement("GRIDSQUARE", AdifLine);
            FieldList.GUEST_OP = GetAdifElement("GUEST_OP", AdifLine);
            FieldList.HRDLOG_QSO_UPLOAD_DATE = GetAdifElement("HRDLOG_QSO_UPLOAD_DATE", AdifLine);
            FieldList.HRDLOG_QSO_UPLOAD_STATUS = GetAdifElement("HRDLOG_QSO_UPLOAD_STATUS", AdifLine);
            FieldList.IOTA = GetAdifElement("IOTA", AdifLine);
            FieldList.IOTA_ISLAND_ID = GetAdifElement("IOTA_ISLAND_ID", AdifLine);
            FieldList.ITUZ = GetAdifElement("ITUZ", AdifLine);
            FieldList.K_INDEX = GetAdifElement("K_INDEX", AdifLine);
            FieldList.LAT = GetAdifElement("LAT", AdifLine);
            FieldList.LON = GetAdifElement("LON", AdifLine);
            FieldList.LOTW_QSLRDATE = GetAdifElement("LOTW_QSLRDATE", AdifLine);
            FieldList.LOTW_QSLSDATE = GetAdifElement("LOTW_QSLSDATE", AdifLine);
            FieldList.LOTW_QSL_RCVD = GetAdifElement("LOTW_QSL_RCVD", AdifLine);
            FieldList.LOTW_QSL_SENT = GetAdifElement("LOTW_QSL_SENT", AdifLine);
            FieldList.MAX_BURSTS = GetAdifElement("MAX_BURSTS", AdifLine);
            FieldList.MODE = GetAdifElement("MODE", AdifLine);
            FieldList.MS_SHOWER = GetAdifElement("MS_SHOWER", AdifLine);
            FieldList.MY_ANTENNA = GetAdifElement("MY_ANTENNA", AdifLine);
            FieldList.MY_ANTENNA_INTL = GetAdifElement("MY_ANTENNA_INTL", AdifLine);
            FieldList.MY_CITY = GetAdifElement("MY_CITY", AdifLine);
            FieldList.MY_CITY_INTL = GetAdifElement("MY_CITY_INTL", AdifLine);
            FieldList.MY_CNTY = GetAdifElement("MY_CNTY", AdifLine);
            FieldList.MY_COUNTRY = GetAdifElement("MY_COUNTRY", AdifLine);
            FieldList.MY_COUNTRY_INTL = GetAdifElement("MY_COUNTRY_INTL", AdifLine);
            FieldList.MY_CQ_ZONE = GetAdifElement("MY_CQ_ZONE", AdifLine);
            FieldList.MY_DXCC = GetAdifElement("MY_DXCC", AdifLine);
            FieldList.MY_FISTS = GetAdifElement("MY_FISTS", AdifLine);
            FieldList.MY_GRIDSQUARE = GetAdifElement("MY_GRIDSQUARE", AdifLine);
            FieldList.MY_IOTA = GetAdifElement("MY_IOTA", AdifLine);
            FieldList.MY_IOTA_ISLAND_ID = GetAdifElement("MY_IOTA_ISLAND_ID", AdifLine);
            FieldList.MY_ITU_ZONE = GetAdifElement("MY_ITU_ZONE", AdifLine);
            FieldList.MY_LAT = GetAdifElement("MY_LAT", AdifLine);
            FieldList.MY_LON = GetAdifElement("MY_LON", AdifLine);
            FieldList.MY_NAME = GetAdifElement("MY_NAME", AdifLine);
            FieldList.MY_NAME_INTL = GetAdifElement("MY_NAME_INTL", AdifLine);
            FieldList.MY_POSTAL_CODE = GetAdifElement("MY_POSTAL_CODE", AdifLine);
            FieldList.MY_POSTAL_CODE_INTL = GetAdifElement("MY_POSTAL_CODE_INTL", AdifLine);
            FieldList.MY_RIG = GetAdifElement("MY_RIG", AdifLine);
            FieldList.MY_RIG_INTL = GetAdifElement("MY_RIG_INTL", AdifLine);
            FieldList.MY_SIG = GetAdifElement("MY_SIG", AdifLine);
            FieldList.MY_SIG_INTL = GetAdifElement("MY_SIG_INTL", AdifLine);
            FieldList.MY_SIG_INFO = GetAdifElement("MY_SIG_INFO", AdifLine);
            FieldList.MY_SIG_INFO_INTL = GetAdifElement("MY_SIG_INFO_INTL", AdifLine);
            FieldList.MY_SOTA_REF = GetAdifElement("MY_SOTA_REF", AdifLine);
            FieldList.MY_STATE = GetAdifElement("MY_STATE", AdifLine);
            FieldList.MY_STREET = GetAdifElement("MY_STREET", AdifLine);
            FieldList.MY_STREET_INTL = GetAdifElement("MY_STREET_INTL", AdifLine);
            FieldList.MY_USACA_COUNTIES = GetAdifElement("MY_USACA_COUNTIES", AdifLine);
            FieldList.MY_VUCC_GRIDS = GetAdifElement("MY_VUCC_GRIDS", AdifLine);
            FieldList.NAME = GetAdifElement("NAME", AdifLine);
            FieldList.NAME_INTL = GetAdifElement("NAME_INTL", AdifLine);
            FieldList.NOTES = GetAdifElement("NOTES", AdifLine);
            FieldList.NOTES_INTL = GetAdifElement("NOTES_INTL", AdifLine);
            FieldList.NR_BURSTS = GetAdifElement("NR_BURSTS", AdifLine);
            FieldList.NR_PINGS = GetAdifElement("NR_PINGS", AdifLine);
            FieldList.OPERATOR = GetAdifElement("OPERATOR", AdifLine);
            FieldList.OWNER_CALLSIGN = GetAdifElement("OWNER_CALLSIGN", AdifLine);
            FieldList.PFX = GetAdifElement("PFX", AdifLine);
            FieldList.PRECEDENCE = GetAdifElement("PRECEDENCE", AdifLine);
            FieldList.PROP_MODE = GetAdifElement("PROP_MODE", AdifLine);
            FieldList.PUBLIC_KEY = GetAdifElement("PUBLIC_KEY", AdifLine);
            FieldList.QRZCOM_QSO_UPLOAD_DATE = GetAdifElement("QRZCOM_QSO_UPLOAD_DATE", AdifLine);
            FieldList.QRZCOM_QSO_UPLOAD_STATUS = GetAdifElement("QRZCOM_QSO_UPLOAD_STATUS", AdifLine);
            FieldList.QSLMSG = GetAdifElement("QSLMSG", AdifLine);
            FieldList.QSLMSG_INTL = GetAdifElement("QSLMSG_INTL", AdifLine);
            FieldList.QSLRDATE = GetAdifElement("QSLRDATE", AdifLine);
            FieldList.QSLSDATE = GetAdifElement("QSLSDATE", AdifLine);
            FieldList.QSL_RCVD = GetAdifElement("QSL_RCVD", AdifLine);
            FieldList.QSL_RCVD_VIA = GetAdifElement("QSL_RCVD_VIA", AdifLine);
            FieldList.QSL_SENT = GetAdifElement("QSL_SENT", AdifLine);
            FieldList.QSL_SENT_VIA = GetAdifElement("QSL_SENT_VIA", AdifLine);
            FieldList.QSL_VIA = GetAdifElement("QSL_VIA", AdifLine);
            FieldList.QSO_COMPLETE = GetAdifElement("QSO_COMPLETE", AdifLine);
            FieldList.QSO_DATE = GetAdifElement("QSO_DATE", AdifLine);
            FieldList.QSO_DATE_OFF = GetAdifElement("QSO_DATE_OFF", AdifLine);
            FieldList.QSO_RANDOM = GetAdifElement("QSO_RANDOM", AdifLine);
            FieldList.QTH = GetAdifElement("QTH", AdifLine);
            FieldList.QTH_INTL = GetAdifElement("QTH_INTL", AdifLine);
            FieldList.REGION = GetAdifElement("REGION", AdifLine);
            FieldList.RIG = GetAdifElement("RIG", AdifLine);
            FieldList.RIG_INTL = GetAdifElement("RIG_INTL", AdifLine);
            FieldList.RST_RCVD = GetAdifElement("RST_RCVD", AdifLine);
            FieldList.RST_SENT = GetAdifElement("RST_SENT", AdifLine);
            FieldList.RX_PWR = GetAdifElement("RX_PWR", AdifLine);
            FieldList.SAT_MODE = GetAdifElement("SAT_MODE", AdifLine);
            FieldList.SAT_NAME = GetAdifElement("SAT_NAME", AdifLine);
            FieldList.SFI = GetAdifElement("SFI", AdifLine);
            FieldList.SIG = GetAdifElement("SIG", AdifLine);
            FieldList.SIG_INTL = GetAdifElement("SIG_INTL", AdifLine);
            FieldList.SIG_INFO = GetAdifElement("SIG_INFO", AdifLine);
            FieldList.SIG_INFO_INTL = GetAdifElement("SIG_INFO_INTL", AdifLine);
            FieldList.SILENT_KEY = GetAdifElement("SILENT_KEY", AdifLine);
            FieldList.SKCC = GetAdifElement("SKCC", AdifLine);
            FieldList.SOTA_REF = GetAdifElement("SOTA_REF", AdifLine);
            FieldList.SRX = GetAdifElement("SRX", AdifLine);
            FieldList.SRX_STRING = GetAdifElement("SRX_STRING", AdifLine);
            FieldList.STATE = GetAdifElement("STATE", AdifLine);
            FieldList.STATION_CALLSIGN = GetAdifElement("STATION_CALLSIGN", AdifLine);
            FieldList.STX = GetAdifElement("STX", AdifLine);
            FieldList.STX_STRING = GetAdifElement("STX_STRING", AdifLine);
            FieldList.SUBMODE = GetAdifElement("SUBMODE", AdifLine);
            FieldList.SWL = GetAdifElement("SWL", AdifLine);
            FieldList.TEN_TEN = GetAdifElement("TEN_TEN", AdifLine);
            FieldList.TIME_OFF = GetAdifElement("TIME_OFF", AdifLine);
            FieldList.TIME_ON = GetAdifElement("TIME_ON", AdifLine);
            FieldList.TX_PWR = GetAdifElement("TX_PWR", AdifLine);
            FieldList.UKSMG = GetAdifElement("UKSMG", AdifLine);
            FieldList.USACA_COUNTIES = GetAdifElement("USACA_COUNTIES", AdifLine);
            FieldList.VE_PROV = GetAdifElement("VE_PROV", AdifLine);
            FieldList.VUCC_GRIDS = GetAdifElement("VUCC_GRIDS", AdifLine);
            FieldList.WEB = GetAdifElement("WEB", AdifLine);
            FieldList.APP_N1MM_POINTS = GetAdifElement("APP_N1MM_POINTS", AdifLine);
            FieldList.APP_N1MM_RADIO_NR = GetAdifElement("APP_N1MM_RADIO_NR", AdifLine);
            FieldList.APP_N1MM_CONTINENT = GetAdifElement("APP_N1MM_CONTINENT", AdifLine);
            FieldList.APP_N1MM_RUN1RUN2 = GetAdifElement("APP_N1MM_RUN1RUN2", AdifLine);
            FieldList.APP_N1MM_RADIOINTERFACED = GetAdifElement("APP_N1MM_RADIOINTERFACED", AdifLine);
            FieldList.APP_N1MM_ISORIGINAL = GetAdifElement("APP_N1MM_ISORIGINAL", AdifLine);
            FieldList.APP_N1MM_NETBIOSNAME = GetAdifElement("APP_N1MM_NETBIOSNAME", AdifLine);
            FieldList.APP_N1MM_ISRUNQSO = GetAdifElement("APP_N1MM_ISRUNQSO", AdifLine);
            FieldList.APP_N1MM_ID = GetAdifElement("APP_N1MM_ID", AdifLine);
            FieldList.APP_N1MM_CLAIMEDQSO = GetAdifElement("APP_N1MM_CLAIMEDQSO", AdifLine);
            FieldList.APP_EQSL_AG = GetAdifElement("APP_EQSL_AG", AdifLine);
            return FieldList;
        }
        public static string GetAdifElement(string FieldName, string AdifString)
        {

            try
            {
                // <CALL:6>SV1PMR
                string AdifStringUpper = AdifString.ToUpper();
                string SearchFieldName = "<" + FieldName.ToUpper() + ":";
                int StartPositionFieldName = AdifStringUpper.IndexOf(SearchFieldName);
                if (StartPositionFieldName >= 0)
                {
                    int StartPositionGreaterThen = AdifStringUpper.IndexOf(">", StartPositionFieldName) + 1;
                    int FieldNameLength = SearchFieldName.Length;
                    int StartPositionValue = StartPositionFieldName + FieldNameLength;
                    int EndPositionValue = StartPositionGreaterThen - FieldNameLength - StartPositionFieldName;
                    string ValueLength = AdifStringUpper.Substring(StartPositionValue, EndPositionValue - 1);
                    if (ValueLength.IndexOf(":D") >= 0)
                    {
                        ValueLength = ValueLength.Replace(":D", "");
                    }
                    if (!IsNumeric(ValueLength))
                    {
                        throw (new IsNotNumeric("IsNotNumeric Generated: The string contains characters. "));
                    }
                    int ValueLengthInteger = Int16.Parse(ValueLength);
                    string ReturnValue = AdifString.Substring(StartPositionGreaterThen, ValueLengthInteger);
                    return ReturnValue;
                }
                else
                {
                    return null;
                }
            }
            catch (IsNotNumeric ex)
            {
                // return null;
                return ex.Message.ToString();
                //Console.WriteLine(ex.Message.ToString());
                //Console.ReadLine();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool IsNumeric(string strNumber)
        {
            if (string.IsNullOrEmpty(strNumber))
            {
                return false;
            }
            else
            {
                int numberOfChar = strNumber.Count();
                if (numberOfChar > 0)
                {
                    bool r = strNumber.All(char.IsDigit);
                    return r;
                }
                else
                {
                    return false;
                }
            }
        }


        public class IsNotNumeric : Exception
        {
            public IsNotNumeric(string message)
                : base(message)
            {
            }
        }


        public static ArrayList AdifArray = new ArrayList();
        public static ArrayList HeaderArray = new ArrayList();

        public static dynamic AdifReadFile(string filename)
        {
            string line;
            Boolean FoundEOH = false;
            string QsoLine = "";
            AdifArray.Clear();
            HeaderArray.Clear();


            try
            {
                StreamReader file = new System.IO.StreamReader(filename);
                while ((line = file.ReadLine()) != null)
                {
                    //System.Console.WriteLine(line);
                    if (line.IndexOf("<EOH") >= 0) FoundEOH = true;
                    if (FoundEOH)
                    {
                        QsoLine = QsoLine + line;
                    }
                    else
                    {
                        HeaderArray.Add(line);
                    }
                    if (line.IndexOf("<EOR>") >= 0)
                    {
                        AdifArray.Add(QsoLine);
                        QsoLine = "";
                    }
                }
                file.Close();
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
                return false;
            }
        }

        public void AdifclearArray()
        {
            AdifArray.Clear();
        }

        public static int AdifCount()
        {
            return AdifArray.Count;
        }

        public bool AdifFind(string qsodetails)
        {
            if (AdifArray.Contains(qsodetails))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AdifWriteFile(string FileName)
        {

            string AllAdifLines = "";
            try
            {
                foreach (string AdifLine in AdifArray)
                {
                    AllAdifLines = AllAdifLines + AdifLine + Environment.NewLine;
                }
                File.WriteAllText(FileName, AllAdifLines);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //public class AdifFieldObjects
        //{

        //    public string ADDRESS { get; set; }
        //    public string ADDRESS_INTL { get; set; }
        //    public string AGE { get; set; }
        //    public string A_INDEX { get; set; }
        //    public string ANT_AZ { get; set; }
        //    public string ANT_EL { get; set; }
        //    public string ANT_PATH { get; set; }
        //    public string ARRL_SECT { get; set; }
        //    public string AWARD_SUBMITTED { get; set; }
        //    public string AWARD_GRANTED { get; set; }
        //    public string BAND { get; set; }
        //    public string BAND_RX { get; set; }
        //    public string CALL { get; set; }
        //    public string CHECK { get; set; }
        //    public string CLASS { get; set; }
        //    public string CLUBLOG_QSO_UPLOAD_DATE { get; set; }
        //    public string CLUBLOG_QSO_UPLOAD_STATUS { get; set; }
        //    public string CNTY { get; set; }
        //    public string COMMENT { get; set; }
        //    public string COMMENT_INTL { get; set; }
        //    public string CONT { get; set; }
        //    public string CONTACTED_OP { get; set; }
        //    public string CONTEST_ID { get; set; }
        //    public string COUNTRY { get; set; }
        //    public string COUNTRY_INTL { get; set; }
        //    public string CQZ { get; set; }
        //    public string CREDIT_SUBMITTED { get; set; }
        //    public string CREDIT_GRANTED { get; set; }
        //    public string DARC_DOK { get; set; }
        //    public string DISTANCE { get; set; }
        //    public string DXCC { get; set; }
        //    public string EMAIL { get; set; }
        //    public string EQ_CALL { get; set; }
        //    public string EQSL_QSLRDATE { get; set; }
        //    public string EQSL_QSLSDATE { get; set; }
        //    public string EQSL_QSL_RCVD { get; set; }
        //    public string EQSL_QSL_SENT { get; set; }
        //    public string FISTS { get; set; }
        //    public string FISTS_CC { get; set; }
        //    public string FORCE_INIT { get; set; }
        //    public string FREQ { get; set; }
        //    public string FREQ_RX { get; set; }
        //    public string GRIDSQUARE { get; set; }
        //    public string GUEST_OP { get; set; }
        //    public string HRDLOG_QSO_UPLOAD_DATE { get; set; }
        //    public string HRDLOG_QSO_UPLOAD_STATUS { get; set; }
        //    public string IOTA { get; set; }
        //    public string IOTA_ISLAND_ID { get; set; }
        //    public string ITUZ { get; set; }
        //    public string K_INDEX { get; set; }
        //    public string LAT { get; set; }
        //    public string LON { get; set; }
        //    public string LOTW_QSLRDATE { get; set; }
        //    public string LOTW_QSLSDATE { get; set; }
        //    public string LOTW_QSL_RCVD { get; set; }
        //    public string LOTW_QSL_SENT { get; set; }
        //    public string MAX_BURSTS { get; set; }
        //    public string MODE { get; set; }
        //    public string MS_SHOWER { get; set; }
        //    public string MY_ANTENNA { get; set; }
        //    public string MY_ANTENNA_INTL { get; set; }
        //    public string MY_CITY { get; set; }
        //    public string MY_CITY_INTL { get; set; }
        //    public string MY_CNTY { get; set; }
        //    public string MY_COUNTRY { get; set; }
        //    public string MY_COUNTRY_INTL { get; set; }
        //    public string MY_CQ_ZONE { get; set; }
        //    public string MY_DXCC { get; set; }
        //    public string MY_FISTS { get; set; }
        //    public string MY_GRIDSQUARE { get; set; }
        //    public string MY_IOTA { get; set; }
        //    public string MY_IOTA_ISLAND_ID { get; set; }
        //    public string MY_ITU_ZONE { get; set; }
        //    public string MY_LAT { get; set; }
        //    public string MY_LON { get; set; }
        //    public string MY_NAME { get; set; }
        //    public string MY_NAME_INTL { get; set; }
        //    public string MY_POSTAL_CODE { get; set; }
        //    public string MY_POSTAL_CODE_INTL { get; set; }
        //    public string MY_RIG { get; set; }
        //    public string MY_RIG_INTL { get; set; }
        //    public string MY_SIG { get; set; }
        //    public string MY_SIG_INTL { get; set; }
        //    public string MY_SIG_INFO { get; set; }
        //    public string MY_SIG_INFO_INTL { get; set; }
        //    public string MY_SOTA_REF { get; set; }
        //    public string MY_STATE { get; set; }
        //    public string MY_STREET { get; set; }
        //    public string MY_STREET_INTL { get; set; }
        //    public string MY_USACA_COUNTIES { get; set; }
        //    public string MY_VUCC_GRIDS { get; set; }
        //    public string NAME { get; set; }
        //    public string NAME_INTL { get; set; }
        //    public string NOTES { get; set; }
        //    public string NOTES_INTL { get; set; }
        //    public string NR_BURSTS { get; set; }
        //    public string NR_PINGS { get; set; }
        //    public string OPERATOR { get; set; }
        //    public string OWNER_CALLSIGN { get; set; }
        //    public string PFX { get; set; }
        //    public string PRECEDENCE { get; set; }
        //    public string PROP_MODE { get; set; }
        //    public string PUBLIC_KEY { get; set; }
        //    public string QRZCOM_QSO_UPLOAD_DATE { get; set; }
        //    public string QRZCOM_QSO_UPLOAD_STATUS { get; set; }
        //    public string QSLMSG { get; set; }
        //    public string QSLMSG_INTL { get; set; }
        //    public string QSLRDATE { get; set; }
        //    public string QSLSDATE { get; set; }
        //    public string QSL_RCVD { get; set; }
        //    public string QSL_RCVD_VIA { get; set; }
        //    public string QSL_SENT { get; set; }
        //    public string QSL_SENT_VIA { get; set; }
        //    public string QSL_VIA { get; set; }
        //    public string QSO_COMPLETE { get; set; }
        //    public string QSO_DATE { get; set; }
        //    public string QSO_DATE_OFF { get; set; }
        //    public string QSO_RANDOM { get; set; }
        //    public string QTH { get; set; }
        //    public string QTH_INTL { get; set; }
        //    public string REGION { get; set; }
        //    public string RIG { get; set; }
        //    public string RIG_INTL { get; set; }
        //    public string RST_RCVD { get; set; }
        //    public string RST_SENT { get; set; }
        //    public string RX_PWR { get; set; }
        //    public string SAT_MODE { get; set; }
        //    public string SAT_NAME { get; set; }
        //    public string SFI { get; set; }
        //    public string SIG { get; set; }
        //    public string SIG_INTL { get; set; }
        //    public string SIG_INFO { get; set; }
        //    public string SIG_INFO_INTL { get; set; }
        //    public string SILENT_KEY { get; set; }
        //    public string SKCC { get; set; }
        //    public string SOTA_REF { get; set; }
        //    public string SRX { get; set; }
        //    public string SRX_STRING { get; set; }
        //    public string STATE { get; set; }
        //    public string STATION_CALLSIGN { get; set; }
        //    public string STX { get; set; }
        //    public string STX_STRING { get; set; }
        //    public string SUBMODE { get; set; }
        //    public string SWL { get; set; }
        //    public string TEN_TEN { get; set; }
        //    public string TIME_OFF { get; set; }
        //    public string TIME_ON { get; set; }
        //    public string TX_PWR { get; set; }
        //    public string UKSMG { get; set; }
        //    public string USACA_COUNTIES { get; set; }
        //    public string VE_PROV { get; set; }
        //    public string VUCC_GRIDS { get; set; }
        //    public string WEB { get; set; }
        //    //  N1MM
        //    public string APP_N1MM_POINTS { get; set; }
        //    public string APP_N1MM_RADIO_NR { get; set; }
        //    public string APP_N1MM_CONTINENT { get; set; }
        //    public string APP_N1MM_RUN1RUN2 { get; set; }
        //    public string APP_N1MM_RADIOINTERFACED { get; set; }
        //    public string APP_N1MM_ISORIGINAL { get; set; }
        //    public string APP_N1MM_NETBIOSNAME { get; set; }
        //    public string APP_N1MM_ISRUNQSO { get; set; }
        //    public string APP_N1MM_ID { get; set; }
        //    public string APP_N1MM_CLAIMEDQSO { get; set; }
        //    //eQSL.cc
        //    public string APP_EQSL_AG { get; set; }
        //}
    }
}


