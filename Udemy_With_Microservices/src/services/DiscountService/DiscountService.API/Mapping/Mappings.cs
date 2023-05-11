using AutoMapper;
using DiscountService.API.DTOs;
using DiscountService.API.Models.Entities;

namespace DiscountService.API.Mapping
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<DiscountDTO , Discount>().ReverseMap();
        }
    }
}
