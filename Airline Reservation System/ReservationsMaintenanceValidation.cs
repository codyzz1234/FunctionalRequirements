using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;


namespace Airline_Reservation_System
{
    
    internal class ReservationsMaintenanceValidation
    {
        public Boolean validateFlightDate(String userInput){
            Boolean validator = false;
            const string pattern = @"(0\d{1}|1[0-2])\/([0-2]\d{1}|3[0-1])\/(19|20)(\d{2})";
            var match = Regex.Match(userInput, pattern);
            if(!match.Success){
                Console.WriteLine("Invalid Date Format[mm/dd/yyyy] ");
                validator = false;
            }
            else{
                    DateTime dateToday = DateTime.Today; // As DateTime
                    var parameterDate = DateTime.ParseExact(userInput, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    if(parameterDate < dateToday){
                        validator = false;
                        Console.WriteLine("Invalid Flight Date. Please set a date today or in the future");
                    }
                    else{
                        validator = true;
                    }
            }
            return validator;
        }

        public Boolean validatePassengerNumber(String userInput){
            int passengerNumber;
            bool canConvert = Int32.TryParse(userInput, out passengerNumber);
            if (canConvert == true)
            {
                if (passengerNumber > 5 || passengerNumber < 1)
                {
                    Console.WriteLine("Number of Passengers must be between(inclusive) 1-5");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Must be numeric");
                return false;
            }

        }

        public Boolean validatePnrNo(String pnrNumber){
            Boolean validator = false;
            const string pattern = @"^([A-Z]{1}[A-Z0-9]{5})$";
            var match = Regex.Match(pnrNumber, pattern);
            if(!match.Success){
                validator = false;
            }
            else{
                validator = true;
            }
            return validator;
        }

        public Boolean checkIfNumExistsInCsv(String pnrNumber){
            Boolean doesExist = false;
            string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\", "Reservations.csv"); // Path For File Location
            path = path.Replace(@"\", @"\\");
            using (var streamReader = new StreamReader(path, Encoding.UTF8))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    var records = csvReader.GetRecords<ReservationCsvInfo>().ToList();
                    foreach (var record in records)
                    {
                        if(record.PNR_Number.Equals(pnrNumber)){
                            doesExist = true;
                            break;
                        }          
                    }
                }
            }
            return doesExist;
        }
    }
}
