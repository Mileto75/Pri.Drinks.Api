using Pri.Drinks.Core.Entities;
using Pri.Drinks.Core.Interfaces.Repositories;
using Pri.Drinks.Core.Interfaces.Services;
using Pri.Drinks.Core.Interfaces.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Drinks.Core.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPropertyRepository _propertyRepository;

        public DrinkService(IDrinkRepository drinkRepository, ICategoryRepository categoryRepository, IPropertyRepository propertyRepository)
        {
            _drinkRepository = drinkRepository;
            _categoryRepository = categoryRepository;
            _propertyRepository = propertyRepository;
        }

        public async Task<ResultModel<Drink>> CreateAsync(string name, int categoryId, int alcoholPercentage, IEnumerable<int> propertyIds)
        {
            //check if categoryId exists
            var categories = _categoryRepository.GetAll();
            if (!categories.Any(c => c.Id == categoryId))
            {
                return new ResultModel<Drink>
                {
                    IsSuccess = false,
                    Errors = new List<string>() { "Unkown category" }
                };
            }
            //check if propertyIds exist
            var properties = _propertyRepository.GetAll();
            if(!propertyIds.All(pi => properties.Any(p => p.Id == pi)))
            {
                return new ResultModel<Drink>
                {
                    IsSuccess = false,
                    Errors = new List<string>() { "Unkown Property" }
                };
            }

            //var props = _propertyRepository.GetAll().Where(p => propertyIds.Contains(p.Id)).ToList();
            //if(properties.Count() != propertyIds.Count())
            //{
            //    return new ResultModel<Drink>
            //    {
            //        IsSuccess = false,
            //        Errors = new List<string>() { "Unkown category" }
            //    };
            //}
            //check if name exists
            if(_drinkRepository
                .GetAll().Any(d => d.Name.ToUpper()
                .Equals(name.ToUpper())))
            {
                return new ResultModel<Drink>
                {
                    IsSuccess = false,
                    Errors = new List<string>() { "Name exists!" }
                };
            }
            //call create method
            var drink = new Drink
            {
                Name = name,
                AlcoholPercentage = alcoholPercentage,
                CategoryId = categoryId,
                Properties = _propertyRepository.GetAll()
                    .Where(p => propertyIds.Contains(p.Id)).ToList(),
            };
            if(!await _drinkRepository.CreateAsync(drink))
            {
                return new ResultModel<Drink>
                {
                    IsSuccess = false,
                    Errors = new List<string>() { "Unknown error, please try again later..." }
                };
            }
            return new ResultModel<Drink> 
            { 
                IsSuccess = true
            };
        }

        public async Task<ResultModel<Drink>> GetAllAsync()
        {
            var drinks = await _drinkRepository.GetAllAsync();
            if(drinks.Count() == 0)
            {
                return new ResultModel<Drink>
                {
                    IsSuccess = false,
                    Errors = new List<string> { "No drinks found!" }
                };
            }
            return new ResultModel<Drink>
            {
                IsSuccess = true,
                Items = drinks
            };
        }

        public async Task<ResultModel<Drink>> GetByIdAsync(int id)
        {
            var drink = await _drinkRepository.GetByIdAsync(id);
            if(drink == null)
            {
                return new ResultModel<Drink>
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Drink not found!" }
                };
            }
            return new ResultModel<Drink>
            {
                IsSuccess = true,
                Items = new List<Drink> { drink },
            };
        }
    }
}
