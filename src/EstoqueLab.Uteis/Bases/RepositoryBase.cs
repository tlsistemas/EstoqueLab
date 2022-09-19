using EstoqueLab.Uteis.Bases.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Data.Common;
using System.Data;

namespace EstoqueLab.Uteis.Bases
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {

        protected readonly DbContext context;
        protected DbSet<TEntity> DbSet;

        public RepositoryBase(DbContext context) : base()
        {
            this.context = context;
            DbSet = context.Set<TEntity>();
        }
        public virtual void Add(TEntity entity)
        {
            DbSet.AddAsync(entity);
            SaveChanges();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            await SaveChangesAsync();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> conditions = null, string orderBy = null, string includes = null)
        {
            var query = DbSet.AsNoTracking();

            if (conditions != null)
                query = query.Where(conditions);


            if (!String.IsNullOrWhiteSpace(includes))
            {
                var properties = includes.Split(',', ' ').ToList();
                properties.ForEach(property => query = query.Include(property));
            }

            if (!String.IsNullOrWhiteSpace(orderBy))
                query = query.OrderBy(x => x.Id);

            return query.ToList();
        }
        public virtual async Task<IEnumerable<TEntity>> GetByParamsAsync(
Expression<Func<TEntity, bool>> filter = null,
String orderBy = null,
String includeProps = null,
bool asNoTracking = true)
        {
            return await GetQueryable(filter, orderBy, includeProps, asNoTracking).ToListAsync();
        }
        protected virtual IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter = null,
            String orderBy = null,
            String includeProps = null,
            bool asNoTracking = true)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!String.IsNullOrWhiteSpace(orderBy))
            {
                query = query.OrderBy(orderBy);
            }

            if (!String.IsNullOrWhiteSpace(includeProps))
            {
                query = includeProps.Split(',').Aggregate(query, (q, p) => q.Include(p));
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return query;
        }


        public virtual IEnumerable<TEntity> GetAll()
        {
            var list = context.Set<TEntity>().ToList();
            return list;
        }

        public virtual TEntity GetById(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual void Remove(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                DbSet.Remove(entity);
                SaveChanges();
            }
        }

        public virtual void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
            SaveChanges();
        }

        public async Task<int> RemoveAllAsync()
        {
            context.Set<TEntity>().RemoveRange(await context.Set<TEntity>().ToListAsync());
            return await context.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            await SaveChangesAsync();
        }

        public virtual void Update(TEntity entity)
        {
            DbSet.Update(entity);
            SaveChanges();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChangesAsync();
        }
        private void SaveChanges()
        {
            context.SaveChanges();
        }

        private async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public virtual async Task<List<T>> RawSqlQueryAsync<T>(string query, Func<DbDataReader, T> map)
        {
            var _context = context;
            var command = _context.Database.GetDbConnection().CreateCommand();

            command.CommandText = query;
            command.CommandType = CommandType.Text;

            _context.Database.OpenConnection();

            using (var result = await command.ExecuteReaderAsync())
            {
                var entities = new List<T>();

                while (result.Read())
                {
                    entities.Add(map(result));
                }

                return entities;
            }


        }


        public virtual async Task<int> RawSqlQueryAsync(string query)
        {
            return await context.Database.ExecuteSqlRawAsync(query);
        }

        public Task BulkInsert(List<TEntity> objs)
        {
            return context.BulkInsertAsync(objs, options => options.BatchSize = 100);
        }
    }
}
