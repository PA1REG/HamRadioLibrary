using System;


namespace HamRadioDeluxeProperties
{
    public class HRDProperties
    {

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
            public string KEY { get; set; }
        }

        public class HrdIndexObjects
        {
            public string INDEX_NAME { get; set; }
            public string INDEX_COLUMNS { get; set; }
            public Int32 CARDINALITY { get; set; }
        }

        public class ReportBandObjects
        {
            public string BAND { get; set; }
            public string WORKED { get; set; }
        }

        public class ReportModeObjects
        {
            public string MODE { get; set; }
            public string SUBMODE { get; set; }
            public string WORKED { get; set; }
        }

        public class ReportBandModeObjects
        {
            public string BAND { get; set; }
            public string MODE { get; set; }
            public string SUBMODE { get; set; }
            public string WORKED { get; set; }
        }

        public class ReportYearBandModeObjects
        {
            public string YEAR { get; set; }
            public string BAND { get; set; }
            public string MODE { get; set; }
            public string SUBMODE { get; set; }
            public string WORKED { get; set; }
        }

        public class ReportQsoObjects

        {
            public string WORKED { get; set; }
            public string OM { get; set; }
            public string SWL { get; set; }
        }

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

        public class ReportYearModeProperty
        {
            public string YEAR { get; set; }
            public string AM { get; set; }
            public string ARDOP { get; set; }
            public string ATV { get; set; }
            public string C4FM { get; set; }
            public string CHIP { get; set; }
            public string CLO { get; set; }
            public string CONTESTI { get; set; }
            public string CW { get; set; }
            public string DIGITALVOICE { get; set; }
            public string DOMINO { get; set; }
            public string DSTAR { get; set; }
            public string FAX { get; set; }
            public string FM { get; set; }
            public string FSK441 { get; set; }
            public string FT8 { get; set; }
            public string HELL { get; set; }
            public string ISCAT { get; set; }
            public string JT4 { get; set; }
            public string JT6M { get; set; }
            public string JT9 { get; set; }
            public string JT44 { get; set; }
            public string JT65 { get; set; }
            public string MFSK { get; set; }
            public string MSK144 { get; set; }
            public string MT63 { get; set; }
            public string OLIVIA { get; set; }
            public string OPERA { get; set; }
            public string PAC { get; set; }
            public string PAX { get; set; }
            public string PKT { get; set; }
            public string PSK { get; set; }
            public string PSK2K { get; set; }
            public string Q15 { get; set; }
            public string QRA64 { get; set; }
            public string ROS { get; set; }
            public string RTTY { get; set; }
            public string RTTYM { get; set; }
            public string SSB { get; set; }
            public string SSTV { get; set; }
            public string T10 { get; set; }
            public string THOR { get; set; }
            public string THRB { get; set; }
            public string TOR { get; set; }
            public string V4 { get; set; }
            public string VOI { get; set; }
            public string WINMOR { get; set; }
            public string WSPR { get; set; }
            public string AMTORFEC { get; set; }
            public string ASCI { get; set; }
            public string CHIP64 { get; set; }
            public string CHIP128 { get; set; }
            public string DOMINOF { get; set; }
            public string FMHELL { get; set; }
            public string FSK31 { get; set; }
            public string GTOR { get; set; }
            public string HELL80 { get; set; }
            public string HFSK { get; set; }
            public string JT4A { get; set; }
            public string JT4B { get; set; }
            public string JT4C { get; set; }
            public string JT4D { get; set; }
            public string JT4E { get; set; }
            public string JT4F { get; set; }
            public string JT4G { get; set; }
            public string JT65A { get; set; }
            public string JT65B { get; set; }
            public string JT65C { get; set; }
            public string MFSK8 { get; set; }
            public string MFSK16 { get; set; }
            public string PAC2 { get; set; }
            public string PAC3 { get; set; }
            public string PAX2 { get; set; }
            public string PCW { get; set; }
            public string PSK10 { get; set; }
            public string PSK31 { get; set; }
            public string PSK63 { get; set; }
            public string PSK63F { get; set; }
            public string PSK125 { get; set; }
            public string PSKAM10 { get; set; }
            public string PSKAM31 { get; set; }
            public string PSKAM50 { get; set; }
            public string PSKFEC31 { get; set; }
            public string PSKHELL { get; set; }
            public string QPSK31 { get; set; }
            public string QPSK63 { get; set; }
            public string QPSK125 { get; set; }
            public string THRBX { get; set; }
        }
    }
}
