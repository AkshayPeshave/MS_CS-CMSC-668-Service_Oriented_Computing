using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.IO;

namespace GSC_Assimilator
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream("C:\\GSC_Decoded\\value_listing.txt", FileMode.Create);
            int gscIndex = 0;
            while (gscIndex <= 9537)
            {
                gscIndex++;
                string gscIndexString="";
                for (int gscIndexDigits = gscIndex.ToString().Length; gscIndexDigits < 5; gscIndexDigits++)
                    gscIndexString += "0";
                gscIndexString += gscIndex.ToString();
                string command = @"C:\Catalogs\gsc.exe -g " + gscIndexString + " -n 2000000";
                string output = "";

                ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process proc = new Process())
                {
                    proc.StartInfo = procStartInfo;
                    proc.Start();

                    output = proc.StandardOutput.ReadToEnd();
                }
                string[] valuesList = output.Split(' ');
                
                //byte[] valuesInByteArray = null;
                int index = 0;
                foreach (string value in valuesList)
                {
                    //valuesInByteArray[index] = Byte.Parse(value + "\n");
                    //fs.WriteByte(Byte.Parse(value.to + "\n"));
                    if (value == "")
                        continue;


                    string temp = value + " ";


                    byte[] byteArray = System.Text.Encoding.Unicode.GetBytes(temp.ToCharArray());
                    fs.Write(byteArray, 0, byteArray.Length);
                }

                //fs.Write(valuesInByteArray, 0, valuesInByteArray.Length);
                System.Console.WriteLine(output);
                
            }
            fs.Close();
        }
    }
}
