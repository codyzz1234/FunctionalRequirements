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

    }

    class ReservationCsvInfo
    {
        [Name("Airline_Code")]
        public String Airline_Code { get; set; }

        [Name("Flight_Number")]
        public String Flight_Number { get; set; }

        [Name("Arrival Station")]
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
