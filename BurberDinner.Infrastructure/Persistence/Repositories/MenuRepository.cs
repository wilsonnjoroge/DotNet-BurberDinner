
using BurberDinner.Application.Common.Interfaces.Persistence;
using BurberDinner.Domain.MenuAggregate.Entities;

namespace BurberDinner.Infrastructure.Persistence.Repositories
{
    public class MenuRepository : IMenuRepository
    {
      private readonly BurberDinnerDbContext _dbContext;

        public MenuRepository(BurberDinnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Menu menu)
        {
          _dbContext.Add(menu);

          _dbContext.SaveChanges();
        }
    }
}