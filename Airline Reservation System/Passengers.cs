using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;



namespace Airline_Reservation_System
{
  
    internal class Passengers
    {
        public Boolean addNewPassenger(Boolean validator, String numberOfPassengers)
        {
            Boolean isValid = true;
            int passengerNumber;
            String firstName;
            String lastName;
            String birthDate;
            String age;
            int counter = 0;
            bool canConvert = Int32.TryParse(numberOfPassengers, out passengerNumber);
            List<Passengers> passengers = new List<Passengers>();
            while(counter < passengerNumber){
                Console.WriteLine("Passenger: "+counter+1);
                Console.Write("Input First Name: ");
                firstName = Console.ReadLine();
                Console.Write("Input Last Name: ");
                lastName = Console.ReadLine();
                isValid = checkFirstNameLastName(firstName,lastName);
                if(!isValid){
                    Console.WriteLine();
                    continue;
                }
                else{
                    birthDate:
                    Console.Write("Input BirthDate: ");
                    birthDate = Console.ReadLine();
                    isValid = checkBirthDate(birthDate);
                    if(!isValid){
                        goto birthDate;
                    }
                    else{
                        age = getAge(birthDate);
                        Console.WriteLine("Age is "+age);
                    }
                }
                
                counter++;
            }
            return false;

        }
   

        private Boolean checkFirstNameLastName(String firstName, String lastName){
            Boolean isValid = false;
            const string pattern = @"^([a-z ,.'-]){1,20}$";
            var match = Regex.Match(firstName, pattern);
            if(!match.Success)
                Console.WriteLine("Invalid First Name. Must be 20 characters at least");
            else{
                match = Regex.Match(lastName, pattern);
                if(!match.Success){
                     Console.WriteLine("Invalid Last Name. Must be 20 characters at least");
                }
                else{
                    isValid = true;
                }
            }
            return isValid;
        }
        private Boolean checkBirthDate(string birthDate){        
                Boolean validator = false;
                const string pattern = @"(0\d{1}|1[0-2])\/([0-2]\d{1}|3[0-1])\/(19|20)(\d{2})";
                var match = Regex.Match(birthDate, pattern);
                if(!match.Success){
                    validator = false;
                }
                else{
                    DateTime dateToday = DateTime.Today; // As DateTime
                    var parameterDate = DateTime.ParseExact(birthDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    if(parameterDate > dateToday){
                        validator = false;
                        Console.WriteLine("Invalid BirthDate. Please set a date in the Past");
                    }
                    else{
                        validator = true;
                }
            }
            return validator;
        }
        private String getAge(String birthDate){
            DateTime dateToday = DateTime.Today; // As DateTime
            var parameterDate = DateTime.ParseExact(birthDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var diffOfDates = dateToday - parameterDate;
            return ("Difference of Dates is " + diffOfDates.TotalDays);
        }
    }

    class passengerInfo
    {
        String First_Name { get;set;}
        String Last_Name { get;set;}
        String Birth_Date { get;set;}
        String Age { get;set;}
    }

}
