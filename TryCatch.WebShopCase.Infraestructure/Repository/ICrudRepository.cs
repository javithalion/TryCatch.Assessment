﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TryCatch.WebShopCase.Infraestructure.Repository
{
    public interface ICrudRepository<Type,IdType>
    {
        /// <summary>
        /// Retrieve al the entities from the data source
        /// </summary>
        /// <returns>The list of existing entities</returns>
        IQueryable<Type> FindAll();

        /// <summary>
        /// Retrieve al the entities from the data source which fits with the provided filter
        /// </summary>
        /// <param name="query">The filter to apply</param>
        /// <returns>The list of existing filtered entities</returns>
        IQueryable<Type> FindAll(Expression<Func<Type,bool>> query);

        /// <summary>
        /// Retrieve the entity identified with the provided id. It return null if doesn't exist an entity with the provided ID
        /// </summary>
        /// <param name="id">The Id</param>
        /// <returns></returns>
        Type Get(IdType id);

        /// <summary>
        /// Inserts a new entity into the data source
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        /// <returns>The entity after the insert action</returns>
        Type Insert(Type entity);

        /// <summary>
        /// Updates an existing entity on the data source
        /// </summary>
        /// <param name="entity"></param>
        void Update(Type entity);

        /// <summary>
        /// Deletes an existing entity from the data source
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        void Delete(Type entity);

        /// <summary>
        /// Deletes an existing entity from the data source
        /// </summary>
        /// <param name="id">The id of the entity to delete from the data source</param>
        void Delete(IdType id);
    }
}
