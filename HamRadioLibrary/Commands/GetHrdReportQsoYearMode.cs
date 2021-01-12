using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.IO;
using System.Reflection;
using HamRadioDeluxeProperties;
using HamRadioDeluxeDatabaseLibrary;

namespace HrdReportQsoYearMode
{

    [Cmdlet(VerbsCommon.Get, "HrdReportQsoYearMode")]
    [OutputType(typeof(string))]
    public class GetHrdReportQsoYearMode : Cmdlet
    {

        protected override void ProcessRecord()
        {
            if (!HamRadioDeluxeDatabase.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }
            WriteVerbose("Fill the list.");
            HamRadioDeluxeDatabase.ReportQsoYearMode();

            WriteVerbose("Reading list.");
            foreach (HRDProperties.ReportYearModeProperty QsoYearModeList in HamRadioDeluxeDatabase.HrdQsoYearModeList)
            {
                WriteObject(new HRDProperties.ReportYearModeProperty
                {
                    YEAR = QsoYearModeList.YEAR,
                    AM = QsoYearModeList.AM,
                    ARDOP = QsoYearModeList.ARDOP,
                    ATV = QsoYearModeList.ATV,
                    C4FM = QsoYearModeList.C4FM,
                    CHIP = QsoYearModeList.CHIP,
                    CLO = QsoYearModeList.CLO,
                    CONTESTI = QsoYearModeList.CONTESTI,
                    CW = QsoYearModeList.CW,
                    DIGITALVOICE = QsoYearModeList.DIGITALVOICE,
                    DOMINO = QsoYearModeList.DOMINO,
                    DSTAR = QsoYearModeList.DSTAR,
                    FAX = QsoYearModeList.FAX,
                    FM = QsoYearModeList.FM,
                    FSK441 = QsoYearModeList.FSK441,
                    FT8 = QsoYearModeList.FT8,
                    HELL = QsoYearModeList.HELL,
                    ISCAT = QsoYearModeList.ISCAT,
                    JT4 = QsoYearModeList.JT4,
                    JT6M = QsoYearModeList.JT6M,
                    JT9 = QsoYearModeList.JT9,
                    JT44 = QsoYearModeList.JT44,
                    JT65 = QsoYearModeList.JT65,
                    MFSK = QsoYearModeList.MFSK,
                    MSK144 = QsoYearModeList.MSK144,
                    MT63 = QsoYearModeList.MT63,
                    OLIVIA = QsoYearModeList.OLIVIA,
                    OPERA = QsoYearModeList.OPERA,
                    PAC = QsoYearModeList.PAC,
                    PAX = QsoYearModeList.PAX,
                    PKT = QsoYearModeList.PKT,
                    PSK = QsoYearModeList.PSK,
                    PSK2K = QsoYearModeList.PSK2K,
                    Q15 = QsoYearModeList.Q15,
                    QRA64 = QsoYearModeList.QRA64,
                    ROS = QsoYearModeList.ROS,
                    RTTY = QsoYearModeList.RTTY,
                    RTTYM = QsoYearModeList.RTTYM,
                    SSB = QsoYearModeList.SSB,
                    SSTV = QsoYearModeList.SSTV,
                    T10 = QsoYearModeList.T10,
                    THOR = QsoYearModeList.THOR,
                    THRB = QsoYearModeList.THRB,
                    TOR = QsoYearModeList.TOR,
                    V4 = QsoYearModeList.V4,
                    VOI = QsoYearModeList.VOI,
                    WINMOR = QsoYearModeList.WINMOR,
                    WSPR = QsoYearModeList.WSPR,
                    AMTORFEC = QsoYearModeList.AMTORFEC,
                    ASCI = QsoYearModeList.ASCI,
                    CHIP64 = QsoYearModeList.CHIP64,
                    CHIP128 = QsoYearModeList.CHIP128,
                    DOMINOF = QsoYearModeList.DOMINOF,
                    FMHELL = QsoYearModeList.FMHELL,
                    FSK31 = QsoYearModeList.FSK31,
                    GTOR = QsoYearModeList.GTOR,
                    HELL80 = QsoYearModeList.HELL80,
                    HFSK = QsoYearModeList.HFSK,
                    JT4A = QsoYearModeList.JT4A,
                    JT4B = QsoYearModeList.JT4B,
                    JT4C = QsoYearModeList.JT4C,
                    JT4D = QsoYearModeList.JT4D,
                    JT4E = QsoYearModeList.JT4E,
                    JT4F = QsoYearModeList.JT4F,
                    JT4G = QsoYearModeList.JT4G,
                    JT65A = QsoYearModeList.JT65A,
                    JT65B = QsoYearModeList.JT65B,
                    JT65C = QsoYearModeList.JT65C,
                    MFSK8 = QsoYearModeList.MFSK8,
                    MFSK16 = QsoYearModeList.MFSK16,
                    PAC2 = QsoYearModeList.PAC2,
                    PAC3 = QsoYearModeList.PAC3,
                    PAX2 = QsoYearModeList.PAX2,
                    PCW = QsoYearModeList.PCW,
                    PSK10 = QsoYearModeList.PSK10,
                    PSK31 = QsoYearModeList.PSK31,
                    PSK63 = QsoYearModeList.PSK63,
                    PSK63F = QsoYearModeList.PSK63F,
                    PSK125 = QsoYearModeList.PSK125,
                    PSKAM10 = QsoYearModeList.PSKAM10,
                    PSKAM31 = QsoYearModeList.PSKAM31,
                    PSKAM50 = QsoYearModeList.PSKAM50,
                    PSKFEC31 = QsoYearModeList.PSKFEC31,
                    PSKHELL = QsoYearModeList.PSKHELL,
                    QPSK31 = QsoYearModeList.QPSK31,
                    QPSK63 = QsoYearModeList.QPSK63,
                    QPSK125 = QsoYearModeList.QPSK125,
                    THRBX = QsoYearModeList.THRBX
                });
            }
        }
    }



}
