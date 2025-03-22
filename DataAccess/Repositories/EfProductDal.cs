using DataAccess.Abstract;
using Entities.Context;
using Entities.Entities;
using ETicareBitirme.Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class EfProductDal : EfEntityRepositoryBase<Product, ETicaretContext>, IProductDal
    {
        public new List<Product> GetAll(Expression<Func<Product, bool>>? filter = null)
        {
            using (var context = new ETicaretContext())
            {
                // Products tablosuna Category tablosunu ekliyoruz (Include)
                var query = context.Products
                                   .Include(p => p.Category)
                                   .AsQueryable();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                return query.ToList();
            }
        }
    }
}
