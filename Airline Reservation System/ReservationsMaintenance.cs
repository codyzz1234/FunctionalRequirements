using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;


namespace Airline_Reservation_System
{
    public struct ReservationInformation{
        public String airLineCode;

        public String flightNumber { get; set; }

        public String arrivalStation { get; set; }


        public String departureStation { get; set; }

        public String flightDate { get; set; }

        public String numPassengers { get; set; }

        public String pnrNumber { get; set; }
        }
    internal class ReservationsMaintenance
    {
        public void addReservation()
        {
           reservationsMain:
           String userInput;
           Boolean validator;
           ReservationInformation reservationInformation = new ReservationInformation();
           FlightMaintenanceValidation flightMaintenanceValidation = new FlightMaintenanceValidation();
           ReservationsMaintenanceValidation reservationsMaintenanceValidation = new ReservationsMaintenanceValidation();
           FlightsInformation flightsInformation = new FlightsInformation();
           FlightWriterReader flightWriterReader = new FlightWriterReader();


            Console.WriteLine("Add A New Reservation. ");
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
                reservationInformation.airLineCode = userInput;
            
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
                reservationInformation.flightNumber = userInput;
            }


            //Arrival Station
            arrivalStation:
            Console.Write("Arrival Station: ");
            userInput = Console.ReadLine();
            validator = flightMaintenanceValidation.validateArrivalStat(userInput);
            if(validator == false)
                goto arrivalStation;
            else{
                flightsInformation.arrivalStation = userInput;
                reservationInformation.arrivalStation = userInput.ToUpper();
            }

            //DepartureStation
            departureStation:
            Console.Write("Departure Station: ");
            userInput = Console.ReadLine();
            validator = flightMaintenanceValidation.validateDepartureStat(userInput);
            if(validator == false)
                goto departureStation;
            else{
                flightsInformation.departureStation = userInput;
                reservationInformation.departureStation = userInput.ToUpper();
            }

            //Check if The flight exists
            validator = flightWriterReader.checkFlightExistWhenMakingReserve(flightsInformation);
            if(validator == false){
                Console.WriteLine("Invalid Flight Information Inputted, A flight with this parameters does not exist \n\n");
                Console.WriteLine("Input Again?[Y][N]");
                userInput = Console.ReadLine();
                if(userInput.Equals("Y",StringComparison.CurrentCultureIgnoreCase)){
                    goto reservationsMain;
                }
                else{
                    return;
                }
            }
            
        
        //     //Flight Date
        //     flightDate:
        //     Console.Write("Flight Date: ");
        //     userInput = Console.ReadLine();
        //     validator = reservationsMaintenanceValidation.validateFlightDate(userInput);
        //     if(validator  == false)
        //         goto flightDate;
        //     else
        //         reservationInformation.flightDate = userInput;


        //     //Number Of Passengers
        //     numberOfPassengers:
        //     Console.Write("Number Of Passengers: ");
        //     userInput = Console.ReadLine();
        //     validator = reservationsMaintenanceValidation.validatePassengerNumber(userInput);
        //     if(validator == false)
        //         goto numberOfPassengers;
        //     else
        //         reservationInformation.numPassengers = userInput;  

            

            // // Add Passenger Info
            // addPassengerInfo:
            // Console.WriteLine("Input: ");
            // String userInput = Console.ReadLine();
            // Boolean validator = false;
            // Passengers passengers = new Passengers();
            // validator = passengers.addNewPassenger(validator,userInput);

            //Genarate 6 Digit PNR
            generatePnrNumber:
            String pnrNumber;
            pnrNumber = generatePnrNumber();
            validator = reservationsMaintenanceValidation.validatePnrNo(pnrNumber);
            if(validator == false){
                goto generatePnrNumber;
            }
            else{
                validator = reservationsMaintenanceValidation.checkIfNumExistsInCsv(pnrNumber);
                if(validator == true){
                    goto generatePnrNumber;
                }
                else{
                    reservationInformation.pnrNumber = pnrNumber;
                    //Console.Clear();
                    DisplayReservationInformation(reservationInformation);
                    Console.WriteLine();
                    Console.Write("Do you want to Save this Reservation Info[Y][N][Exit]");
                    userInput = Console.ReadLine();
                    if(userInput.Equals("Y",StringComparison.CurrentCultureIgnoreCase)){
                        ReservationReadWrite reservationReadWrite = new ReservationReadWrite();
                        reservationReadWrite.addNewReservation(reservationInformation);
                        Console.WriteLine("SuccessFully Added A Reservation");
                        return;
                    }
                    else if(userInput.Equals("N",StringComparison.CurrentCultureIgnoreCase)){
                         goto reservationsMain;
                    }
                    else{
                        return;
                    }
                }
            }

        }
        public void DisplayReservationInformation(ReservationInformation reservationInformation){
                Console.WriteLine("Airline Code Is: " + reservationInformation.airLineCode);
                Console.WriteLine("Flight Number Is: " + reservationInformation.flightNumber);
                Console.WriteLine("Arrival Station Is: " + reservationInformation.arrivalStation);
                Console.WriteLine("Departure Stations Is: " + reservationInformation.departureStation);
                Console.WriteLine("Flight Date Is: " + reservationInformation.flightDate);
                Console.WriteLine("Number Of Passeners Is: " + reservationInformation.numPassengers);
                Console.WriteLine("PNR Number is " + reservationInformation.pnrNumber);
        }

            


        public void ListAllReservations(){

        }
        public void searchByPNR(){

        }

        private string generatePnrNumber(){
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
