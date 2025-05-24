using System;
using Core.Entities;

namespace Core.Interfaces;

//Generic T, restrict what T can be
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    //new method for specification
    Task<T?> GetEntityWithSpec(ISpecification<T> spec);
    //new method for specification
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task<bool> SaveAllAsync();
    bool Exists(int id);


}


