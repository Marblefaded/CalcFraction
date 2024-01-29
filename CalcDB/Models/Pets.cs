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
    [Table("Pets")]
    public class Pets:IRowVersion
    {
        [Key]
        public int PetsId { get; set; }
        public string TypeOfAnimal{ get; set; } 
        public string Name { get; set; } 
        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
