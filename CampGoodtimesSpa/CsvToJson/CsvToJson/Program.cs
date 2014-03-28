using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;

// C:\Users\mikedice\Documents\Repos\CampGoodtimesWeb\GoodtimesWeb\GoodtimesWeb\Content\DonationsData\Donations.csv
namespace CsvToJson
{
    public class Donor
    {
        public DateTime DateTime { get; set; }
        public string Giver { get; set; }
        public string InHonorOf { get; set; }
    }

    class Program
    {
        
        static void Main(string[] args)
        {
            var donorList = new List<Donor>();
            string inputFile = args[0];
            string outputFile = args[1];
            var fieldParser = new TextFieldParser(inputFile);
            fieldParser.SetDelimiters(new []{","});
            var headers = fieldParser.ReadFields();
            string[] fields;
            do
            {
                fields = fieldParser.ReadFields();
                if (fields != null)
                {
                    var donor = new Donor();
                    donor.DateTime = DateTime.Parse(fields[0]);
                    donor.Giver = fields[1];
                    if (fields.Length > 2)
                    {
                        string[] dest = new string[fields.Length-2];
                        Array.Copy(fields, 2, dest, 0, fields.Length - 2);
                        donor.InHonorOf = string.Join(", ", dest);
                    }
                    donorList.Add(donor);
                }

            } while (fields != null);

            string result = JsonConvert.SerializeObject(donorList);
            using (var outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var bytes = Encoding.UTF8.GetBytes(result);
                outputStream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
