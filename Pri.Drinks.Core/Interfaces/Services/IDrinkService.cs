using Pri.Drinks.Core.Entities;
using Pri.Drinks.Core.Interfaces.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Drinks.Core.Interfaces.Services
{
    public interface IDrinkService
    {
        Task<ResultModel<Drink>> GetAllAsync();
        Task<ResultModel<Drink>> GetByIdAsync(int id);
        Task<ResultModel<Drink>> CreateAsync(string name, int categoryId,
            int alcoholPercentage, IEnumerable<int> propertyIds);
    }
}
