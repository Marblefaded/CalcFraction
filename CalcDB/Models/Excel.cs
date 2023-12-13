using CalcDB.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcDB.Models
{
    [Table("Exceled")]
    public class Excel
    {
        /*[]*/
        [Key]
        public int Id { get; set; }
        public string Artikelnummer { get; set; }
        public string Name { get; set; }
        public int Anzahl { get; set; }
        public int Preis  { get; set; }
        public int Gesamt { get; set; }
    }
}
