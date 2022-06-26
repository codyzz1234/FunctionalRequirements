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
           Console.Clear();
           reservationsMain:
           String userInput;
           Boolean validator;
           ReservationInformation reservationInformation = new ReservationInformation();
           FlightMaintenanceValidation flightMaintenanceValidation = new FlightMaintenanceValidation();
           ReservationsMaintenanceValidation reservationsMaintenanceValidation = new ReservationsMaintenanceValidation();
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
                reservationInformation.flightNumber = userInput;
            }


            //Arrival Station
            arrivalStation:
            Console.Write("Arrival Station: ");
            userInput = Console.ReadLine();
            validator = flightMaintenanceValidation.validateArrivalStat(userInput);
            if(validator == false)
                goto arrivalStation;
            else
                reservationInformation.arrivalStation = userInput.ToUpper();

            //DepartureStation
            departureStation:
            Console.Write("Departure Station: ");
            userInput = Console.ReadLine();
            validator = flightMaintenanceValidation.validateDepartureStat(userInput);
            if(validator == false)
                goto departureStation;
            else
                reservationInformation.departureStation = userInput.ToUpper();

            //Flight Date
            flightDate:
            Console.Write("Departure Station: ");
            userInput = Console.ReadLine();
            validator = reservationsMaintenanceValidation.validateFlightDate(userInput);

            
        }
        public void ListAllReservations(){

        }
        public void searchByPNR(){

        }
    }
}
