using CalcDB.Models;
using NPOI.SS.Formula.Functions;
using System.ComponentModel.DataAnnotations;

namespace CalcFraction.Data.ViewModel
{
    public class ZooViewModel
    {
        private Pets _item;
        public Pets Item => _item;

        public ZooViewModel()
        {
            _item = new Pets();

        }
        public ZooViewModel(Pets item)
        {
            _item = item;
        }
        [Key]
        public int PetsId
        {
            get => _item.PetsId;
            set => _item.PetsId = value;
        }
        public string TypeOfAnimal
        {
            get => _item.TypeOfAnimal;
            set => _item.TypeOfAnimal = value;
        }
        public string Name
        {
            get => _item.Name;
            set => _item.Name = value;
        }
       
    }
}
