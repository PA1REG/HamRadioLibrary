using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.IO;
using System.Reflection;
using HamRadioDeluxeProperties;
using HamRadioDeluxeDatabaseLibrary;

namespace HrdSearch
{

    [Cmdlet(VerbsCommon.Get, "HrdSearch")]
    [OutputType(typeof(string))]
    public class GetHrdSearch : Cmdlet
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
            if (!HamRadioDeluxeDatabase.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }

            WriteVerbose("Date : '" + Date + "'");
            object SearchDate = null;
            if (Date != "" && Date != null)
            {
                SearchDate = HamRadioDeluxeDatabase.ConvertToDate(Date);
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
            if (HamRadioDeluxeDatabase.SearchDatabase(Call, Band, Mode, SearchDate, SwlInclude))
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
                foreach (HRDProperties.HrdFieldsObjects FieldList in HamRadioDeluxeDatabase.HrdFieldsList)
                {
                    WriteObject(new HRDProperties.HrdFieldsObjects
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
                        SWL = FieldList.SWL,
                        KEY = FieldList.KEY
                    });
                }

            }
        }
    }



}
