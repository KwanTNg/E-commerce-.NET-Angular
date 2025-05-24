using System;
using Core.Entities;

namespace Core.Interfaces;

//Generic T, restrict what T can be
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    //new method for specification (filter, sorting)
    Task<T?> GetEntityWithSpec(ISpecification<T> spec);
    //new method for specification (filter, sorting)
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    //new method for projection
    Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec);
    //new method for projection
    Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec);
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task<bool> SaveAllAsync();
    bool Exists(int id);


}


