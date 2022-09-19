using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueLab.Uteis.Bases.Interface
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity GetById(Int32 id);
        Task<TEntity> GetByIdAsync(Int32 id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void Remove(int id);
        void Remove(TEntity entity);
        Task RemoveAsync(TEntity entity);
        Task<int> RemoveAllAsync();
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> conditions = null, String orderBy = null, String includes = null);
        Task<IEnumerable<TEntity>> GetByParamsAsync(
            Expression<Func<TEntity, bool>> filter = null,
            String orderBy = null,
            String includeProps = null,
            bool asNoTracking = true);

        Task<List<T>> RawSqlQueryAsync<T>(string query, Func<DbDataReader, T> map);

        Task<int> RawSqlQueryAsync(string query);

        Task BulkInsert(List<TEntity> objs);
    }

}
