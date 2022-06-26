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
    internal class ReservationReadWrite
    {
        //Add New Reservation
        public void addNewReservation(ReservationInformation reservationInformation){
            string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\", "Reservations.csv");
            path = path.Replace(@"\", @"\\");
            var newReservation = new List<ReservationCsvInfo>{
                new ReservationCsvInfo{
                    Airline_Code = reservationInformation.airLineCode,
                    Flight_Number = reservationInformation.flightNumber,
                    Arrival_Station = reservationInformation.arrivalStation,
                    Departure_Station = reservationInformation.departureStation,
                    Flight_Date = reservationInformation.flightDate,
                    Number_Of_Passengers = reservationInformation.numPassengers,
                    PNR_Number = reservationInformation.pnrNumber
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
                csv.WriteRecords(newReservation);
            }
        }

    }

    class ReservationCsvInfo
    {
        [Name("Airline_Code")]
        public String Airline_Code { get; set; }

        [Name("Flight_Number")]
        public String Flight_Number { get; set; }

        [Name("Arrival_Station")]
        public String Arrival_Station { get; set; }

        [Name("Departure_Station")]

        public String Departure_Station { get; set; }

        [Name("Flight_Date")]

        public String Flight_Date { get; set; }

        [Name("Number_Of_Passengers")]

        public String Number_Of_Passengers { get; set; }

        [Name("PNR_Number")]

        public String PNR_Number { get; set; }
    }
}
