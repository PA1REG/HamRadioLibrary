using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.IO;
using System.Reflection;
using HamRadioDeluxeProperties;
using HamRadioDeluxeDatabaseLibrary;

namespace HrdReportQsoYear
{


    [Cmdlet(VerbsCommon.Get, "HrdReportQsoYear")]
    [OutputType(typeof(string))]
    public class GetHrdReportQsoYear : Cmdlet
    {

        protected override void ProcessRecord()
        {
            if (!HamRadioDeluxeDatabase.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }
            WriteVerbose("Fill the list.");
            HamRadioDeluxeDatabase.ReportQsoPerYear();

            WriteVerbose("Reading list.");
            foreach (HRDProperties.ReportQsoByYearObjects QsoByYearList in HamRadioDeluxeDatabase.HrdQsoByYearList)
            {
                WriteObject(new HRDProperties.ReportQsoByYearObjects
                {
                    YEAR = QsoByYearList.YEAR,
                    WORKED = QsoByYearList.WORKED,
                    PERCENT = QsoByYearList.PERCENT
                });
            }
        }
    }


}
