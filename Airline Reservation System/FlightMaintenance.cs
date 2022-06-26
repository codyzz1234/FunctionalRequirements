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
            FlightMaintenanceValidation flightMaintenanceValidation = new FlightMaintenanceValidation();
   
            // AirLine Code
            airLineCode:
            Console.Write("AirlineCode: ");
            userInput = Console.ReadLine();
            validator = flightMaintenanceValidation.validateAirCode(userInput);
            if(validator == false){
                goto airLineCode;
            }
            else
                flightsInformation.airlineCode = userInput;
            
            
            //Flight Number
            flightNumber:
            Console.Write("Flight Number: ");
            userInput = Console.ReadLine();
            validator = flightMaintenanceValidation.validateFlightNum(userInput);
            if(validator == false){
                goto flightNumber;
            }
            else{
                flightsInformation.flightNum = userInput;
            }

            //Arrival Station
            arrivalStation:
            Console.Write("Arrival Station: ");
            userInput = Console.ReadLine();
            validator = flightMaintenanceValidation.validateArrivalStat(userInput);
            if(validator == false)
                goto arrivalStation;
            else
                flightsInformation.arrivalStation = userInput.ToUpper();

            
            //DepartureStation
            departureStation:
            Console.Write("Departure Station: ");
            userInput = Console.ReadLine();
            validator = flightMaintenanceValidation.validateDepartureStat(userInput);
            if(validator == false)
                goto departureStation;
            else
                flightsInformation.departureStation = userInput.ToUpper();

            //STA
            scheduledSta:
            Console.Write("Input STA(Scheduled Time of Arrival: ");
            userInput = Console.ReadLine();
            validator = flightMaintenanceValidation.validateSta(userInput);
            if(validator == false)
                goto scheduledSta;
            else
                flightsInformation.sta = userInput;


            //STD
            scheduledStd: // lul
            Console.Write("Input STD(Scheduled Time of Departure: ");
            userInput = Console.ReadLine();
            validator = flightMaintenanceValidation.validateStd(userInput);
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
                    Console.WriteLine("The flight information you have entered already exists as shown above, please re enter new information" + "\n");
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

        private bool doTheseInputsExist(FlightsInformation flightsInformation){
            Boolean doesExist = false;
            FlightWriterReader fileWriter = new FlightWriterReader();
            doesExist = fileWriter.checkFlightsExist(doesExist, flightsInformation); 
            return doesExist;
        }


        private void WriteFlightInfoToCsv(FlightsInformation flightsInformation){
            FlightWriterReader fileWriter = new FlightWriterReader();
            fileWriter.writeNewFlights(flightsInformation);
            Console.WriteLine("New flight information has been published");
        }
        //Search Flights
        
   
        public void searchByFlightNumber(){
          FlightMaintenanceValidation flightMaintenanceValidation = new FlightMaintenanceValidation();
          RetryFlightNum:
          String flightNumber;
          String validation = "flightNum";
          Console.Write("Input Flight Number: ");
          flightNumber = Console.ReadLine();
          Console.Clear();
          Boolean isValid = flightMaintenanceValidation.validateFlightNum(flightNumber);
          if(isValid == false)
              goto RetryFlightNum;
            
    
          else{
              FlightWriterReader fileWriter = new FlightWriterReader();
              fileWriter.searchFlightNumber(flightNumber);
          }
        }

        public void searchByAirlineCode(){
         FlightMaintenanceValidation flightMaintenanceValidation = new FlightMaintenanceValidation();
          RetryAirLineCode:
          String airLineCode;
          String validation = "airCode";
          Console.Write("Input AirlineCode: ");
          airLineCode = Console.ReadLine();
          Console.Clear();
          Boolean isValid = flightMaintenanceValidation.validateAirCode(airLineCode);
          if(isValid == false)
              goto RetryAirLineCode;
            
          else{
              FlightWriterReader fileWriter = new FlightWriterReader();
              fileWriter.searchAirLineCode(airLineCode);
          }

        }
        public void searchByOriginDestination(){
          FlightMaintenanceValidation flightMaintenanceValidation = new FlightMaintenanceValidation();
          RetryArrival:
          String arrivalStation;
          String validation = "arrivalStat";
          Console.Write("Input Arrival Station: ");
          arrivalStation = Console.ReadLine();
          arrivalStation = arrivalStation.ToUpper();
          Boolean isValid = flightMaintenanceValidation.validateArrivalStat(arrivalStation);
          if(isValid == false){
              goto RetryArrival;
          }
          else{
              RetryDeparture:
              validation = "departureStat";
              Console.Write("Input Departure Station: ");
              String departureStation = Console.ReadLine();
              departureStation = departureStation.ToUpper();
              isValid = flightMaintenanceValidation.validateDepartureStat(departureStation);
              if(isValid == false){
                 goto RetryDeparture;
              }
              else{
                 Console.Clear();
                 FlightWriterReader fileWriter = new FlightWriterReader();
                 fileWriter.searchStations(arrivalStation,departureStation);
              }
          }


        }
    }


    
    

}
