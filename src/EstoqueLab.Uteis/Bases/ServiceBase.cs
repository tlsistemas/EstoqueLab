using EstoqueLab.Uteis.Bases.Interface;
using System.Data.Common;
using System.Linq.Expressions;

namespace EstoqueLab.Uteis.Bases
{
    public class ServiceBase<TEntity> :IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> repositoryBase;

        public ServiceBase(IRepositoryBase<TEntity> repositoryBase)
        {
            this.repositoryBase = repositoryBase;
        }

        public virtual void Add(TEntity entity)
        {
            repositoryBase.Add(entity);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await repositoryBase.AddAsync(entity);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> conditions = null, string orderBy = null, string includes = null)
        {
            return repositoryBase.Get(conditions, orderBy, includes);
        }
        public async Task<IEnumerable<TEntity>> GetByParamsAsync(
    Expression<Func<TEntity, bool>> filter = null,
    string orderBy = null,
    string includeProps = null,
    bool asNoTracking = true)
        {
            return await repositoryBase.GetByParamsAsync(filter, orderBy, includeProps, asNoTracking);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return repositoryBase.GetAll();
        }
        public virtual TEntity GetById(Int32 id)
        {
            return repositoryBase.GetById(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(Int32 id)
        {
            return await repositoryBase.GetByIdAsync(id);
        }
        public virtual void Remove(TEntity entity)
        {
            repositoryBase.Remove(entity);
        }

        public virtual void Remove(Int32 id)
        {
            repositoryBase.Remove(id);
        }

        public async Task<int> RemoveAllAsync()
        {
            return await repositoryBase.RemoveAllAsync();
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            await repositoryBase.RemoveAsync(entity);
        }

        public virtual void Update(TEntity entity)
        {
            repositoryBase.Update(entity);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            await repositoryBase.UpdateAsync(entity);
        }

        public virtual Task<List<T>> RawSqlQueryAsync<T>(string query, Func<DbDataReader, T> map)
        {
            return repositoryBase.RawSqlQueryAsync<T>(query, map);
        }

        public virtual async Task<int> RawSqlQueryAsync(string query)
        {
            return await repositoryBase.RawSqlQueryAsync(query);
        }

        public Task BulkInsert(List<TEntity> objs)
        {
            return repositoryBase.BulkInsert(objs);
        }
    }

}
