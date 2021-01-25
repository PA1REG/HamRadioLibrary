using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.IO;
using System.Reflection;
using AdifLibrary;
using AdifProperties;

namespace AdiReadFile
{

    [Cmdlet(VerbsCommon.Get, "AdiReadFile")]
    [OutputType(typeof(string))]
    public class GetAdiReadFile : Cmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        [Alias("AdifFile")]
        public string FileName { get; set; }
        [Parameter(Position = 0, Mandatory = false)]
        [Alias("AdifHeader")]
        public SwitchParameter Header { get; set; }

        protected override void ProcessRecord()
        {
            //Boolean result = false;

            WriteVerbose("Input filename : " + FileName);
            SessionState ss = new SessionState();
            Directory.SetCurrentDirectory(ss.Path.CurrentFileSystemLocation.Path);
            FileName = Path.GetFullPath(FileName);
            WriteVerbose("Output (Full) filename : " + FileName);

            WriteVerbose("Check file exists.");
            if (!File.Exists(FileName))
            {
                WriteWarning("File not found : " + FileName);
                return;
            }
            WriteVerbose("Reading and fill the list.");
            HamRadioDeLuxeAdif.AdifReadFile(FileName);

            if (Header)
            {
                WriteVerbose("Reading Header.");
                foreach (string HeaderLine in HamRadioDeLuxeAdif.HeaderArray)
                {
                    WriteObject(HeaderLine);
                }

            }
            WriteVerbose("Total calls readed : " + HamRadioDeLuxeAdif.AdifCount());
            WriteVerbose("Reading list.");
            foreach (string AdifLine in HamRadioDeLuxeAdif.AdifArray)
            {
                // Access your properties using item.NazivProizvoda, etc. here

                var AdifElementsObj = HamRadioDeLuxeAdif.GetAllAdifElements(AdifLine);
                WriteObject(new ADIFProperties.AdifFieldObjects
                {
                    ADDRESS = AdifElementsObj.ADDRESS,
                    ADDRESS_INTL = AdifElementsObj.ADDRESS_INTL,
                    AGE = AdifElementsObj.AGE,
                    A_INDEX = AdifElementsObj.A_INDEX,
                    ANT_AZ = AdifElementsObj.ANT_AZ,
                    ANT_EL = AdifElementsObj.ANT_EL,
                    ANT_PATH = AdifElementsObj.ANT_PATH,
                    ARRL_SECT = AdifElementsObj.ARRL_SECT,
                    AWARD_SUBMITTED = AdifElementsObj.AWARD_SUBMITTED,
                    AWARD_GRANTED = AdifElementsObj.AWARD_GRANTED,
                    BAND = AdifElementsObj.BAND,
                    BAND_RX = AdifElementsObj.BAND_RX,
                    CALL = AdifElementsObj.CALL,
                    CHECK = AdifElementsObj.CHECK,
                    CLASS = AdifElementsObj.CLASS,
                    CLUBLOG_QSO_UPLOAD_DATE = AdifElementsObj.CLUBLOG_QSO_UPLOAD_DATE,
                    CLUBLOG_QSO_UPLOAD_STATUS = AdifElementsObj.CLUBLOG_QSO_UPLOAD_STATUS,
                    CNTY = AdifElementsObj.CNTY,
                    COMMENT = AdifElementsObj.COMMENT,
                    COMMENT_INTL = AdifElementsObj.COMMENT_INTL,
                    CONT = AdifElementsObj.CONT,
                    CONTACTED_OP = AdifElementsObj.CONTACTED_OP,
                    CONTEST_ID = AdifElementsObj.CONTEST_ID,
                    COUNTRY = AdifElementsObj.COUNTRY,
                    COUNTRY_INTL = AdifElementsObj.COUNTRY_INTL,
                    CQZ = AdifElementsObj.CQZ,
                    CREDIT_SUBMITTED = AdifElementsObj.CREDIT_SUBMITTED,
                    CREDIT_GRANTED = AdifElementsObj.CREDIT_GRANTED,
                    DARC_DOK = AdifElementsObj.DARC_DOK,
                    DISTANCE = AdifElementsObj.DISTANCE,
                    DXCC = AdifElementsObj.DXCC,
                    EMAIL = AdifElementsObj.EMAIL,
                    EQ_CALL = AdifElementsObj.EQ_CALL,
                    EQSL_QSLRDATE = AdifElementsObj.EQSL_QSLRDATE,
                    EQSL_QSLSDATE = AdifElementsObj.EQSL_QSLSDATE,
                    EQSL_QSL_RCVD = AdifElementsObj.EQSL_QSL_RCVD,
                    EQSL_QSL_SENT = AdifElementsObj.EQSL_QSL_SENT,
                    FISTS = AdifElementsObj.FISTS,
                    FISTS_CC = AdifElementsObj.FISTS_CC,
                    FORCE_INIT = AdifElementsObj.FORCE_INIT,
                    FREQ = AdifElementsObj.FREQ,
                    FREQ_RX = AdifElementsObj.FREQ_RX,
                    GRIDSQUARE = AdifElementsObj.GRIDSQUARE,
                    GUEST_OP = AdifElementsObj.GUEST_OP,
                    HRDLOG_QSO_UPLOAD_DATE = AdifElementsObj.HRDLOG_QSO_UPLOAD_DATE,
                    HRDLOG_QSO_UPLOAD_STATUS = AdifElementsObj.HRDLOG_QSO_UPLOAD_STATUS,
                    IOTA = AdifElementsObj.IOTA,
                    IOTA_ISLAND_ID = AdifElementsObj.IOTA_ISLAND_ID,
                    ITUZ = AdifElementsObj.ITUZ,
                    K_INDEX = AdifElementsObj.K_INDEX,
                    LAT = AdifElementsObj.LAT,
                    LON = AdifElementsObj.LON,
                    LOTW_QSLRDATE = AdifElementsObj.LOTW_QSLRDATE,
                    LOTW_QSLSDATE = AdifElementsObj.LOTW_QSLSDATE,
                    LOTW_QSL_RCVD = AdifElementsObj.LOTW_QSL_RCVD,
                    LOTW_QSL_SENT = AdifElementsObj.LOTW_QSL_SENT,
                    MAX_BURSTS = AdifElementsObj.MAX_BURSTS,
                    MODE = AdifElementsObj.MODE,
                    MS_SHOWER = AdifElementsObj.MS_SHOWER,
                    MY_ANTENNA = AdifElementsObj.MY_ANTENNA,
                    MY_ANTENNA_INTL = AdifElementsObj.MY_ANTENNA_INTL,
                    MY_CITY = AdifElementsObj.MY_CITY,
                    MY_CITY_INTL = AdifElementsObj.MY_CITY_INTL,
                    MY_CNTY = AdifElementsObj.MY_CNTY,
                    MY_COUNTRY = AdifElementsObj.MY_COUNTRY,
                    MY_COUNTRY_INTL = AdifElementsObj.MY_COUNTRY_INTL,
                    MY_CQ_ZONE = AdifElementsObj.MY_CQ_ZONE,
                    MY_DXCC = AdifElementsObj.MY_DXCC,
                    MY_FISTS = AdifElementsObj.MY_FISTS,
                    MY_GRIDSQUARE = AdifElementsObj.MY_GRIDSQUARE,
                    MY_IOTA = AdifElementsObj.MY_IOTA,
                    MY_IOTA_ISLAND_ID = AdifElementsObj.MY_IOTA_ISLAND_ID,
                    MY_ITU_ZONE = AdifElementsObj.MY_ITU_ZONE,
                    MY_LAT = AdifElementsObj.MY_LAT,
                    MY_LON = AdifElementsObj.MY_LON,
                    MY_NAME = AdifElementsObj.MY_NAME,
                    MY_NAME_INTL = AdifElementsObj.MY_NAME_INTL,
                    MY_POSTAL_CODE = AdifElementsObj.MY_POSTAL_CODE,
                    MY_POSTAL_CODE_INTL = AdifElementsObj.MY_POSTAL_CODE_INTL,
                    MY_RIG = AdifElementsObj.MY_RIG,
                    MY_RIG_INTL = AdifElementsObj.MY_RIG_INTL,
                    MY_SIG = AdifElementsObj.MY_SIG,
                    MY_SIG_INTL = AdifElementsObj.MY_SIG_INTL,
                    MY_SIG_INFO = AdifElementsObj.MY_SIG_INFO,
                    MY_SIG_INFO_INTL = AdifElementsObj.MY_SIG_INFO_INTL,
                    MY_SOTA_REF = AdifElementsObj.MY_SOTA_REF,
                    MY_STATE = AdifElementsObj.MY_STATE,
                    MY_STREET = AdifElementsObj.MY_STREET,
                    MY_STREET_INTL = AdifElementsObj.MY_STREET_INTL,
                    MY_USACA_COUNTIES = AdifElementsObj.MY_USACA_COUNTIES,
                    MY_VUCC_GRIDS = AdifElementsObj.MY_VUCC_GRIDS,
                    NAME = AdifElementsObj.NAME,
                    NAME_INTL = AdifElementsObj.NAME_INTL,
                    NOTES = AdifElementsObj.NOTES,
                    NOTES_INTL = AdifElementsObj.NOTES_INTL,
                    NR_BURSTS = AdifElementsObj.NR_BURSTS,
                    NR_PINGS = AdifElementsObj.NR_PINGS,
                    OPERATOR = AdifElementsObj.OPERATOR,
                    OWNER_CALLSIGN = AdifElementsObj.OWNER_CALLSIGN,
                    PFX = AdifElementsObj.PFX,
                    PRECEDENCE = AdifElementsObj.PRECEDENCE,
                    PROP_MODE = AdifElementsObj.PROP_MODE,
                    PUBLIC_KEY = AdifElementsObj.PUBLIC_KEY,
                    QRZCOM_QSO_UPLOAD_DATE = AdifElementsObj.QRZCOM_QSO_UPLOAD_DATE,
                    QRZCOM_QSO_UPLOAD_STATUS = AdifElementsObj.QRZCOM_QSO_UPLOAD_STATUS,
                    QSLMSG = AdifElementsObj.QSLMSG,
                    QSLMSG_INTL = AdifElementsObj.QSLMSG_INTL,
                    QSLRDATE = AdifElementsObj.QSLRDATE,
                    QSLSDATE = AdifElementsObj.QSLSDATE,
                    QSL_RCVD = AdifElementsObj.QSL_RCVD,
                    QSL_RCVD_VIA = AdifElementsObj.QSL_RCVD_VIA,
                    QSL_SENT = AdifElementsObj.QSL_SENT,
                    QSL_SENT_VIA = AdifElementsObj.QSL_SENT_VIA,
                    QSL_VIA = AdifElementsObj.QSL_VIA,
                    QSO_COMPLETE = AdifElementsObj.QSO_COMPLETE,
                    QSO_DATE = AdifElementsObj.QSO_DATE,
                    QSO_DATE_OFF = AdifElementsObj.QSO_DATE_OFF,
                    QSO_RANDOM = AdifElementsObj.QSO_RANDOM,
                    QTH = AdifElementsObj.QTH,
                    QTH_INTL = AdifElementsObj.QTH_INTL,
                    REGION = AdifElementsObj.REGION,
                    RIG = AdifElementsObj.RIG,
                    RIG_INTL = AdifElementsObj.RIG_INTL,
                    RST_RCVD = AdifElementsObj.RST_RCVD,
                    RST_SENT = AdifElementsObj.RST_SENT,
                    RX_PWR = AdifElementsObj.RX_PWR,
                    SAT_MODE = AdifElementsObj.SAT_MODE,
                    SAT_NAME = AdifElementsObj.SAT_NAME,
                    SFI = AdifElementsObj.SFI,
                    SIG = AdifElementsObj.SIG,
                    SIG_INTL = AdifElementsObj.SIG_INTL,
                    SIG_INFO = AdifElementsObj.SIG_INFO,
                    SIG_INFO_INTL = AdifElementsObj.SIG_INFO_INTL,
                    SILENT_KEY = AdifElementsObj.SILENT_KEY,
                    SKCC = AdifElementsObj.SKCC,
                    SOTA_REF = AdifElementsObj.SOTA_REF,
                    SRX = AdifElementsObj.SRX,
                    SRX_STRING = AdifElementsObj.SRX_STRING,
                    STATE = AdifElementsObj.STATE,
                    STATION_CALLSIGN = AdifElementsObj.STATION_CALLSIGN,
                    STX = AdifElementsObj.STX,
                    STX_STRING = AdifElementsObj.STX_STRING,
                    SUBMODE = AdifElementsObj.SUBMODE,
                    SWL = AdifElementsObj.SWL,
                    TEN_TEN = AdifElementsObj.TEN_TEN,
                    TIME_OFF = AdifElementsObj.TIME_OFF,
                    TIME_ON = AdifElementsObj.TIME_ON,
                    TX_PWR = AdifElementsObj.TX_PWR,
                    UKSMG = AdifElementsObj.UKSMG,
                    USACA_COUNTIES = AdifElementsObj.USACA_COUNTIES,
                    VE_PROV = AdifElementsObj.VE_PROV,
                    VUCC_GRIDS = AdifElementsObj.VUCC_GRIDS,
                    WEB = AdifElementsObj.WEB,
                    APP_N1MM_POINTS = AdifElementsObj.APP_N1MM_POINTS,
                    APP_N1MM_RADIO_NR = AdifElementsObj.APP_N1MM_RADIO_NR,
                    APP_N1MM_CONTINENT = AdifElementsObj.APP_N1MM_CONTINENT,
                    APP_N1MM_RUN1RUN2 = AdifElementsObj.APP_N1MM_RUN1RUN2,
                    APP_N1MM_RADIOINTERFACED = AdifElementsObj.APP_N1MM_RADIOINTERFACED,
                    APP_N1MM_ISORIGINAL = AdifElementsObj.APP_N1MM_ISORIGINAL,
                    APP_N1MM_NETBIOSNAME = AdifElementsObj.APP_N1MM_NETBIOSNAME,
                    APP_N1MM_ISRUNQSO = AdifElementsObj.APP_N1MM_ISRUNQSO,
                    APP_N1MM_ID = AdifElementsObj.APP_N1MM_ID,
                    APP_N1MM_CLAIMEDQSO = AdifElementsObj.APP_N1MM_CLAIMEDQSO,
                    APP_EQSL_AG = AdifElementsObj.APP_EQSL_AG
                });
            }
        }
    }


}
