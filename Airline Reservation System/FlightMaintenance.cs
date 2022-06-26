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
      
        //Add Flights
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
            Console.Write("Re enter inputs?[Y][N]: ");
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

        private void DisplayEnteredFlightInformation(FlightsInformation flightsInformation){
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
                const string pattern = @"^(([A-zA-Z]){2}|([0-9]){1}([A-zA-Z])|([A-zA-Z]){1}([0-9]))$";
                var match = Regex.Match(userInput,pattern);
                if (match.Success == false)
                {
                    Console.WriteLine("Must either comprise of all letters or If first character is a numeric digit, secondcharacter should be a letter");
                    return false;
                }
                else{
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
                    return true;
                }
            }

            else if(validation == "arrivalStat" ){
                const string pattern = @"^([A-Z]{1})([A-Z0-9]{2})$";
                var match = Regex.Match(userInput,pattern);
                if (match.Success == false)
                {
                    Console.WriteLine("numeric digit is optional. Must at least comprise of all letters.First character must be a letter amd is are all Uppercased");
                    return false;
                }
                else{
                    return true;
                }
                
            }

             else if(validation == "departureStat" ){
                const string pattern = @"^([A-Z]{1})([A-Z0-9]{2})$";
                var match = Regex.Match(userInput,pattern);
                if (match.Success == false)
                {
                    Console.WriteLine("numeric digit is optional. Must at least comprise of all letters.First character must be a letter amd is are all Uppercased");
                    return false;
                }
                else{
                    return true;
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

        private bool doTheseInputsExist(FlightsInformation flightsInformation){
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


        //Search Flights
        
   
        public void searchByFlightNumber(){
          RetryFlightNum:
          String flightNumber;
          String validation = "flightNum";
          Console.Write("Input Flight Number: ");
          flightNumber = Console.ReadLine();
          Console.Clear();
          Boolean isValid = validateInput(validation,flightNumber);
          if(isValid == false)
              goto RetryFlightNum;
            
    
          else{
              FileWriter fileWriter = new FileWriter();
              fileWriter.searchFlightNumber(flightNumber);
          }
        }

        public void searchByAirlineCode(){
          RetryAirLineCode:
          String airLineCode;
          String validation = "airCode";
          Console.Write("Input AirlineCode: ");
          airLineCode = Console.ReadLine();
          Console.Clear();
          Boolean isValid = validateInput(validation,airLineCode);
          if(isValid == false)
              goto RetryAirLineCode;
            
          else{
              FileWriter fileWriter = new FileWriter();
              fileWriter.searchAirLineCode(airLineCode);
          }

        }
        public void searchByOriginDestination(){
          RetryArrival:
          String arrivalStation;
          String validation = "arrivalStat";
          Console.Write("Input Arrival Station: ");
          arrivalStation = Console.ReadLine();
          arrivalStation = arrivalStation.ToUpper();
          Boolean isValid = validateInput(validation,arrivalStation);
          if(isValid == false){
              goto RetryArrival;
          }
           
          else{
              RetryDeparture:
              validation = "departureStat";
              Console.Write("Input Departure Station: ");
              String departureStation = Console.ReadLine();
              departureStation = departureStation.ToUpper();
              isValid = validateInput(validation,departureStation);
              if(isValid == false){
                 goto RetryDeparture;
              }
              else{
                 Console.Clear();
                 FileWriter fileWriter = new FileWriter();
                 fileWriter.searchStations(arrivalStation,departureStation);
              }
          }


        }
    }


    
    

}
