using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IPaymentService
{
    //Use backend to create payment intent
    Task<ShoppingCart> CreateOrUpdatePaymentIntent(string cartId);

}
