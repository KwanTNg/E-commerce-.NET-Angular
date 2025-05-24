using System;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

//Implement generic interface and add database as argument
public class GenericRepository<T>(StoreContext context) : IGenericRepository<T> where T : BaseEntity
{


}
