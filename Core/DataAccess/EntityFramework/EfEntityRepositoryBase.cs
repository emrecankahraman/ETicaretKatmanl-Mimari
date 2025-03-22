using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ETicareBitirme.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new() // IEntity zorunlu hale geldi
        where TContext : DbContext, new() // IdentityDbContext yerine DbContext kullanıl 
    {
        public void Add(TEntity entity)
        {
            //using: IDisposable pattern implementation of c#
            //using bittiği anda garbace collector toplar
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added; //lets the db know, it gets a new value addition 
                context.SaveChanges(); //here it adds
            }
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            using (TContext context = new TContext())
            {
                context.Set<TEntity>().AddRange(entities);
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
                //context.set -> tells hey go to this context // we will get data back 
                //.SingleOrDefault -> youll get one ore NONE returned //like es .FirstOrDefault
            }
        }
        public TEntity Get(int id)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().Find(id);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}