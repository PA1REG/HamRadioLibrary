using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.IO;
using System.Reflection;
using HamRadioDeluxeProperties;
using HamRadioDeluxeDatabaseLibrary;

namespace HrdIndexStatistics
{

    [Cmdlet(VerbsCommon.Get, "HrdIndexStatistics")]
    [OutputType(typeof(string))]
    public class GetHrdIndexStatistics : Cmdlet
    {

        protected override void ProcessRecord()
        {
            if (!HamRadioDeluxeDatabase.CheckDatabaseOpen())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("You must call the Connect-HrdDatabase cmdlet before calling any other cmdlets."), "NotConnectedToDatabase", ErrorCategory.ConnectionError, this));
                return;
            }
            WriteVerbose("Fill the list.");
            HamRadioDeluxeDatabase.IndexDatabaseStatistics();

            //WriteObject(new HrdUtils.HrdIndexObjectUtils
            //{
            //    INDEX_NAME = HrdUtils.HrdIndexen.INDEX_NAME,
            //    INDEX_COLUMNS = HrdUtils.HrdIndexen.INDEX_COLUMNS,
            //    CARDINALITY = HrdUtils.HrdIndexen.CARDINALITY
            //});

            WriteVerbose("Reading list.");
            foreach (HRDProperties.HrdIndexObjects IndexList in HamRadioDeluxeDatabase.HrdIndexList)
            {
                WriteObject(new HRDProperties.HrdIndexObjects
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

}
