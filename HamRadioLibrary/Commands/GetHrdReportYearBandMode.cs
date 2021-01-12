using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.IO;
using System.Reflection;
using HamRadioDeluxeProperties;
using HamRadioDeluxeDatabaseLibrary;

namespace HrdReportYearBandMode
{

    [Cmdlet(VerbsCommon.Get, "HrdReportYearBandMode")]
    [OutputType(typeof(string))]
    public class GetHrdReportYearBandMode : Cmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string StartDate { get; set; }
        [Parameter(Position = 0, Mandatory = false)]
        public string EndDate { get; set; }

        protected override void ProcessRecord()
        {
            if (!HamRadioDeluxeDatabase.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }

            WriteVerbose("StartDate : '" + StartDate + "'");
            object LowestDate = null;
            if (StartDate != "" && StartDate != null)
            {
                LowestDate = HamRadioDeluxeDatabase.ConvertToDate(StartDate);
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
                HighestDate = HamRadioDeluxeDatabase.ConvertToDate(EndDate);
                if (LowestDate == null)
                {
                    WriteWarning("The entered EndDate (" + EndDate + ") is not the correct format (yyyyMMdd).");
                    return;
                }
            }

            WriteVerbose("Fill the list.");
            HamRadioDeluxeDatabase.ReportYearBandMode(LowestDate, HighestDate);

            // todo https://stackoverflow.com/questions/29224905/powershell-output-formatting-from-c-sharp-using-defaultdisplaypropertyset

            //string[] DefaultProperties = { "Name", "Property2", "Property4" };
            //base.WriteObject(SetDefaultProperties(myObject, DefaultProperties));

            string[] DefaultProperties = { "YEAR", "BAND", "Property4" };

            WriteVerbose("Reading list.");
            foreach (HRDProperties.ReportYearBandModeObjects YearBandModeList in HamRadioDeluxeDatabase.HrdYearBandModeList)
            {
                WriteObject(new HRDProperties.ReportYearBandModeObjects
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



}
