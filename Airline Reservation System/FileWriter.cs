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
    public class FileWriter
    {

        // Writing To CSV Files
        public void writeNewFlights(FlightsInformation flightsInformation)
        {
            string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\", "FlightMaintenance.csv");
            path = path.Replace(@"\", @"\\");

            var flights = new List<FlightInfo>
            {
                new FlightInfo{
                    Airline_Code = flightsInformation.airlineCode,
                    Flight_Number = flightsInformation.flightNum,
                    Arrival_Station = flightsInformation.arrivalStation,
                    Departure_Station = flightsInformation.departureStation,
                    STA = flightsInformation.sta,
                    STD = flightsInformation.std
                }
            };

            // Append to the file.
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header again.
                HasHeaderRecord = false,
            };

            using (var stream = File.Open(path, FileMode.Append))
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(flights);
            }

        }


        //Reading CSV Files
        public bool checkFlightsExist(bool doesExist,FlightsInformation flightsInformation)
        {

            string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\", "FlightMaintenance.csv"); // Path For File Location
            path = path.Replace(@"\", @"\\");
            using (var streamReader = new StreamReader(path, Encoding.UTF8))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    var records = csvReader.GetRecords<FlightInfo>().ToList();
                    foreach (var record in records)
                    {
                        if ((record.Airline_Code == flightsInformation.airlineCode)
                           && (record.Flight_Number == flightsInformation.flightNum)
                           && (record.Arrival_Station == flightsInformation.arrivalStation
                           && (record.Departure_Station == flightsInformation.departureStation)))
                        {
                            Console.WriteLine("Airline Code: " + record.Airline_Code);
                            Console.WriteLine("Flight Number: " + record.Flight_Number);
                            Console.WriteLine("Arrival Station: " + record.Arrival_Station);
                            Console.WriteLine("Departure Station: " + record.Departure_Station);
                            doesExist = true;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }

                }
                return doesExist;
            }
        }

        //SearchBy Flight Info
        public void searchFlightNumber(String flightNumber){
            string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\", "FlightMaintenance.csv"); // Path For File Location
            path = path.Replace(@"\", @"\\");
            Boolean found = false;
            using (var streamReader = new StreamReader(path, Encoding.UTF8))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    var records = csvReader.GetRecords<FlightInfo>().ToList();
                    foreach(var record in records){
                        if(record.Flight_Number.Equals(flightNumber)){
                            found = true;
                            Console.WriteLine("Airline Code is: " + record.Airline_Code);
                            Console.WriteLine("Flight Number is: " + record.Flight_Number);
                            Console.WriteLine("Arrival Station is: " + record.Arrival_Station);
                            Console.WriteLine("Departure Station is: " + record.Departure_Station);
                            Console.WriteLine("STA (Scheduled Time of Arrival)  " + record.STA);
                            Console.WriteLine("STD (Scheduled Time of Departure) " + record.STD);
                            Console.WriteLine();
                        }    
                    }
                }
            }
            if(found == false){
                Console.WriteLine("Could not find a matching Flight");
            }
        }
        //Search Airline Code
        public void searchAirLineCode(String airLineCode){
            string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\", "FlightMaintenance.csv"); // Path For File Location
            path = path.Replace(@"\", @"\\");
            Boolean found = false;
            using (var streamReader = new StreamReader(path, Encoding.UTF8))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    var records = csvReader.GetRecords<FlightInfo>().ToList();
                    foreach(var record in records){
                        if(record.Airline_Code.Equals(airLineCode)){
                            found = true;
                            Console.WriteLine("Airline Code is: " + record.Airline_Code);
                            Console.WriteLine("Flight Number is: " + record.Flight_Number);
                            Console.WriteLine("Arrival Station is: " + record.Arrival_Station);
                            Console.WriteLine("Departure Station is: " + record.Departure_Station);
                            Console.WriteLine("STA (Scheduled Time of Arrival)  " + record.STA);
                            Console.WriteLine("STD (Scheduled Time of Departure) " + record.STD);
                            Console.WriteLine();
                        }    
                    }
                }
            }
            if(found == false){
                Console.WriteLine("Could not find a matching Flight");
            }

        }

        public void searchStations(String arrivalStation, String departureStation){
            string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\", "FlightMaintenance.csv"); // Path For File Location
            path = path.Replace(@"\", @"\\");
            Boolean found = false;
            using (var streamReader = new StreamReader(path, Encoding.UTF8))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    var records = csvReader.GetRecords<FlightInfo>().ToList();
                    foreach(var record in records){
                   
                        if((record.Arrival_Station.Equals(arrivalStation)) && (record.Departure_Station.Equals(departureStation))){
                            found = true;
                            Console.WriteLine("Airline Code is: " + record.Airline_Code);
                            Console.WriteLine("Arrival Station is: " + record.Flight_Number);
                            Console.WriteLine("Departure Station is: " + record.Departure_Station);
                            Console.WriteLine("STA (Scheduled Time of Arrival)  " + record.STA);
                            Console.WriteLine("STD (Scheduled Time of Departure) " + record.STD);
                            Console.WriteLine();
                        }    
                            // Console.WriteLine("Airline Code is: " + record.Airline_Code);
                            // Console.WriteLine("Flight Number is: " + record.Flight_Number);
                            // Console.WriteLine("Arrival Station is: " + record.Arrival_Station);
                            // Console.WriteLine("Departure Station is: " + record.Departure_Station);
                            // Console.WriteLine("STA (Scheduled Time of Arrival)  " + record.STA);
                            // Console.WriteLine("STD (Scheduled Time of Departure) " + record.STD);
                            // Console.WriteLine();
                    }
                }
            }
            if(found == false){
                Console.WriteLine("Could not find a matching Flight");
            }

        }

    }





    class FlightInfo
    {
        [Name("Airline_Code")]
        public String Airline_Code { get; set; }

        [Name("Flight_Number")]
        public String Flight_Number { get; set; }

        [Name("Arrival Station")]
        public String Arrival_Station { get; set; }



        [Name("Departure_Station")]

        public String Departure_Station { get; set; }


        [Name("Scheduled_Arrival")]
        public String STA { get; set; }


        [Name("Scheduled_Departure ")]

        public String STD { get; set; }

    }
}
