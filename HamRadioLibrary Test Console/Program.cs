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

namespace HamRadioLibrary_Test_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            HamRadioDeluxeDatabase.ConnectToDatabase("192.168.64.200", 3307, "PA1REG", "dC7K68zUdGx6FKEu", "PA1REG");
            HamRadioDeluxeDatabase.SearchDatabase("op4k","","","",true );
        }
    }
}
