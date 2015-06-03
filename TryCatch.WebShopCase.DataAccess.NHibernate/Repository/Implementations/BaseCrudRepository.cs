using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.WebShopCase.Infraestructure.Exceptions;
using TryCatch.WebShopCase.Infraestructure.Repository;

namespace TryCatch.WebShopCase.DataAccess.NHibernate.Repository.Implementations
{
    public class BaseCrudRepository<TEntity, TId> : ICrudRepository<TEntity, TId> where TEntity : class
    {
        protected readonly ISession _session;

        public BaseCrudRepository(ISession session)
        {
            _session = session;
        }

        public virtual IQueryable<TEntity> FindAll()
        {
            try
            {
                return _session.QueryOver<TEntity>().List().AsQueryable();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("There was a problem executing FindAll method.", ex);
            }
        }

        public virtual IQueryable<TEntity> FindAll(System.Linq.Expressions.Expression<Func<TEntity, bool>> query)
        {
            try
            {
                return _session.QueryOver<TEntity>().Where(query).List().AsQueryable();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("There was a problem executing FindAll method.", ex);
            }
        }

        public virtual TEntity Get(TId id)
        {
            try
            {
                return _session.Get<TEntity>(id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("There was a problem executing Get method.", ex);
            }
        }

        public virtual TEntity Insert(TEntity entity)
        {
            try
            {
                _session.Save(entity);
                return entity;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("There was a problem executing Insert method.", ex);
            }
        }

        public virtual void Update(TEntity entity)
        {
            try
            {
                _session.Update(entity);                
            }
            catch (Exception ex)
            {
                throw new RepositoryException("There was a problem executing Update method.", ex);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            try
            {
                _session.Delete(entity);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("There was a problem executing Delete method.", ex);
            }
        }

        public virtual void Delete(TId id)
        {
            try
            {
                var entityToDelete = this.Get(id);
                if (entityToDelete != null)
                    _session.Delete(entityToDelete);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("There was a problem executing Delete method.", ex);
            }
        }
    }
}
