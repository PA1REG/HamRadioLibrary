using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.IO;
using System.Reflection;
using HamRadioDeluxeProperties;
using HamRadioDeluxeDatabaseLibrary;

namespace HrdReportBand
{

    [Cmdlet(VerbsCommon.Get, "HrdReportBand")]
    [OutputType(typeof(string))]
    public class GetHrdReportBand : Cmdlet
    {

        protected override void ProcessRecord()
        {
            if (!HamRadioDeluxeDatabase.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }
            WriteVerbose("Fill the list.");
            HamRadioDeluxeDatabase.ReportBand();

            WriteVerbose("Reading list.");
            foreach (HRDProperties.ReportBandObjects BandList in HamRadioDeluxeDatabase.HrdBandList)
            {
                WriteObject(new HRDProperties.ReportBandObjects
                {
                    BAND = BandList.BAND,
                    WORKED = BandList.WORKED
                });
            }
        }
    }


}
