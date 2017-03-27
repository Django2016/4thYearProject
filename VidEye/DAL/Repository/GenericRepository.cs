using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Design.PluralizationServices;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class GenericRepository<TObject> where TObject : class
    {
        protected DbContext _context;
        private readonly PluralizationService _pluralizer = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en"));
        public GenericRepository(DbContext context)
        {
            
            _context = context;

        }

        public GenericRepository()
        {
            var ctx = new VidEyeContext();
            ctx.Configuration.LazyLoadingEnabled = true;
            _context = ctx;
        }

        public ICollection<TObject> GetAll()
        {
            return _context.Set<TObject>().ToList();
        }

        public async Task<List<TObject>> GetAllAsync()
        {
            return await _context.Set<TObject>().ToListAsync();
        }

        public TObject Get(int id)
        {
            return _context.Set<TObject>().Find(id);
        }

        public async Task<TObject> GetAsync(int id)
        {
            return await _context.Set<TObject>().FindAsync(id);
        }

        public TObject Find(Expression<Func<TObject, bool>> match)
        {
            return _context.Set<TObject>().SingleOrDefault(match);
        }

        public async Task<TObject> FindAsync(Expression<Func<TObject, bool>> match)
        {
            return await _context.Set<TObject>().SingleOrDefaultAsync(match);
        }


        public TObject FindOne<TObject>(Expression<Func<TObject, bool>> criteria) where TObject : class
        {
            return GetQuery<TObject>().Where(criteria).FirstOrDefault();
        }


        public IQueryable<TObject> GetQuery<TObject>() where TObject : class
        {
           
            var entityName = GetEntityName<TObject>();
            return ((IObjectContextAdapter)_context).ObjectContext.CreateQuery<TObject>(entityName);
        }

        public ICollection<TObject> FindAll(Expression<Func<TObject, bool>> match)
        {
            return _context.Set<TObject>().Where(match).ToList();
        }

        public IEnumerable<TObject> GetAll<TObject>() where TObject : class
        {
            return GetQuery<TObject>().ToList();
        }

        public async Task<ICollection<TObject>> FindAllAsync(Expression<Func<TObject, bool>> match)
        {
            return await _context.Set<TObject>().Where(match).ToListAsync();
        }

        public bool Any(Expression<Func<TObject, bool>> match)
        {
            return _context.Set<TObject>().Any(match);
        }

        public TObject Add(TObject t)
        {
            _context.Set<TObject>().Add(t);
            _context.SaveChanges();
            return t;
        }

        public async Task<TObject> AddAsync(TObject t)
        {
            _context.Set<TObject>().Add(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public TObject Update(TObject updated, int key)
        {
            if (updated == null)
                return null;

            TObject existing = _context.Set<TObject>().Find(key);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(updated);
                _context.SaveChanges();
            }
            return existing;
        }

        public void Update(TObject updated)
        {
            var fqen = GetEntityName<TObject>();

            object originalItem;
            EntityKey key = ((IObjectContextAdapter)_context).ObjectContext.CreateEntityKey(fqen, updated);
            if (((IObjectContextAdapter)_context).ObjectContext.TryGetObjectByKey(key, out originalItem))
            {
                ((IObjectContextAdapter)_context).ObjectContext.ApplyCurrentValues(key.EntitySetName, updated);
            }
        }


        public async Task<TObject> UpdateAsync(TObject updated, int key)
        {
            if (updated == null)
                return null;

            TObject existing = await _context.Set<TObject>().FindAsync(key);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(updated);
                await _context.SaveChangesAsync();
            }
            return existing;
        }

        public void Delete(TObject t)
        {
            _context.Set<TObject>().Remove(t);
            _context.SaveChanges();
        }

        public async Task<int> DeleteAsync(TObject t)
        {
            _context.Set<TObject>().Remove(t);
            return await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.Set<TObject>();
            _context.SaveChanges();
        }

        public int Count()
        {
            return _context.Set<TObject>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<TObject>().CountAsync();
        }

        private string GetEntityName<TEntity>() where TEntity : class
        {
            return string.Format("{0}.{1}", ((IObjectContextAdapter)_context).ObjectContext.DefaultContainerName, _pluralizer.Pluralize(typeof(TEntity).Name));
        }
    }
}
