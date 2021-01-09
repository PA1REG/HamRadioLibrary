using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using AdifLibrary;
using System.IO;
using HrdLibrary;
using System.Reflection;

namespace HrdCmdLets
{

    [Cmdlet(VerbsCommunications.Connect, "HrdDatabase")]
    [OutputType(typeof(string))]
    public class ConnectHrdDatabase : Cmdlet
    {

        [Parameter(Position = 0, Mandatory = true)]
        public string Server { get; set; }
        [Parameter(Position = 1, Mandatory = true)]
        public Int16 Port { get; set; }
        [Parameter(Position = 2, Mandatory = true)]
        public string User { get; set; }
        [Parameter(Position = 3, Mandatory = true)]
        public string Password { get; set; }
        [Parameter(Position = 4, Mandatory = true)]
        public string Database { get; set; }

        protected override void ProcessRecord()
        {
            WriteVerbose("Start Opening Database.");

            if (HamRadioDeluxe.CheckDatabaseOpen())
            {
                //    Console.WriteLine("Database still open.");
                //    return;
                WriteVerbose("Closing database, because it was open.");
                HamRadioDeluxe.DisconnectFromDatabase();
            }

            if (!HamRadioDeluxe.ConnectToDatabase(Server, Port, User, Password, Database))
            {
                WriteVerbose("Database open, checking for existing hrd table.");
                try
                {
                    if (!HamRadioDeluxe.ExistsTableHrdContacts())
                    {
                        WriteVerbose("Hrd table not found.");
                        throw new TABLEHRDCONTACTSException("Could not find table : TABLE_HRD_CONTACTS_V01");
                    }
                }
                catch (TABLEHRDCONTACTSException ex)
                {
                    WriteError(new ErrorRecord(ex, "Hrd Database Disconnected",
                                 ErrorCategory.ObjectNotFound, "TABLE_HRD_CONTACTS_V01"));
                    HamRadioDeluxe.DisconnectFromDatabase();
                }
            }
        }
    }

    //Creating Custome Exception - OutofStockException
    public class TABLEHRDCONTACTSException : Exception
    {
        public TABLEHRDCONTACTSException(string message)
            : base(message)
        {
        }
    }

    [Cmdlet(VerbsCommunications.Disconnect, "HrdDatabase")]
    [OutputType(typeof(string))]
    public class DisconnectHrdDisDatabaseCmdlets : Cmdlet
    {

        protected override void ProcessRecord()
        {
            WriteVerbose("Disconnecting Database.");
            HamRadioDeluxe.DisconnectFromDatabase();
            //WriteObject("jolo");
        }
    }

    [Cmdlet(VerbsCommon.Get, "HrdDatabaseInfo")]
    [OutputType(typeof(string))]
    public class GetHrdDatabaseInfoCmdlets : Cmdlet
    {

        protected override void ProcessRecord()
        {
            if (!HamRadioDeluxe.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }

            WriteVerbose("Show database info.");
            HamRadioDeluxe.DatabaseInfo();

            StringBuilder sb = new StringBuilder();

            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            var modulename = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            sb.AppendLine("Module name (dll) : " + modulename);
            sb.Append("Module version    : " + version.Major + "." + version.Minor + "." + version.Build + "." + version.Revision);

            //if (version.Build > 0)
            //{
            //    sb.Append(" [patch " + version.Build + "]");
            //    sb.Append("Revision " + version.Revision);
            //    sb.Append(version.Build + "." + version.Revision);
            //}
            sb.AppendLine();

            Console.WriteLine();
            Console.Write(sb.ToString());
            //WriteObject("jolo");
        }
    }

    [Cmdlet(VerbsLifecycle.Start, "HrdTableOptimize")]
    [OutputType(typeof(string))]
    public class StartHrdTableOptimizeCmdlets : Cmdlet
    {

        protected override void ProcessRecord()
        {
            if (!HamRadioDeluxe.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }
            WriteVerbose("Start Optimze Database.");
            HamRadioDeluxe.OptimizeHrdTable();
        }
    }

    [Cmdlet(VerbsLifecycle.Start, "HrdTableAnalyze")]
    [OutputType(typeof(string))]
    public class StartHrdTableAnalyzeCmdlets : Cmdlet
    {

