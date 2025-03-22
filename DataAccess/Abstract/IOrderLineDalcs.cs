using Entities.Entities;
using ETicareBitirme.Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
     public interface IOrderLineDal : IEntityRepository<OrderLineManager>

    {
    }
}
