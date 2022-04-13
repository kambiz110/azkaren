using Azmoon.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Persistence.Seeding
{
    public interface ISeeder
    {
        Task SeedAsync(DataBaseContext dbContext, IServiceProvider serviceProvider);
    }
}
