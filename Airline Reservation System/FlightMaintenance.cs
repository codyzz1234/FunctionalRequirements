using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace Airline_Reservation_System
{

    public struct FlightsInformation
    {
        public String airlineCode;
        public String flightNum;
        public String arrivalStation;

        public String departureStation;

        public String sta;

        public String std;
    };

    internal class FlightMaintenance
    {
        public String someMethod()
        {
            return "You accessed this method";
        }
        public void addNewFlight()
        {

            Console.Clear();
            flightInfoMain:
            String userInput;
            String validation;
            Boolean validator;
            FlightsInformation flightsInformation;
    

            // AirLine Code
            airLineCode:
            Console.Write("AirlineCode: ");
            validation = "airCode";
            userInput = Console.ReadLine();
            validator = validateInput(validation,userInput);
            if(validator == false){
                goto airLineCode;
            }
            else
                flightsInformation.airlineCode = userInput;
            
            
            //Flight Number
            flightNumber:
            Console.Write("Flight Number: ");
            validation = "flightNum";
            userInput = Console.ReadLine();
            validator = validateInput(validation,userInput);
            if(validator == false){
                goto flightNumber;
            }
            else{
                flightsInformation.flightNum = userInput;
            }

            //Arrival Station
            arrivalStation:
            Console.Write("Arrival Station: ");
            validation = "arrivalStat";
            userInput = Console.ReadLine();
            validator = validateInput(validation,userInput);
            if(validator == false)
                goto arrivalStation;
            else
                flightsInformation.arrivalStation = userInput.ToUpper();

            
            //DepartureStation
            departureStation:
            Console.Write("Departure Station: ");
            validation = "departureStat";
            userInput = Console.ReadLine();
            validator = validateInput(validation,userInput);
            if(validator == false)
                goto departureStation;
            else
                flightsInformation.departureStation = userInput.ToUpper();

            //STA
            scheduledSta:
            Console.Write("Input STA(Scheduled Time of Arrival: ");
            userInput = Console.ReadLine();
            validation = "sta";
            validator = validateInput(validation,userInput);

            if(validator == false)
                goto scheduledSta;
            else
                flightsInformation.sta = userInput;


            //STD
            scheduledStd: // lul
            Console.Write("Input STD(Scheduled Time of Departure: ");
            userInput = Console.ReadLine();
            validation = "std";
            validator = validateInput(validation,userInput);

            if(validator == false)
                goto scheduledStd;
            else
                 flightsInformation.std = userInput;


            // // Display Inputted Information
            Console.WriteLine("/n/n");
            DisplayEnteredFlightInformation(flightsInformation);
            Console.WriteLine("Re enter inputs?[Y][N]: ");
            userInput = Console.ReadLine();
            if(userInput == "y" || userInput == "Y"){
                Console.Clear();
                goto flightInfoMain;
            }
            else{
                Console.Clear();
                Boolean checker = doTheseInputsExist(flightsInformation);
                if(checker == true){
                    Console.WriteLine("The flight information you have entered already exists as shown above, please re enter new information");
                    goto flightInfoMain;
                }
                else{
                    Console.WriteLine("This is new info");
                    WriteFlightInfoToCsv(flightsInformation);
                }
                return;
            }
        }

        public void DisplayEnteredFlightInformation(FlightsInformation flightsInformation){
            Console.Clear();
            Console.WriteLine("Airline Code: " + flightsInformation.airlineCode);
            Console.WriteLine("Flight Number : " + flightsInformation.flightNum);
            Console.WriteLine("Arrival Station: " + flightsInformation.arrivalStation);
            Console.WriteLine("Departure Station: " + flightsInformation.departureStation);
            Console.WriteLine("STA (Scheduled Time of Arrival): " + flightsInformation.sta);
            Console.WriteLine("STD (Scheduled Time of Departure) : " + flightsInformation.std);
        }






        private Boolean validateInput(String validation, String userInput)
        {
            if (validation == "airCode")
            {
                if ((userInput.Length != 2))
                {
                    Console.WriteLine("Invalid airline code length should only be two characters");
                    return false;
                }
                else if (Char.IsDigit(userInput[0]) == true){
                    if (Char.IsDigit(userInput[1])) {
                        Console.WriteLine("Second character must be a letter ");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
          
            }

            else if(validation == "flightNum"){
                int flightNumber;
                bool canConvert = Int32.TryParse(userInput,out flightNumber);
                if(canConvert == true){
                    if(flightNumber > 9999 || flightNumber < 1){
                        Console.WriteLine("Flight Number must be between(inclusive) 1-9999");
                        return false;
                    }
                    else{
                        return true;
                    }
                }
                else{
                    Console.WriteLine("Flight number must be a number");
                    return false;
                }
            }

            else if(validation == "arrivalStat" ){
                if(userInput.All(char.IsLetterOrDigit) && userInput.Length == 3){
                    if(Char.IsLetter(userInput[0])){
                        return true;
                    }
                    else{
                        Console.WriteLine("Arrival Station Code must start with a Letter");
                        return false;
                    }
                }
                else{
                    Console.WriteLine("Invalid Arrival Station, must only be 3 characters long and only contain Numbers and Letters");
                    return false;
                }
                
            }

             else if(validation == "departureStat" ){
                if(userInput.All(char.IsLetterOrDigit) && userInput.Length == 3){
                    if(Char.IsLetter(userInput[0])){
                        return true;
                    }
                    else{
                        Console.WriteLine("Departure Station Code must start with a Letter");
                        return false;
                    }
                }
                else{
                    Console.WriteLine("Invalid Departure Station, must only be 3 characters long and only contain Numbers and Letters");
                    return false;
                }
            }
            else if(validation == "sta"){
                string pattern = @"^(?:[01][0-9]|2[0-3]):[0-5][0-9]$";

                Regex r = new Regex(pattern,RegexOptions.IgnoreCase);
                var match = Regex.Match(userInput,pattern);
                if(!match.Success){
                    Console.WriteLine("Invalid 24 hour time format ex:[HH:MM]");
                }
                else{
                    return true;
                }


            }
            else if(validation == "std"){
                string pattern = @"^(?:[01][0-9]|2[0-3]):[0-5][0-9]$"; // regex match 24 hour format
                Regex r = new Regex(pattern,RegexOptions.IgnoreCase);
                var match = Regex.Match(userInput,pattern);
                if(!match.Success){
                    Console.WriteLine("Invalid 24 hour time format ex:[HH:MM]");
                }
                else{
                    return true;
                }

            }
            return false;
        }

        public bool doTheseInputsExist(FlightsInformation flightsInformation){
            Boolean doesExist = false;
            FileWriter fileWriter = new FileWriter();
            doesExist = fileWriter.checkFlightsExist(doesExist, flightsInformation); 
            return doesExist;
        }


        private void WriteFlightInfoToCsv(FlightsInformation flightsInformation){
            FileWriter fileWriter = new FileWriter();
            fileWriter.writeNewFlights(flightsInformation);
            Console.WriteLine("New flight information has been published");
        }
    }

    
    

}
