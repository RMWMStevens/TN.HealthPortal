using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TN.HealthPortal.Data.EF.Entities;
using TN.HealthPortal.Lib.Models;
using TN.HealthPortal.Lib.Repositories;

namespace TN.HealthPortal.Data.EF.Repositories
{
    internal class TempFarmRepository : ITempFarmRepository
    {
        protected readonly DbContext context;
        protected readonly DbSet<FarmEntity> entities;
        private readonly IMapper mapper;

        public TempFarmRepository(AppDbContext dbContext, IMapper mapper)
        {
            context = dbContext;
            this.mapper = mapper;

            entities = context.Set<FarmEntity>();
        }

        public Farm GetByBlnNumber(string blnNumber)
        {
            var farmEntity = entities.Where(_ => _.BlnNumber == blnNumber).First();
            return mapper.Map<Farm>(farmEntity);
        }

        public void Add(Farm farm)
        {
            var farmEntity = mapper.Map<FarmEntity>(farm);
            context.Add(farmEntity);
            context.SaveChanges();
        }
    }
}
