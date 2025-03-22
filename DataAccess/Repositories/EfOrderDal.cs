using DataAccess.Abstract;
using Entities.Context;
using Entities.Entities;
using ETicareBitirme.Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class EfOrderDal : EfEntityRepositoryBase<Order, ETicaretContext>, IOrderDal
    {
    }
}
