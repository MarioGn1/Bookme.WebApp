﻿using AutoMapper;
using Bookme.Data.Models;
using Bookme.ViewModels.OfferedServices;

namespace Bookme.Services
{
    public class BookmeProfile : Profile
    {
        public BookmeProfile()
        {
            this.CreateMap<ServiceCategory, ServiceCategoryViewModel>();
        }
    }
}