        protected override void ProcessRecord()
        {
            if (!HamRadioDeluxe.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }
            WriteVerbose("Start Analyze Database.");
            HamRadioDeluxe.AnalyzeHrdTable();
        }
    }

    [Cmdlet(VerbsCommon.Get, "HrdSearch")]
    [OutputType(typeof(string))]
    public class GetHrdSearchCmdlets : Cmdlet
    {

        [Parameter(Position = 0, Mandatory = true)]
        public string Call { get; set; }
        [Parameter(Position = 1, Mandatory = false)]
        public string Band { get; set; }
        [Parameter(Position = 2, Mandatory = false)]
        public string Mode { get; set; }
        [Parameter(Position = 3, Mandatory = false)]
        public string Date { get; set; }
        [Parameter(Position = 4, Mandatory = false)]
        public SwitchParameter IncludeSwl { get; set; }

        protected override void ProcessRecord()
        {
            //if (!HrdUtils.CheckDatabaseOpen()) { WriteWarning(MethodBase.GetCurrentMethod().DeclaringType.Name + " : You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."); return; }
            if (!HamRadioDeluxe.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }

            WriteVerbose("Date : '" + Date + "'");
            object SearchDate = null;
            if (Date != "" && Date != null)
            {
                SearchDate = HamRadioDeluxe.ConvertToDate(Date);
                if (SearchDate == null)
                {
                    WriteWarning("The entered Date (" + Date + ") is not the correct format (yyyyMMdd).");
                    return;
                }
            }

            bool SwlInclude = false;
            if (IncludeSwl)
            {
                SwlInclude = true;
            }

            WriteVerbose("Search database.");
            if (HamRadioDeluxe.SearchDatabase(Call, Band, Mode, SearchDate, SwlInclude))
            {

                // T.O.D.O. qso band mode
                // todo bevestigd qso
                // todo https://stackoverflow.com/questions/22639083/scripted-cmdlets-and-compiled-cmdlets-in-a-single-module
                // todo https://www.reddit.com/r/PowerShell/comments/9tb4oi/mixing_scripts_and_cmdlets_in_the_same_module/

                //WriteObject("Call Found");
                //HrdUtils.HrdFieldsObject HrdFields = new HrdUtils.HrdFieldsObject();
                //HrdUtils.HrdFieldsObjectUtils test = new HrdUtils.HrdFieldsObjectUtils();
                //WriteVerbose("Filling record.");
                //WriteObject(new HrdUtils.HrdFieldsObjects
                //{
                //    CALL = HrdUtils.HrdFields.CALL,
                //    QSO_DATE = HrdUtils.HrdFields.QSO_DATE,
                //    TIME_ON = HrdUtils.HrdFields.TIME_ON,
                //    TIME_OFF = HrdUtils.HrdFields.TIME_OFF,
                //    BAND = HrdUtils.HrdFields.BAND,
                //    MODE = HrdUtils.HrdFields.MODE,
                //    RST_SENT = HrdUtils.HrdFields.RST_RCVD,
                //    RST_RCVD = HrdUtils.HrdFields.RST_SENT,
                //    QSL_SENT = HrdUtils.HrdFields.QSL_SENT,
                //    QSL_SENT_VIA = HrdUtils.HrdFields.QSL_SENT_VIA,
                //    GRIDSQUARE = HrdUtils.HrdFields.GRIDSQUARE,
                //    STATION_CALLSIGN = HrdUtils.HrdFields.STATION_CALLSIGN,
                //    FREQ = HrdUtils.HrdFields.FREQ,
                //    CONTEST_ID = HrdUtils.HrdFields.CONTEST_ID,
                //    OPERATOR = HrdUtils.HrdFields.OPERATOR,
                //    CQZ = HrdUtils.HrdFields.CQZ,
                //    STX = HrdUtils.HrdFields.STX,
                //    SWL = HrdUtils.HrdFields.SWL
                //});

                WriteVerbose("Reading record.");
                foreach (HamRadioDeluxe.HrdFieldsObjects FieldList in HamRadioDeluxe.HrdFieldsList)
                {
                    WriteObject(new HamRadioDeluxe.HrdFieldsObjects
                    {
                        CALL = FieldList.CALL,
                        QSO_DATE = FieldList.QSO_DATE,
                        TIME_ON = FieldList.TIME_ON,
                        TIME_OFF = FieldList.TIME_OFF,
                        BAND = FieldList.BAND,
                        FREQ = FieldList.FREQ,
                        MODE = FieldList.MODE,
                        RST_SENT = FieldList.RST_RCVD,
                        RST_RCVD = FieldList.RST_SENT,
                        QSL_SENT = FieldList.QSL_SENT,
                        QSL_SENT_VIA = FieldList.QSL_SENT_VIA,
                        GRIDSQUARE = FieldList.GRIDSQUARE,
                        STATION_CALLSIGN = FieldList.STATION_CALLSIGN,
                        CONTEST_ID = FieldList.CONTEST_ID,
                        OPERATOR = FieldList.OPERATOR,
                        CQZ = FieldList.CQZ,
                        STX = FieldList.STX,
                        SWL = FieldList.SWL
                    });
                }

            }
        }
    }



