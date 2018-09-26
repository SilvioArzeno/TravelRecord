using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecord
{
     public class TravelPost
    {
        [PrimaryKey , AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250)]
        public string Experience { get; set; }
       
    }
}
