using Entities.Enum;
using ETicareBitirme.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public EnumOrderState orderState { get; set; }
        public string UserName { get; set; }
        public string AdressTitle { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }

        public virtual List<OrderLineManager> OrderLines { get; set; }

    }
}
