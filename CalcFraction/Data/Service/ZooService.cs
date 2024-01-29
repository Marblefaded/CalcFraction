using CalcDB;
using CalcDB.Models;
using CalcFraction.Data.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace CalcFraction.Data.Service
{
    public class ZooService
    {
        private static CalcContext DbContext;
        EFRepository<Pets> mRepoProject;
        private string _user;

        public ZooService(CalcContext context)
        {
            DbContext = context;
            mRepoProject = new EFRepository<Pets>(context, _user);
        }
        public List<ZooViewModel> GetAll()
        {
            var list = mRepoProject.Get().ToList();
            var result = list.Select(Convert).ToList();
            
            return result;
        }

        private static ZooViewModel Convert(Pets Model)
        {
            var item = new ZooViewModel(Model);
            return item;
        }

        public int RequestOrder()
        {
            var result = DbContext.CountPets();

            return result;
        }

        public List<ZooViewModel> Filtering(string filterValue)
        {
            var filteredListRooms = mRepoProject.GetQuery().Where(x => (x.Name.Contains(filterValue))).ToList();
            var result = filteredListRooms.Select(Convert).ToList();
            return result;
        }

        public ZooViewModel Update(ZooViewModel model)
        {
            var x = mRepoProject.FindById(model.PetsId);
            x.Name = model.Name;
            x.TypeOfAnimal = model.TypeOfAnimal;

            return Convert(mRepoProject.Update(x, model.Item.RowVersion));
        }

        public ZooViewModel Create(ZooViewModel item)
        {
            var newItem = mRepoProject.Create(item.Item);

            return Convert(newItem);
        }
        
    }
}
