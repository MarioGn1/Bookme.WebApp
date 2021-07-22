using AutoMapper;
using Bookme.Data;
using Bookme.Services.Contracts;
using Bookme.ViewModels.HomeModels;
using System.Collections.Generic;
using System.Linq;

namespace Bookme.Services
{
    public class HomeService : IHomeService
    {
        private readonly BookmeDbContext data;
        private readonly IMapper mapper;

        public HomeService(BookmeDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public IEnumerable<ClientHomeViewModel> GetAllCategories()
        {
            var categories = this.data.ServiceCategories.ToList();

            var categoriesDtos = mapper.Map<IEnumerable<ClientHomeViewModel>>(categories);

            return categoriesDtos;
        }
    }
}
