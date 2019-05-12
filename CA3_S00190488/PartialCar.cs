
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA3_S00190488
{
    using System;
    using System.Collections.Generic;
    public partial class Car
    {
        //Properties
        public DateTime StartDate1 { get; set; }
        public DateTime EndDate1 { get; set; }

        //methods
        //ToString Method
        public override string ToString()
        {
            return $"Name: {Make} Model: {Model}";
        }
        //Get detail for CarBooking
        public virtual string GetCarDetail()
        {
            return string.Format("CarID: {0}\nMake: {1}\nModel: {2}\nRental Date: {3}\nReturn Date: {4}", Id, Make, Model, StartDate1.ToShortDateString(), EndDate1.ToShortDateString());
        }
    }
}
