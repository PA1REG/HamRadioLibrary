using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using HrdLibrary;


namespace CountryListLibrary
{
    public class CountryList
    {

    //    //        public static List<HrdFieldsObjects> HrdFieldsList = new List<HrdFieldsObjects>();

    //    public static List<HrdLibrary.HrdUtils.DxccObjects> DxccList = new List<HrdLibrary.HrdUtils.DxccObjects>();

    //    public static Boolean ReadCountryList()
    //    {
    //        string filename = @"C:\Users\pa1re\AppData\Roaming\HRDLLC\HRD Logbook\LogbookCountryDataEx.xml";
    //        XmlTextReader reader = new XmlTextReader(filename);
    //        DxccList.Clear();

    //        while (reader.Read())
    //        {
    //            Console.WriteLine("---> {0}", reader.NodeType);
    //            switch (reader.NodeType)
    //            {
    //                case XmlNodeType.Element: // The node is an element.
    //                    Console.Write("<" + reader.Name);
    //                    Console.WriteLine(">");
    //                    Console.WriteLine("no el   : {0}", reader.AttributeCount);
    //                    Console.WriteLine("Country : {0}", reader.GetAttribute("Country"));
    //                    Console.WriteLine("DXCC    : {0}", reader.GetAttribute("DXCC"));
    //                    break;
    //                case XmlNodeType.Text: //Display the text in each element.
    //                    Console.WriteLine(reader.Value);
    //                    break;
    //                case XmlNodeType.EndElement: //Display the end of the element.
    //                    Console.Write("</" + reader.Name);
    //                    Console.WriteLine(">");
    //                    break;
    //            }
    //        }
    //        Console.ReadLine();
    //        return true;
    //    }
    }
}
