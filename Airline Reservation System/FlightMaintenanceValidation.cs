using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Airline_Reservation_System
{
    internal class FlightMaintenanceValidation
    {
        public Boolean validateAirCode(String userInput)
        {
            const string pattern = @"^(([A-zA-Z]){2}|([0-9]){1}([A-zA-Z])|([A-zA-Z]){1}([0-9]))$";
            var match = Regex.Match(userInput, pattern);
            if (match.Success == false)
            {
                Console.WriteLine("Exactly Two Characters,Must either comprise of all letters. If the first character is a numeric digit, secondcharacter should be a letter");
                return false;
            }
            else
            {
                return true;
            }
        }
        public Boolean validateFlightNum(String userInput)
        {
            int flightNumber;
            bool canConvert = Int32.TryParse(userInput, out flightNumber);
            if (canConvert == true)
            {
                if (flightNumber > 9999 || flightNumber < 1)
                {
                    Console.WriteLine("Flight Number must be between(inclusive) 1-9999");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Flight number must be a number");
                return false;
            }
        }

        public Boolean validateArrivalStat(String userInput)
        {
            Boolean validator = false;
            const string pattern = @"^([A-Z]{1})([A-Z0-9]{2})$";
            var match = Regex.Match(userInput, pattern);
            if (match.Success == false)
            {
                validator = false;
                Console.WriteLine("Exactly 3 Characters Long. Numeric digit is optional. Must at least comprise of all letters.The first character must be a letter and all characters uppercased");
            }
            else
            {
                validator = true;
            }
            return validator;
        }
        public Boolean validateDepartureStat(String userInput)
        {
            Boolean validator = false;
            const string pattern = @"^([A-Z]{1})([A-Z0-9]{2})$";
            var match = Regex.Match(userInput, pattern);
            if (match.Success == false)
            {
                validator = false;
                Console.WriteLine("Exactly 3 Characters Long. Numeric digit is optional. Must at least comprise of all letters.The first character must be a letter and all characters uppercased");
            }
            else
            {
                validator = true;
            }
            return validator;
        }
        public Boolean validateSta(String userInput){
                Boolean validator = false;
                if(userInput.Length < 0){
                    Console.WriteLine("Invalid 24 hour time format ex:[HH:MM]");
                    validator = false;
                }
                else{
                     string pattern = @"^(?:[01][0-9]|2[0-3]):[0-5][0-9]$";
                     Regex r = new Regex(pattern,RegexOptions.IgnoreCase);
                     var match = Regex.Match(userInput,pattern);
                     if(!match.Success){
                          validator = false;
                          Console.WriteLine("Invalid 24 hour time format ex:[HH:MM]");
                     }
                     else{
                          validator = true;
                     }
                }
            return validator;
        }


        public Boolean validateStd(String userInput){
                Boolean validator = false;
                if(userInput.Length < 0){
                    Console.WriteLine("Invalid 24 hour time format ex:[HH:MM]");
                    validator = false;
                }
                else{
                     string pattern = @"^(?:[01][0-9]|2[0-3]):[0-5][0-9]$";
                     Regex r = new Regex(pattern,RegexOptions.IgnoreCase);
                     var match = Regex.Match(userInput,pattern);
                     if(!match.Success){
                          validator = false;
                          Console.WriteLine("Invalid 24 hour time format ex:[HH:MM]");
                     }
                     else{
                          validator = true;
                     }

                }
            return validator;
        }
    }

 }
