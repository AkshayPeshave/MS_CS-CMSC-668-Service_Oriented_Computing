using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.Common;
using System.Data.Sql;
using System.Data.ProviderBase;
using System.Data.SqlClient;


namespace GSC_Catalog_Decoder
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //FileStream catalogFile = File.Open("D:\\Workspace\\CMSC 668\\Catalogs\\bincats_GSC_1.2\\N0000\\0001.gsc",FileMode.Open);

            ////string gscString = Encoding.ASCII.GetString(bytesInCatalog);
            ////System.Console.WriteLine(BitConverter.ToInt64(bytesInCatalog,0));

            ////GSC Designation extraction Bytes 1-10 (10 digit integer)
            //byte[] bytesInCatalog=new byte[10];
            //catalogFile.Read(bytesInCatalog,0,10);
            //System.Console.WriteLine(BitConverter.ToString(bytesInCatalog));

            ////Right Ascension (in degrees) extraction Bytes 12-20 (9 digit-5 precision digits float)
            //catalogFile.Seek(1, SeekOrigin.Current);
            //bytesInCatalog = new byte[9];
            //catalogFile.Read(bytesInCatalog, 0, 9);
            //System.Console.WriteLine(BitConverter.ToDouble(bytesInCatalog,0));

            ////Declination (in degrees) extraction Bytes 22-30 (9 digit-5 precision digits float)
            //catalogFile.Seek(1, SeekOrigin.Current);
            //bytesInCatalog = new byte[9];
            //catalogFile.Read(bytesInCatalog,0,9);
            //System.Console.WriteLine(BitConverter.ToDouble(bytesInCatalog, 0));

            ////Mean position error (in arc second) extraction Bytes 33-36 (4 digit-1 precision digits float)
            //catalogFile.Seek(2, SeekOrigin.Current);
            //bytesInCatalog = new byte[8];
            //bytesInCatalog=BitConverter.GetBytes(0);
            //catalogFile.Read(bytesInCatalog, 4, 4);
            //System.Console.WriteLine(BitConverter.ToDouble(bytesInCatalog, 0));

            //System.Console.ReadKey();
            //catalogFile.Close();
        }
    }
}
