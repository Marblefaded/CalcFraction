using CalcDB;
using CalcDB.Models;
using CalcFraction.Data.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace CalcFraction.Data.Service
{
    public class ExcelService
    {
        private static CalcContext DbContext;
        EFRepository<Excel> mRepoProject;
        private string _user;

        public ExcelService(CalcContext context)
        {
            DbContext = context;
            mRepoProject = new EFRepository<Excel>(context, _user);
        }
        public List<ExcelViewModel> GetAll()
        {
            var list = mRepoProject.Get().ToList();
            var result = list.Select(Convert).ToList();
            
            return result;
        }

        private static ExcelViewModel Convert(Excel Model)
        {
            var item = new ExcelViewModel(Model);
            return item;
        }

        /*public ExcelViewModel ReloadItem(ExcelViewModel item)
        {
            var x = mRepoProject.Reload(item.Artikelnummer);
            if (x == null)
            {
                return null;
            }
            return Convert(x);
        }

        public void Delete(ProjectViewModel item)
        {
            var x = mRepoProject.FindById(item.ProjectId);
            mRepoProject.Remove(x);
        }*/

        /*public EditProjectViewModel Update(EditProjectViewModel item)
        {
            var x = mRepoProject.FindByIdForReload(item.ProjectViewModel.ProjectId);
            x.Title = item.Title;
            

            return Convert(mRepoProject.Update(x, item.Item.Timestamp));
        }*/
        /*public EditProjectViewModel Update(EditProjectViewModel item)
        {
            try
            {
                item.ProjectViewModel = Convert(mRepoProject.Update(item.ProjectViewModel.Item, item.ProjectViewModel.Item.Timestamp));

                return item;
            }
            catch (DbUpdateConcurrencyException)
            {
                item.ConcurencyErrorText = "Data is not current, please update";
                item.IsConcurency = true;
                return item;
            }
        }*/

        public ExcelViewModel Create(ExcelViewModel item)
        {
            var newItem = mRepoProject.Create(item.Item);

            return Convert(newItem);
        }
        /*public ExcelViewModel RefreshItem(ExcelViewModel item)
        {
            var x = mRepoProject.Reload(item.ProjectId);

            if (x == null)
            {
                return null;
            }

            return Convert(x);
        }*/
    }
}