    [Cmdlet(VerbsCommon.Get, "HrdIndexStatistics")]
    [OutputType(typeof(string))]
    public class GetHrdIndexStatisticsCmdlets : Cmdlet
    {

        protected override void ProcessRecord()
        {
            if (!HamRadioDeluxe.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }
            WriteVerbose("Fill the list.");
            HamRadioDeluxe.IndexDatabaseStatistics();

            //WriteObject(new HrdUtils.HrdIndexObjectUtils
            //{
            //    INDEX_NAME = HrdUtils.HrdIndexen.INDEX_NAME,
            //    INDEX_COLUMNS = HrdUtils.HrdIndexen.INDEX_COLUMNS,
            //    CARDINALITY = HrdUtils.HrdIndexen.CARDINALITY
            //});

            WriteVerbose("Reading list.");
            foreach (HamRadioDeluxe.HrdIndexObjects IndexList in HamRadioDeluxe.HrdIndexList)
            {
                WriteObject(new HamRadioDeluxe.HrdIndexObjects
                {
                    INDEX_NAME = IndexList.INDEX_NAME,
                    INDEX_COLUMNS = IndexList.INDEX_COLUMNS,
                    CARDINALITY = IndexList.CARDINALITY
                });
                //    Console.WriteLine(IndexList.INDEX_NAME);
                //    Console.WriteLine(IndexList.INDEX_COLUMNS);
                //    Console.WriteLine(IndexList.CARDINALITY);
            }
        }
    }



    [Cmdlet(VerbsCommon.Get, "HrdReportMode")]
    [OutputType(typeof(string))]
    public class GetHrdReportModeCmdlets : Cmdlet
    {

        protected override void ProcessRecord()
        {
            if (!HamRadioDeluxe.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }
            WriteVerbose("Fill the list.");
            HamRadioDeluxe.ReportMode();

            WriteVerbose("Reading list.");
            foreach (HamRadioDeluxe.ReportModeObjects ModeList in HamRadioDeluxe.HrdModeList)
            {
                WriteObject(new HamRadioDeluxe.ReportModeObjects
                {
                    MODE = ModeList.MODE,
                    SUBMODE = ModeList.SUBMODE,
                    WORKED = ModeList.WORKED
                });
            }
        }
    }


    [Cmdlet(VerbsCommon.Get, "HrdReportBand")]
    [OutputType(typeof(string))]
    public class GetHrdReportBandCmdlets : Cmdlet
    {

        protected override void ProcessRecord()
        {
            if (!HamRadioDeluxe.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }
            WriteVerbose("Fill the list.");
            HamRadioDeluxe.ReportBand();

            WriteVerbose("Reading list.");
            foreach (HamRadioDeluxe.ReportBandObjects BandList in HamRadioDeluxe.HrdBandList)
            {
                WriteObject(new HamRadioDeluxe.ReportBandObjects
                {
                    BAND = BandList.BAND,
                    WORKED = BandList.WORKED
                });
            }
        }
    }

    [Cmdlet(VerbsCommon.Get, "HrdReportBandMode")]
    [OutputType(typeof(string))]
    public class GetHrdReportBandModeCmdlets : Cmdlet
    {

