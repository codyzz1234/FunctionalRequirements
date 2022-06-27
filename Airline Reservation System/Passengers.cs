using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;
using CsvHelper;
using System.IO;
using System.Linq;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;



namespace Airline_Reservation_System
{
  
    internal class Passengers
    {
        public Boolean addNewPassenger(Boolean validator, String numberOfPassengers)
        {
            Console.WriteLine();
            Boolean isValid = false;
            int passengerNumber;
            String firstName;
            String lastName;
            String birthDate;
            String age;
            int pass = 1;
            int counter = 0;
            bool canConvert = Int32.TryParse(numberOfPassengers, out passengerNumber);
            List<PassengerInfo> passengers = new List<PassengerInfo>();

            while (counter < passengerNumber) {
                Console.WriteLine("Input Passenger " + pass);
                Console.Write("First Name: ");
                firstName = Console.ReadLine();

                Console.Write("Last Name: ");
                lastName = Console.ReadLine();

                Console.Write("BirthDate: ");
                birthDate = Console.ReadLine();


                isValid = checkPassengerInfo(firstName,lastName,birthDate);
                if(isValid == false)
                {
                    continue;
                }
                else
                {
                    age = calculateAge(birthDate);
                    passengers.Add(new PassengerInfo
                    {
                       First_Name = firstName,
                       Last_Name = lastName,
                       Birth_Date = birthDate,
                       Age = age,
                    });
                    Console.WriteLine("\n");
                    counter++;
                    pass++;
                }
               
            }
            if(isValid == true)
            {
                string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\", "Passengers.csv");
                path = path.Replace(@"\", @"\\");

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Don't write the header again.
                    HasHeaderRecord = false,
                };

                using (var stream = File.Open(path, FileMode.Append))
                using (var writer = new StreamWriter(stream, Encoding.UTF8))
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.WriteRecords(passengers);
                }

                Console.WriteLine("Passengers Saved");
                return true;
            }
            else
            {
                return false;
            }
        }

    
        private Boolean checkPassengerInfo(String firstName, String lastName, String birthDate)
        {
            Boolean isValid = false;
            const string pattern = @"^(?=.{1,20}$)[a-zA-Z]+(?:[-'\s][a-zA-Z]+)*$$";
            var match = Regex.Match(firstName,pattern);
            if (!match.Success)
            {
                isValid = false;
                Console.WriteLine("Invalid First Name. Must be 20 characters at most and not blank");

            }
            else
            {
                match = Regex.Match(lastName, pattern);
                if (!match.Success)
                {
                    Console.WriteLine("Invalid Last Name. Must be 20 characters at most and not blank");
                }
                else
                {
                    isValid = true;
                }
            }

            if(isValid == false)
            {
                return isValid;
            }
            else
            {
                const String pattern2 = @"(0\d{1}|1[0-2])\/([0-2]\d{1}|3[0-1])\/(19|20)(\d{2})";
                match = Regex.Match(birthDate, pattern2);
                if (!match.Success)
                {
                    isValid = false;
                
                }
                else
                {
                    DateTime dateToday = DateTime.Today; // As DateTime
                    var parameterDate = DateTime.ParseExact(birthDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    if (parameterDate > dateToday)
                    {
                        isValid = false;
                        Console.WriteLine("Invalid BirthDate. Please set a date in the Past");

                    }
                    else
                    {
                        isValid = true;
                    }
                }
            }
            return isValid;
       
        }
  
   
        private String calculateAge(String birthDate){
            DateTime zeroTime = new DateTime(1, 1, 1);
            DateTime dateToday = DateTime.Today; // As DateTime
            var parameterDate = DateTime.ParseExact(birthDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            TimeSpan span = dateToday - parameterDate;
            // var diffOfDates = dateToday - parameterDate;
            int years = (zeroTime + span).Year - 1;
            return years.ToString();
        }
    }

    class PassengerInfo
    {
        public String First_Name { get;set;}
        public String Last_Name { get;set;}
        public String Birth_Date { get;set;}
        public String Age { get;set;}
    }

}
