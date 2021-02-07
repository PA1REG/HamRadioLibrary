using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Management.Automation;
using AdifLibrary;
using CsvLibrary;
using HamRadioDeluxeDatabaseLibrary;

namespace Adif_Test_App
{
    class Program
    {

        static void Main(string[] args)
        {


            //HamRadioDeluxeDatabase.ConnectToDatabase("localhost", 3307, "PA1REG", "MyPassword", "PA1REG");
            //HamRadioDeluxeDatabase.DatabaseInfo();
            //Console.ReadLine();
            //// CsvUtils.ReadBandList();

          // CsvUtils.ReadBandList();


            //CsvUtils.CsvReaderQso(@"Z:\Repos\HamRadioLibrary\src\eQsl.ccWithComment.csv",true,true,false);

            //CsvUtils.AdiWriteQso(@"Z:\Repos\HamRadioLibrary\src\Test.Adi");


            //string StartDate = "2011-05-02";
            //DateTime postingDate = Convert.ToDateTime(StartDate);

            //object StDate = CheckDate("20031007");
            //object EnDate = CheckDate("20070624");
            //object LowestDate = HrdUtils.ConvertToDate("20110623");
            //object HighestDate = HrdUtils.ConvertToDate("20151030");

            //HrdUtils.ReportYearBandMode(LowestDate,HighestDate  );

            ////string FileName = @"Z:\Repos\HamRadioLibrary\src\Eqsl.ccInbox_25122020.adi";
            //AdifUtils.AdifReadFile(FileName);

            //foreach (string AdifLine in AdifUtils.AdifArray)
            //{
            //    // Access your properties using item.NazivProizvoda, etc. here

            //    var AdifElementsObj = AdifUtils.GetAllAdifElements(AdifLine);
            //    if (AdifElementsObj.QSLMSG_INTL != null || AdifElementsObj.QSLMSG != null)
            //    {
            //        Console.Write(AdifElementsObj.CALL);
            //        Console.Write(" : ");
            //        //QSO_DATE = AdifElementsObj.QSO_DATE,
            //        //TIME_ON = AdifElementsObj.TIME_ON,
            //        //BAND = AdifElementsObj.BAND,
            //        //MODE = AdifElementsObj.MODE,
            //        //RST_SENT = AdifElementsObj.RST_SENT,
            //        //RST_RCVD = AdifElementsObj.RST_RCVD,
            //        //QSL_SENT = AdifElementsObj.QSL_SENT,
            //        //QSL_SENT_VIA = AdifElementsObj.QSL_SENT_VIA,
            //        //APP_EQSL_AG = AdifElementsObj.APP_EQSL_AG,
            //        //GRIDSQUARE = AdifElementsObj.GRIDSQUARE,
            //        //TIME_OFF = AdifElementsObj.TIME_OFF,
            //        //STATION_CALLSIGN = AdifElementsObj.STATION_CALLSIGN,
            //        //FREQ = AdifElementsObj.FREQ,
            //        //CONTEST_ID = AdifElementsObj.CONTEST_ID,
            //        //FREQ_RX = AdifElementsObj.FREQ_RX,
            //        //OPERATOR = AdifElementsObj.OPERATOR,
            //        //CQZ = AdifElementsObj.CQZ,
            //        //STX = AdifElementsObj.STX,
            //        //APP_N1MM_POINTS = AdifElementsObj.APP_N1MM_POINTS,
            //        //APP_N1MM_RADIO_NR = AdifElementsObj.APP_N1MM_RADIO_NR,
            //        //APP_N1MM_CONTINENT = AdifElementsObj.APP_N1MM_CONTINENT,
            //        //APP_N1MM_RUN1RUN2 = AdifElementsObj.APP_N1MM_RUN1RUN2,
            //        //APP_N1MM_RADIOINTERFACED = AdifElementsObj.APP_N1MM_RADIOINTERFACED,
            //        //APP_N1MM_ISORIGINAL = AdifElementsObj.APP_N1MM_ISORIGINAL,
            //        //APP_N1MM_NETBIOSNAME = AdifElementsObj.APP_N1MM_NETBIOSNAME,
            //        //APP_N1MM_ISRUNQSO = AdifElementsObj.APP_N1MM_ISRUNQSO,
            //        //APP_N1MM_ID = AdifElementsObj.APP_N1MM_ID,
            //        //APP_N1MM_CLAIMEDQSO = AdifElementsObj.APP_N1MM_CLAIMEDQSO,
            //        //SWL = AdifElementsObj.SWL,

            //        //Console.Write(AdifElementsObj.QSLMSG_INTL);
            //        if (AdifElementsObj.QSLMSG_INTL != null) { Console.Write(AdifElementsObj.QSLMSG_INTL); };
            //        Console.Write(" : ");
            //        if (AdifElementsObj.QSLMSG != null) { Console.Write(AdifElementsObj.QSLMSG); };

            //        //QSLMSG = AdifElementsObj.QSLMSG,
            //        //TX_PWR = AdifElementsObj.TX_PWR
            //        Console.WriteLine();
            //    }
            //}
            //Console.ReadLine();
            // Boolean a = CountryList.ReadCountryList();


            //AdifUtils.AdifReadFile(".\test.adi");




            ////The first step is to create a new instance of the PowerShell class
            //using (PowerShell powerShellInstance = PowerShell.Create()) //PowerShell.Create() creates an empty PowerShell pipeline for us to use for execution.
            //{
            //    // use "AddScript" to add the contents of a script file to the end of the execution pipeline.
            //    // use "AddCommand" to add individual commands/cmdlets to the end of the execution pipeline.

            //    powerShellInstance.AddScript("param($param1) $d = get-date; $s = 'test string value'; $d; $s; $param1; get-service");

            //    // use "AddParameter" to add a single parameter to the last command/script on the pipeline.
            //    powerShellInstance.AddParameter("param1", "parameter 1 value!");

            //    //Result of the script with Invoke()
            //    Collection<PSObject> result = powerShellInstance.Invoke();

            //    //output example : @{yourProperty=value; yourProperty1=value1; yourProperty2=StoppedDeallocated; PowerState=Stopped; OperationStatus=OK}}

            //    foreach (PSObject r in result)
            //    {
            //        //access to values
            //        //string r1 = r.Properties["yourProperty"].Value.ToString();
            //        Console.WriteLine(r.BaseObject.GetType().FullName);
            //        Console.WriteLine(r.BaseObject.ToString() + "\n");
            //    }
            //}




            ////AdifUtils AdifElement = new AdifUtils();

            ////string test = "<CALL:6>SV1PMR <QSO_DATE:8>20201024 <TIME_ON:6>062928<APP_N1MM_ID:32>82239d1b45a44d9eb5cc71118cdf7e7d";
            ////string returntest = AdifUtils.GetAdifElement(test, "CALL");

            ////string AdifLine = "<CALL:5>DK6JM<QSO_DATE:8:D>19980411<TIME_ON:4>1711<BAND:3>40M<MODE:3>SSB<RST_SENT:2>59<RST_RCVD:0><QSL_SENT:1>Y<QSL_SENT_VIA:1>E<APP_EQSL_AG:1>Y<GRIDSQUARE:6>jo31jg<EOR>";

            ////var AdifElementsObj = AdifUtils.GetAllAdifElements(AdifLine);


            ////Console.WriteLine("CALL : {0} QSO_DATE : {1} TIME_ON : {2} BAND : {3} MODE : {4} RST_SENT : {5} RST_RCVD : {6} QSL__SENT : {7} QSL_SENT_VIA : {8} APP_EQSL_AG : {9} GRIDSQUARE : {10}",AdifElementsObj.CALL,          AdifElementsObj.QSO_DATE,    AdifElementsObj.TIME_ON,   AdifElementsObj.BAND,   AdifElementsObj.MODE,AdifElementsObj.RST_SENT, AdifElementsObj.RST_RCVD,AdifElementsObj.QSL_SENT,   AdifElementsObj.QSL_SENT_VIA,  AdifElementsObj.APP_EQSL_AG, AdifElementsObj.GRIDSQUARE     );
            //Console.WriteLine("end of exec");
            //Console.ReadLine();


        }

    }

}
