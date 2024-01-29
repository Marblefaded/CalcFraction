using CalcFraction.Data;
using CalcFraction.Data.EditModel;
using CalcFraction.Data.ViewModel;
using Microsoft.AspNetCore.Components;

namespace CalcFraction.Pages
{
    public class EditPetsView:ComponentBase
    {
        [Parameter]
        public EditZooViewModel ViewModel { get; set; }
        /*[Parameter]
        public Globals globals { get; set; }*/
        [Parameter]
        public EventCallback<ZooViewModel> Save { get; set; }
        public Globals globals { get; set; }
    }
}
