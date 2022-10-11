﻿using Apartment_Web.Models;

namespace Apartment_Web.Services.IServices
{
    public interface IBaseService
    {
        APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest aPIRequest); // returned type will be Type<T> generic
    }
}
