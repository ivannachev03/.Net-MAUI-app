using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PMU_APP.Models
{


    public class Car
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public string Information { get; set; }

        public string ImagePath { get; set; }

        public int UserId { get; set; }
    }
}



