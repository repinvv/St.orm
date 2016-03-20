namespace Storm.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Load service retrieves a list of items for navigation property,
    /// caches it and then entity can get a subset of item which are linked
    /// to it by foregn key
    /// TQuery is most of the time the same as TField, it will differ only
    /// for the case where TField is value type. This is needed for 
    /// EntityFramework, since it can only use reference types for entities 
    /// </summary>
    public interface ILoadService
    {
        IStormContext Context { get; }

        /// <summary>
        /// Method to retrieve subset of items for navigation property of the entity
        /// Used for reference type of index
        /// </summary>
        /// <typeparam name="TField">type of navigation property item</typeparam>
        /// <typeparam name="TQuery">type of query</typeparam>
        /// <typeparam name="TIndex">type of index</typeparam>
        /// <param name="navPropIndex">index of navigation property in the entity, needed to mark the caching</param>
        /// <param name="query">query to retrieve the subset, used once then cached</param>
        /// <param name="indexLambda">lambda to run on all items to get their index value to match key of entity</param>
        /// <param name="key">key of entiy to match item index value</param>
        /// <returns>subset of items</returns>
        List<TField> GetList<TField, TQuery, TIndex>(int navPropIndex, 
            Func<IQueryable<TQuery>> query, 
            Func<TField, TIndex> indexLambda, 
            TIndex key);

        /// <summary>
        /// Method to retrieve subset of items for navigation property of the entity
        /// Used for value type of index
        /// </summary>
        /// <typeparam name="TField">type of navigation property item</typeparam>
        /// <typeparam name="TQuery">type of query</typeparam>
        /// <typeparam name="TIndex">type of index</typeparam>
        /// <param name="navPropIndex">index of navigation property in the entity, needed to mark the caching</param>
        /// <param name="query">query to retrieve the subset, used once then cached</param>
        /// <param name="indexLambda">lambda to run on all items to get their index value to match key of entity</param>
        /// <param name="key">key of entiy to match item index value</param>
        /// <returns>subset of items</returns>
        List<TField> GetList<TField, TQuery, TIndex>(int navPropIndex, 
            Func<IQueryable<TQuery>> query, 
            Func<TField, TIndex> indexLambda, 
            TIndex? key)
            where TIndex : struct;

        /// <summary>
        /// Method to retrieve single item for navigation property of the entity
        /// Used for value type of index
        /// </summary>
        /// <typeparam name="TField">type of navigation property item</typeparam>
        /// <typeparam name="TQuery">type of query</typeparam>
        /// <typeparam name="TIndex">type of index</typeparam>
        /// <param name="navPropIndex">index of navigation property in the entity, needed to mark the caching</param>
        /// <param name="query">query to retrieve the subset, used once then cached</param>
        /// <param name="indexLambda">lambda to run on all items to get their index value to match key of entity</param>
        /// <param name="key">key of entiy to match item index value</param>
        /// <returns>single item which key matches the key of entity</returns>
        TField GetSingle<TField, TQuery, TIndex>(int navPropIndex,
            Func<IQueryable<TQuery>> query, 
            Func<TField, TIndex> indexLambda, 
            TIndex key)
            where TField : class;

        /// <summary>
        /// Method to retrieve single item for navigation property of the entity
        /// Used for reference type of index
        /// </summary>
        /// <typeparam name="TField">type of navigation property item</typeparam>
        /// <typeparam name="TQuery">type of query</typeparam>
        /// <typeparam name="TIndex">type of index</typeparam>
        /// <param name="navPropIndex">index of navigation property in the entity, needed to mark the caching</param>
        /// <param name="query">query to retrieve the subset, used once then cached</param>
        /// <param name="indexLambda">lambda to run on all items to get their index value to match key of entity</param>
        /// <param name="key">key of entiy to match item index value</param>
        /// <returns>single item which key matches the key of entity</returns>
        TField GetSingle<TField, TQuery, TIndex>(int navPropIndex, 
            Func<IQueryable<TQuery>> query, 
            Func<TField, TIndex> indexLambda, 
            TIndex? key)
            where TIndex : struct
            where TField : class;
    }
}
