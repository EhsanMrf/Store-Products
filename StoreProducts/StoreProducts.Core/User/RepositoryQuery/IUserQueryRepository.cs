﻿using Common.Response;
using Common.TransientService;
using StoreProducts.Core.User.Query;

namespace StoreProducts.Core.User.RepositoryQuery;

public interface IUserQueryRepository :ITransientService
{
    Task<ServiceResponse<bool>> Login(UserLoginQuery  query);
}