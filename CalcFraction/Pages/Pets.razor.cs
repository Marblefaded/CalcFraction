using CalcFraction.Data.EditModel;
using CalcFraction.Data.Service;
using CalcFraction.Data.ViewModel;
using Microsoft.AspNetCore.Components;
using MatBlazor;
using System.Linq;

namespace CalcFraction.Pages
{
    public class PetsView : ComponentBase
    {
        public List<ZooViewModel> Model { get; set; } = new();
        public ZooViewModel zooModel;
        public EditZooViewModel EditModel = new EditZooViewModel();
        [Inject] ZooService Service { get; set; }
        public int count { get; set; }
        public string mSearch { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {

                Model = Service.GetAll();
                await InvokeAsync(StateHasChanged);
            }
        }

        public string Search
        {
            get => mSearch;

            set
            {
                mSearch = value;
                Filter();

            }
        }
        protected void Filter()
        {
            Model = Service.Filtering(mSearch);
            StateHasChanged();
        }

        public void Counting()
        {
            /*Model = Service.GetAll();*/
            count = Service.RequestOrder();

        }

        public void CreateItem()
        {

            zooModel = new ZooViewModel();
            EditModel.Model = zooModel;
            EditModel.DialogIsOpen = true;
        }

        public void Save(ZooViewModel item)
        {

            if (item.PetsId > 0)
            {
                var newItem = Service.Update(item);
                var index = Model.FindIndex(x => x.PetsId == newItem.PetsId);
                Model[index] = newItem;
            }
            else
            {
                var newItem = Service.Create(item);
                Model.Add(newItem);
            }
            EditModel.DialogIsOpen = false;
            StateHasChanged();
        }
        public void EditItem(ZooViewModel item)
        {
            zooModel = item;
            EditModel.Model = item;
            EditModel.DialogIsOpen = true;
        }

        /*public List<ZooViewModel> Searching()
        {
            *//*Model = Service.GetAll();*//*
            Model = Service.Filtering(search);
            return Model;
        }*/



    }
}
