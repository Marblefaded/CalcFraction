using System.ComponentModel.DataAnnotations;

namespace CalcFraction.Data.ViewModel
{
    public class ClientViewModel
    {
        public string uuid { get; set; }
        public string phoneNumber { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string street { get; set; }
        public string zip { get; set; }
        public string area { get; set; }
        public string cvv { get; set; }
        public string cardNumber { get; set; }
    }
    
    /*!CreditCard:Number
    !CreditCard:cvv
    !uuid
    !job:area
    !location:street
    !location:zip
    !phoneNumber
    !username
    !password*/
}