        protected override void ProcessRecord()
        {
            if (!HamRadioDeluxe.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }
            WriteVerbose("Fill the list.");
            HamRadioDeluxe.ReportModeBand();

            WriteVerbose("Reading list.");
            foreach (HamRadioDeluxe.ReportBandModeObjects BandModeList in HamRadioDeluxe.HrdBandModeList)
            {
                WriteObject(new HamRadioDeluxe.ReportBandModeObjects
                {
                    BAND = BandModeList.BAND,
                    MODE = BandModeList.MODE,
                    SUBMODE = BandModeList.SUBMODE,
                    WORKED = BandModeList.WORKED
                });
            }
        }
    }


    [Cmdlet(VerbsCommon.Get, "HrdReportYearBandMode")]
    [OutputType(typeof(string))]
    public class GetHrdReportYearBandModeCmdlets : Cmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string StartDate { get; set; }
        [Parameter(Position = 0, Mandatory = false)]
        public string EndDate { get; set; }

        protected override void ProcessRecord()
        {
            if (!HamRadioDeluxe.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }

            WriteVerbose("StartDate : '" + StartDate + "'");
            object LowestDate = null;
            if (StartDate != "" && StartDate != null)
            {
                LowestDate = HamRadioDeluxe.ConvertToDate(StartDate);
                if (LowestDate == null)
                {
                    WriteWarning("The entered StartDate (" + StartDate + ") is not the correct format (yyyyMMdd).");
                    return;
                }
            }

            WriteVerbose("EndDate : '" + EndDate + "'");
            object HighestDate = null;
            if (EndDate != "" && EndDate != null)
            {
                HighestDate = HamRadioDeluxe.ConvertToDate(EndDate);
                if (LowestDate == null)
                {
                    WriteWarning("The entered EndDate (" + EndDate + ") is not the correct format (yyyyMMdd).");
                    return;
                }
            }

            WriteVerbose("Fill the list.");
            HamRadioDeluxe.ReportYearBandMode(LowestDate, HighestDate);

            // todo https://stackoverflow.com/questions/29224905/powershell-output-formatting-from-c-sharp-using-defaultdisplaypropertyset

            //string[] DefaultProperties = { "Name", "Property2", "Property4" };
            //base.WriteObject(SetDefaultProperties(myObject, DefaultProperties));

            string[] DefaultProperties = { "YEAR", "BAND", "Property4" };

            WriteVerbose("Reading list.");
            foreach (HamRadioDeluxe.ReportYearBandModeObjects YearBandModeList in HamRadioDeluxe.HrdYearBandModeList)
            {
                WriteObject(new HamRadioDeluxe.ReportYearBandModeObjects
                {
                    YEAR = YearBandModeList.YEAR,
                    BAND = YearBandModeList.BAND,
                    MODE = YearBandModeList.MODE,
                    SUBMODE = YearBandModeList.SUBMODE,
                    WORKED = YearBandModeList.WORKED
                }, true);
            }
        }
    }

    [Cmdlet(VerbsCommon.Get, "HrdReportQso")]
    [OutputType(typeof(string))]
    public class GetHrdReportQsoCmdlets : Cmdlet
    {

        protected override void ProcessRecord()
        {
            if (!HamRadioDeluxe.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }
            WriteVerbose("Fill the list.");
            HamRadioDeluxe.ReportQso();

            WriteVerbose("Reading list.");
            foreach (HamRadioDeluxe.ReportQsoObjects QsoList in HamRadioDeluxe.HrdQsoList)
            {
                WriteObject(new HamRadioDeluxe.ReportQsoObjects
                {
                    WORKED = QsoList.WORKED,
                    OM = QsoList.OM,
                    SWL = QsoList.SWL
                });
            }
        }
    }


    [Cmdlet(VerbsCommon.Get, "HrdReportQsoPerYear")]
    [OutputType(typeof(string))]
    public class GetHrdReportQsoPerYearCmdlets : Cmdlet
    {

        protected override void ProcessRecord()
        {
            if (!HamRadioDeluxe.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }
            WriteVerbose("Fill the list.");
            HamRadioDeluxe.ReportQsoPerYear();

            WriteVerbose("Reading list.");
            foreach (HamRadioDeluxe.ReportQsoByYearObjects QsoByYearList in HamRadioDeluxe.HrdQsoByYearList)
            {
                WriteObject(new HamRadioDeluxe.ReportQsoByYearObjects
                {
                    YEAR = QsoByYearList.YEAR,
                    WORKED = QsoByYearList.WORKED,
                    PERCENT = QsoByYearList.PERCENT
                });
            }
        }
    }

}
