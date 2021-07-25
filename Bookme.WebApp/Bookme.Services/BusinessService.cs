using AutoMapper;
using Bookme.Data;
using Bookme.Data.Models;
using Bookme.Services.Contracts;
using Bookme.ViewModels.Business;
using System.Linq;
using System.Threading.Tasks;

namespace Bookme.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly BookmeDbContext data;
        private readonly IMapper mapper;

        public BusinessService(BookmeDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task CreateBusiness(CreateBusinessViewModel model, string userId)
        {
            var business = mapper.Map<Business>(model);
            business.UserId = userId;

            await data.BusinessInfos.AddAsync(business);
            await data.SaveChangesAsync();
        }
    }
}
