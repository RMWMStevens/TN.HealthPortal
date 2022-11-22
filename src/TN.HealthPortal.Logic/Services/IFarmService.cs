﻿using TN.HealthPortal.Logic.Entities;

namespace TN.HealthPortal.Logic.Services
{
    public interface IFarmService
    {
        Task AddAsync(Farm farm);

        Task<Farm> GetByBlnNumberAsync(string blnNumber);

        Task DeleteByBlnNumberAsync(string blnNumber);
    }
}