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
    public class MainClass
    {

        
        static void Main(String[] args)
        {
            
            // string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\", "FlightMaintenance.csv");
            // Console.WriteLine("Path is " + path);
            // path = path.Replace(@"\", @"\\");
            // Console.WriteLine("New Path is " + path);

            // var appendFlightInfo = new List<FlightInfo>
            // {
            //     new FlightInfo{
            //         Airline_Code = "A1",
            //         Flight_Number = "336",
            //         Arrival_Station = "A52",
            //         Departure_Station = "D55",
            //         STA = "22:05",
            //         STD = "15:05"
            //     }
            // };
            // // Append to the file.
            // var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            // {
            //     // Don't write the header again.
            //     HasHeaderRecord = false,
            // };

            // using (var stream = File.Open(path, FileMode.Append))
            // using (var writer = new StreamWriter(stream))
            // using (var csv = new CsvWriter(writer, config))
            // {
            //     csv.WriteRecords(appendFlightInfo);
            // }

            
           
            String choice;
            mainMenu:
            Console.WriteLine("Select A Choice: ");
            Console.WriteLine("[A]. Flight Maintenance:");
            Console.WriteLine("[B]. Reservations");
            choice = Console.ReadLine();
            Console.Clear();
                if (choice == "a" || choice == "A"){
                   flightMain:
                   FlightMaintenance flightMaintenance = new FlightMaintenance();
                   Console.WriteLine("[A]. Add A Flight");
                   Console.WriteLine("[B]. Search For A Flight");
                   Console.WriteLine("[C]. Exit");
                   choice = Console.ReadLine();
                   if (choice == "a" || choice == "A"){
                        flightMaintenance.addNewFlight();
                        Console.Write("Add More Flights?[y][n]: ");
                        choice = Console.ReadLine();
                        if(choice == "y" || choice == "Y"){
                            Console.Clear();
                            goto flightMain;
                        }
                        else{
                            Console.Clear();
                            goto mainMenu;
                        }
                   }

                   else if (choice == "b" || choice == "B"){
                    flightSearch:
                    Console.Clear();
                    Console.WriteLine("[1]. Search By Flight Number");
                    Console.WriteLine("[2]. Search By AirlineCode");
                    Console.WriteLine("[3]. Search By Arrival And Departure Station");
                    Console.WriteLine("[4]. Exit");
                    choice = Console.ReadLine();
                        if(choice == "1"){
                            searchFlightNumber:
                            Console.Clear();
                            flightMaintenance.searchByFlightNumber();
                            Console.Write("Search for Another Flight?[Y][N]: ");
                            choice = Console.ReadLine();
                            if(choice == "Y" || choice == "y" ){
                                goto searchFlightNumber;
                            }
                            else{
                                goto flightSearch;
                            }

                        }

                        else if(choice == "2"){
                            searchAirLineCode:
                            Console.Clear();
                            flightMaintenance.searchByAirlineCode();
                            Console.Write("Search for Another Flight?[Y][N]: ");
                            choice = Console.ReadLine();
                            if(choice == "Y" || choice == "y" ){
                                goto searchAirLineCode;
                            }
                            else{
                                goto flightSearch;
                            }
                        }
                        else if(choice == "3"){
                            searchOriginDestination:
                            Console.Clear();
                            flightMaintenance.searchByOriginDestination();
                            Console.Write("Search for Another Flight?[Y][N]: ");
                            choice = Console.ReadLine();
                            if(choice == "Y" || choice == "y" ){
                                
                                goto searchOriginDestination;
                            }
                            else{
                                goto flightSearch;
                            }
                        }

                        else if(choice == "4"){
                            Console.Clear();
                            goto mainMenu;
                        }
                        else{
                            goto mainMenu;
                        }
                        
                    }
                    else{
                            Console.Clear();
                            goto mainMenu;
                    }
                }
                else if(choice == "b" || choice == "B") {
                        reservationMenu:
                        ReservationsMaintenance reservationsMaintenance = new ReservationsMaintenance();
                        Console.WriteLine("[A]. Create Reservation");
                        Console.WriteLine("[B]. List All Reservations");
                        Console.WriteLine("[C]. Search By PNR number");
                        Console.WriteLine("[D]. Exit");
                        choice = Console.ReadLine();
                        if(choice.Equals("A",StringComparison.OrdinalIgnoreCase)){
                            addReservation:
                            Console.Clear();
                            reservationsMaintenance.addReservation();
                            Console.WriteLine("Add Another Reservation?[Y][N]: ");
                            choice = Console.ReadLine();
                            if(choice.Equals("Y",StringComparison.CurrentCultureIgnoreCase)){
                                goto addReservation;
                            }
                            else{
                                Console.Clear();
                                goto reservationMenu;
                            }
                        }

                        else if(choice.Equals("B",StringComparison.OrdinalIgnoreCase)){
                            Console.Clear();
                            reservationsMaintenance.ListAllReservations();
                        }

                        else if(choice.Equals("C",StringComparison.OrdinalIgnoreCase)){
                            Console.Clear();
                            reservationsMaintenance.searchByPNR();
                        }
                        else{
                            goto mainMenu;
                        }
                }
            else
                {
                   Console.WriteLine("Please Enter either 'a' or 'b' ");
                   goto mainMenu;
                }
        }
    }
    

    // CSV Definition Reader Deserialization Using Classes

}
