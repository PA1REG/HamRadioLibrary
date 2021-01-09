using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CsvHelper;
using AdifLibrary;
using AdifObjects;
using System.Globalization;
using System.Collections;
using System.Reflection;
using System.Management.Automation;
using Microsoft.PowerShell.Commands;


namespace CsvLibrary
{
    public class CsvUtils
    {

        public static dynamic GetAdifElements(string AdifName, object AdifRecord)
        {


            //        // <CALL:5>DK6JM<QSO_DATE:8:D>19980411<TIME_ON:4>1711<BAND:3>40M<MODE:3>SSB<RST_SENT:2>59<RST_RCVD:0><QSL_SENT:1>Y
            //        // <QSL_SENT_VIA:1>E<APP_EQSL_AG:1>Y<GRIDSQUARE:6>jo31jg<EOR>

            //Console.WriteLine(nameof(AdifRecord));
            //Console.WriteLine(AdifRecord);
            string ReturnElement = null;
            if (AdifRecord is null)
            {
                ReturnElement = null;
            }
            else
            {
                ReturnElement = "<" + AdifName + ":" + AdifRecord.ToString().Length + ">" + AdifRecord.ToString();

            }
            return ReturnElement;

        }

        public static ArrayList AdiArray = new ArrayList();

        public static void CsvReaderQso(string pathToCsvFile, bool SwlStation, bool QslMsgAsComment, bool AllFields)
        {
            ReadBandList();
            string AllElements = "";
            using (var reader = new StreamReader(pathToCsvFile))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {


                //                ADIF 3 Export from eQSL.cc
                //Received eQSLs for PA1REG
                //for QSOs between 01 - Jan - 1993 and 31 - Dec - 2035
                //Generated on Friday, December 25, 2020 at 11:55:27 AM UTC
                //< PROGRAMID:21 > eQSL.cc DownloadInBox
                //< ADIF_Ver:5 > 3.1.0
                //< EOH >

                //DateTime.Now.ToString("dddd, dd MMMM yyyy")	Friday, 29 May 2015 05:50 AM

                AdiArray.Clear();
                AdiArray.Add("ADIF 3 Export from HamRadioLibrary");
                AdiArray.Add("Created QSLs with PowerShell");
                CultureInfo culture = new CultureInfo("en-GB");
                AdiArray.Add("Generated on " + DateTime.UtcNow.ToString("dddd, MMMM dd, yyyy 'at' hh:mm:ss tt 'UTC'", culture));
                var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                var modulename = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                AdiArray.Add(GetAdifElements("PROGRAMID", modulename));
                AdiArray.Add(GetAdifElements("PROGRAMVERSION", version));
                AdiArray.Add(GetAdifElements("ADIF_VERS", "3.1.1"));
                AdiArray.Add("<EOH>");

                csv.Read();
                try
                {
                    csv.ReadHeader();
                }
                catch (Exception)
                {
                    return;
                }
                csv.Configuration.MissingFieldFound = null;

                while (csv.Read())
                {
                    var record = csv.GetRecord<AdifFields.AdifFieldObjects>();
                    AllElements = "";
                    AllElements += GetAdifElements(nameof(record.ADDRESS), record.ADDRESS);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.ADDRESS_INTL), record.ADDRESS_INTL); };
                    AllElements += GetAdifElements(nameof(record.AGE), record.AGE);
                    AllElements += GetAdifElements(nameof(record.A_INDEX), record.A_INDEX);
                    AllElements += GetAdifElements(nameof(record.ANT_AZ), record.ANT_AZ);
                    AllElements += GetAdifElements(nameof(record.ANT_EL), record.ANT_EL);
                    AllElements += GetAdifElements(nameof(record.ANT_PATH), record.ANT_PATH);
                    AllElements += GetAdifElements(nameof(record.ARRL_SECT), record.ARRL_SECT);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.AWARD_SUBMITTED), record.AWARD_SUBMITTED); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.AWARD_GRANTED), record.AWARD_GRANTED); };
                    AllElements += GetAdifElements(nameof(record.BAND), record.BAND);
                    AllElements += GetAdifElements(nameof(record.BAND_RX), record.BAND_RX);
                    AllElements += GetAdifElements(nameof(record.CALL), record.CALL);
                    AllElements += GetAdifElements(nameof(record.CHECK), record.CHECK);
                    AllElements += GetAdifElements(nameof(record.CLASS), record.CLASS);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.CLUBLOG_QSO_UPLOAD_DATE), record.CLUBLOG_QSO_UPLOAD_DATE); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.CLUBLOG_QSO_UPLOAD_STATUS), record.CLUBLOG_QSO_UPLOAD_STATUS); };
                    AllElements += GetAdifElements(nameof(record.CNTY), record.CNTY);
                    if (QslMsgAsComment)
                    {
                        AllElements += GetAdifElements(nameof(record.COMMENT), record.QSLMSG);
                    }
                    else
                    {
                        AllElements += GetAdifElements(nameof(record.COMMENT), record.COMMENT);
                    }
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.COMMENT_INTL), record.COMMENT_INTL); };
                    AllElements += GetAdifElements(nameof(record.CONT), record.CONT);
                    AllElements += GetAdifElements(nameof(record.CONTACTED_OP), record.CONTACTED_OP);
                    AllElements += GetAdifElements(nameof(record.CONTEST_ID), record.CONTEST_ID);
                    AllElements += GetAdifElements(nameof(record.COUNTRY), record.COUNTRY);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.COUNTRY_INTL), record.COUNTRY_INTL); };
                    AllElements += GetAdifElements(nameof(record.CQZ), record.CQZ);
                    AllElements += GetAdifElements(nameof(record.CREDIT_SUBMITTED), record.CREDIT_SUBMITTED);
                    AllElements += GetAdifElements(nameof(record.CREDIT_GRANTED), record.CREDIT_GRANTED);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.DARC_DOK), record.DARC_DOK); };
                    AllElements += GetAdifElements(nameof(record.DISTANCE), record.DISTANCE);
                    AllElements += GetAdifElements(nameof(record.DXCC), record.DXCC);
                    AllElements += GetAdifElements(nameof(record.EMAIL), record.EMAIL);
                    AllElements += GetAdifElements(nameof(record.EQ_CALL), record.EQ_CALL);
                    AllElements += GetAdifElements(nameof(record.EQSL_QSLRDATE), record.EQSL_QSLRDATE);
                    AllElements += GetAdifElements(nameof(record.EQSL_QSLSDATE), record.EQSL_QSLSDATE);
                    AllElements += GetAdifElements(nameof(record.EQSL_QSL_RCVD), record.EQSL_QSL_RCVD);
                    AllElements += GetAdifElements(nameof(record.EQSL_QSL_SENT), record.EQSL_QSL_SENT);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.FISTS), record.FISTS); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.FISTS_CC), record.FISTS_CC); };
                    AllElements += GetAdifElements(nameof(record.FORCE_INIT), record.FORCE_INIT);
                    if (record.FREQ == null || record.FREQ == "")
                    {
                        string FrequentieFromBand = FindLowerFrequentie(record.BAND);
                        AllElements += GetAdifElements(nameof(record.FREQ), FrequentieFromBand);
                    }
                    else
                    {
                        AllElements += GetAdifElements(nameof(record.FREQ), record.FREQ);
                    }
                    AllElements += GetAdifElements(nameof(record.FREQ_RX), record.FREQ_RX);
                    AllElements += GetAdifElements(nameof(record.GRIDSQUARE), record.GRIDSQUARE);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.GUEST_OP), record.GUEST_OP); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.HRDLOG_QSO_UPLOAD_DATE), record.HRDLOG_QSO_UPLOAD_DATE); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.HRDLOG_QSO_UPLOAD_STATUS), record.HRDLOG_QSO_UPLOAD_STATUS); };
                    AllElements += GetAdifElements(nameof(record.IOTA), record.IOTA);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.IOTA_ISLAND_ID), record.IOTA_ISLAND_ID); };
                    AllElements += GetAdifElements(nameof(record.ITUZ), record.ITUZ);
                    AllElements += GetAdifElements(nameof(record.K_INDEX), record.K_INDEX);
                    AllElements += GetAdifElements(nameof(record.LAT), record.LAT);
                    AllElements += GetAdifElements(nameof(record.LON), record.LON);
                    AllElements += GetAdifElements(nameof(record.LOTW_QSLRDATE), record.LOTW_QSLRDATE);
                    AllElements += GetAdifElements(nameof(record.LOTW_QSLSDATE), record.LOTW_QSLSDATE);
                    AllElements += GetAdifElements(nameof(record.LOTW_QSL_RCVD), record.LOTW_QSL_RCVD);
                    AllElements += GetAdifElements(nameof(record.LOTW_QSL_SENT), record.LOTW_QSL_SENT);
                    AllElements += GetAdifElements(nameof(record.MAX_BURSTS), record.MAX_BURSTS);
                    AllElements += GetAdifElements(nameof(record.MODE), record.MODE);
                    AllElements += GetAdifElements(nameof(record.MS_SHOWER), record.MS_SHOWER);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.MY_ANTENNA), record.MY_ANTENNA); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.MY_ANTENNA_INTL), record.MY_ANTENNA_INTL); };
                    AllElements += GetAdifElements(nameof(record.MY_CITY), record.MY_CITY);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.MY_CITY_INTL), record.MY_CITY_INTL); };
                    AllElements += GetAdifElements(nameof(record.MY_CNTY), record.MY_CNTY);
                    AllElements += GetAdifElements(nameof(record.MY_COUNTRY), record.MY_COUNTRY);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.MY_COUNTRY_INTL), record.MY_COUNTRY_INTL); };
                    AllElements += GetAdifElements(nameof(record.MY_CQ_ZONE), record.MY_CQ_ZONE);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.MY_DXCC), record.MY_DXCC); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.MY_FISTS), record.MY_FISTS); };
                    AllElements += GetAdifElements(nameof(record.MY_GRIDSQUARE), record.MY_GRIDSQUARE);
                    AllElements += GetAdifElements(nameof(record.MY_IOTA), record.MY_IOTA);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.MY_IOTA_ISLAND_ID), record.MY_IOTA_ISLAND_ID); };
                    AllElements += GetAdifElements(nameof(record.MY_ITU_ZONE), record.MY_ITU_ZONE);
                    AllElements += GetAdifElements(nameof(record.MY_LAT), record.MY_LAT);
                    AllElements += GetAdifElements(nameof(record.MY_LON), record.MY_LON);
                    AllElements += GetAdifElements(nameof(record.MY_NAME), record.MY_NAME);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.MY_NAME_INTL), record.MY_NAME_INTL); };
                    AllElements += GetAdifElements(nameof(record.MY_POSTAL_CODE), record.MY_POSTAL_CODE);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.MY_POSTAL_CODE_INTL), record.MY_POSTAL_CODE_INTL); };
                    AllElements += GetAdifElements(nameof(record.MY_RIG), record.MY_RIG);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.MY_RIG_INTL), record.MY_RIG_INTL); };
                    AllElements += GetAdifElements(nameof(record.MY_SIG), record.MY_SIG);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.MY_SIG_INTL), record.MY_SIG_INTL); };
                    AllElements += GetAdifElements(nameof(record.MY_SIG_INFO), record.MY_SIG_INFO);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.MY_SIG_INFO_INTL), record.MY_SIG_INFO_INTL); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.MY_SOTA_REF), record.MY_SOTA_REF); };
                    AllElements += GetAdifElements(nameof(record.MY_STATE), record.MY_STATE);
                    AllElements += GetAdifElements(nameof(record.MY_STREET), record.MY_STREET);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.MY_STREET_INTL), record.MY_STREET_INTL); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.MY_USACA_COUNTIES), record.MY_USACA_COUNTIES); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.MY_VUCC_GRIDS), record.MY_VUCC_GRIDS); };
                    AllElements += GetAdifElements(nameof(record.NAME), record.NAME);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.NAME_INTL), record.NAME_INTL); };
                    AllElements += GetAdifElements(nameof(record.NOTES), record.NOTES);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.NOTES_INTL), record.NOTES_INTL); };
                    AllElements += GetAdifElements(nameof(record.NR_BURSTS), record.NR_BURSTS);
                    AllElements += GetAdifElements(nameof(record.NR_PINGS), record.NR_PINGS);
                    AllElements += GetAdifElements(nameof(record.OPERATOR), record.OPERATOR);
                    AllElements += GetAdifElements(nameof(record.OWNER_CALLSIGN), record.OWNER_CALLSIGN);
                    AllElements += GetAdifElements(nameof(record.PFX), record.PFX);
                    AllElements += GetAdifElements(nameof(record.PRECEDENCE), record.PRECEDENCE);
                    AllElements += GetAdifElements(nameof(record.PROP_MODE), record.PROP_MODE);
                    AllElements += GetAdifElements(nameof(record.PUBLIC_KEY), record.PUBLIC_KEY);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.QRZCOM_QSO_UPLOAD_DATE), record.QRZCOM_QSO_UPLOAD_DATE); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.QRZCOM_QSO_UPLOAD_STATUS), record.QRZCOM_QSO_UPLOAD_STATUS); };
                    AllElements += GetAdifElements(nameof(record.QSLMSG), record.QSLMSG);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.QSLMSG_INTL), record.QSLMSG_INTL); };
                    AllElements += GetAdifElements(nameof(record.QSLRDATE), record.QSLRDATE);
                    AllElements += GetAdifElements(nameof(record.QSLSDATE), record.QSLSDATE);
                    AllElements += GetAdifElements(nameof(record.QSL_RCVD), record.QSL_RCVD);
                    AllElements += GetAdifElements(nameof(record.QSL_RCVD_VIA), record.QSL_RCVD_VIA);
                    AllElements += GetAdifElements(nameof(record.QSL_SENT), record.QSL_SENT);
                    AllElements += GetAdifElements(nameof(record.QSL_SENT_VIA), record.QSL_SENT_VIA);
                    AllElements += GetAdifElements(nameof(record.QSL_VIA), record.QSL_VIA);
                    AllElements += GetAdifElements(nameof(record.QSO_COMPLETE), record.QSO_COMPLETE);
                    AllElements += GetAdifElements(nameof(record.QSO_DATE), record.QSO_DATE);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.QSO_DATE_OFF), record.QSO_DATE_OFF); };
                    AllElements += GetAdifElements(nameof(record.QSO_RANDOM), record.QSO_RANDOM);
                    AllElements += GetAdifElements(nameof(record.QTH), record.QTH);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.QTH_INTL), record.QTH_INTL); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.REGION), record.REGION); };
                    AllElements += GetAdifElements(nameof(record.RIG), record.RIG);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.RIG_INTL), record.RIG_INTL); };
                    AllElements += GetAdifElements(nameof(record.RST_RCVD), record.RST_RCVD);
                    AllElements += GetAdifElements(nameof(record.RST_SENT), record.RST_SENT);
                    AllElements += GetAdifElements(nameof(record.RX_PWR), record.RX_PWR);
                    AllElements += GetAdifElements(nameof(record.SAT_MODE), record.SAT_MODE);
                    AllElements += GetAdifElements(nameof(record.SAT_NAME), record.SAT_NAME);
                    AllElements += GetAdifElements(nameof(record.SFI), record.SFI);
                    AllElements += GetAdifElements(nameof(record.SIG), record.SIG);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.SIG_INTL), record.SIG_INTL); };
                    AllElements += GetAdifElements(nameof(record.SIG_INFO), record.SIG_INFO);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.SIG_INFO_INTL), record.SIG_INFO_INTL); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.SILENT_KEY), record.SILENT_KEY); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.SKCC), record.SKCC); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.SOTA_REF), record.SOTA_REF); };
                    AllElements += GetAdifElements(nameof(record.SRX), record.SRX);
                    AllElements += GetAdifElements(nameof(record.SRX_STRING), record.SRX_STRING);
                    AllElements += GetAdifElements(nameof(record.STATE), record.STATE);
                    AllElements += GetAdifElements(nameof(record.STATION_CALLSIGN), record.STATION_CALLSIGN);
                    AllElements += GetAdifElements(nameof(record.STX), record.STX);
                    AllElements += GetAdifElements(nameof(record.STX_STRING), record.STX_STRING);
                    AllElements += GetAdifElements(nameof(record.SUBMODE), record.SUBMODE);
                    if (SwlStation)
                    {
                        AllElements += GetAdifElements(nameof(record.SWL), "1");
                    }
                    else
                    {
                        AllElements += GetAdifElements(nameof(record.SWL), record.SWL);
                    }
                    AllElements += GetAdifElements(nameof(record.TEN_TEN), record.TEN_TEN);
                    AllElements += GetAdifElements(nameof(record.TIME_OFF), record.TIME_OFF);
                    AllElements += GetAdifElements(nameof(record.TIME_ON), record.TIME_ON);
                    AllElements += GetAdifElements(nameof(record.TX_PWR), record.TX_PWR);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.UKSMG), record.UKSMG); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.USACA_COUNTIES), record.USACA_COUNTIES); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.VE_PROV), record.VE_PROV); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.VUCC_GRIDS), record.VUCC_GRIDS); };
                    AllElements += GetAdifElements(nameof(record.WEB), record.WEB);
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.APP_N1MM_POINTS), record.APP_N1MM_POINTS); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.APP_N1MM_RADIO_NR), record.APP_N1MM_RADIO_NR); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.APP_N1MM_CONTINENT), record.APP_N1MM_CONTINENT); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.APP_N1MM_RUN1RUN2), record.APP_N1MM_RUN1RUN2); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.APP_N1MM_RADIOINTERFACED), record.APP_N1MM_RADIOINTERFACED); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.APP_N1MM_ISORIGINAL), record.APP_N1MM_ISORIGINAL); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.APP_N1MM_NETBIOSNAME), record.APP_N1MM_NETBIOSNAME); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.APP_N1MM_ISRUNQSO), record.APP_N1MM_ISRUNQSO); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.APP_N1MM_ID), record.APP_N1MM_ID); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.APP_N1MM_CLAIMEDQSO), record.APP_N1MM_CLAIMEDQSO); };
                    if (AllFields) { AllElements += GetAdifElements(nameof(record.APP_EQSL_AG), record.APP_EQSL_AG); };
                    AllElements += "<EOR>";
                    //Console.WriteLine(AllElements);
                    AdiArray.Add(AllElements);

                    // Console.ReadLine();
                }
            }
        }

        public static void AdiWriteQso(string FileName)
        {

            string AllAdifLines = "";
            try
            {
                foreach (string AdifLine in AdiArray)
                {
                    AllAdifLines = AllAdifLines + AdifLine + Environment.NewLine;
                }
                File.WriteAllText(FileName, AllAdifLines);
                //               return true;
            }
            catch
            {
                //                return false;
            }
        }

        public static List<BandEnumerationFieldsObjects> BandEnumerationList = new List<BandEnumerationFieldsObjects>();

        public static void ReadBandList()
        {

            BandEnumerationList.Clear();
            //System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames(); 
            // https://stackoverflow.com/questions/55958901/csvhelper-barfing-on-bad-dates-on-ef-migration-with-configuration-seed
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "HamRadioLibrary.Resources.BandEnumeration.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                    csvReader.Configuration.HeaderValidated = null;
                    csvReader.Configuration.MissingFieldFound = null;
                    csvReader.Configuration.Delimiter = ";";
                    csvReader.Read();
                    csvReader.ReadHeader();

                    while (csvReader.Read())
                    {
                        var record = csvReader.GetRecord<BandEnumerationFieldsObjects>();
                        BandEnumerationFieldsObjects BandEnumList = new BandEnumerationFieldsObjects();
                        BandEnumList.BAND = csvReader["BAND"].ToString();
                        BandEnumList.LOWERFREQUENTIE = csvReader["LOWERFREQUENTIE"].ToString();
                        BandEnumList.HIGHERFREQUENTIE = csvReader["HIGHERFREQUENTIE"].ToString();
                        BandEnumerationList.Add(BandEnumList);
                        //string Vpcr = csvReader.GetField("BAND");
                        //Console.WriteLine(record.BAND);
                    }
                }
            }


            //Assembly asm = Assembly.GetExecutingAssembly();

            ////will loop tru all assembly files
            //foreach (string assembly in asm.GetManifestResourceNames())
            //{
            //   Console.WriteLine  (assembly);
            //}

            ////will load all the resource names in the string array
            //string[] assemblies = asm.GetManifestResourceNames();

            ////will load specified assembly file to the Stream
            //System.IO.Stream strm = asm.GetManifestResourceStream("Blogs.images.image1.JPG");


        }

        public static string FindLowerFrequentie(string Band)
        {
            string ReturnFrequentie = null;
            foreach (BandEnumerationFieldsObjects FieldList in BandEnumerationList)
            {
                if (FieldList.BAND == Band.ToLower())
                {
                    ReturnFrequentie = FieldList.LOWERFREQUENTIE;
                    break;
                }
            }
            return ReturnFrequentie;
        }

        public static string Check6Digits(string CheckString)
        {
            if (CheckString == null) { return ""; };
            return CheckString.PadLeft(6, '0');
        }

        //public class SwlHrdFieldsObjects
        //{
        //    public string CALL { get; set; }
        //    public string QSO_DATE { get; set; }
        //    public string TIME_ON { get; set; }
        //    public string TIME_OFF { get; set; }
        //    public string BAND { get; set; }
        //    public string MODE { get; set; }
        //    public string RST_SENT { get; set; }
        //    public string RST_RCVD { get; set; }
        //    public string QSL_SENT { get; set; }
        //    public string QSL_SENT_VIA { get; set; }
        //    public string GRIDSQUARE { get; set; }
        //    public string STATION_CALLSIGN { get; set; }
        //    public string FREQ { get; set; }
        //    public string CONTEST_ID { get; set; }
        //    public string OPERATOR { get; set; }
        //    public string CQZ { get; set; }
        //    public string STX { get; set; }
        //    public string SWL { get; set; }
        //    public string QSLMSG { get; set; }
        //}


        public class BandEnumerationFieldsObjects
        {
            public string BAND { get; set; }
            public string LOWERFREQUENTIE { get; set; }
            public string HIGHERFREQUENTIE { get; set; }
        }

    }
}
