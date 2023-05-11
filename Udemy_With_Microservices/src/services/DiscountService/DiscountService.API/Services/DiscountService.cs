using AutoMapper;
using DiscountService.API.Abstarctions;
using DiscountService.API.DTOs;
using DiscountService.API.Models.Context;
using DiscountService.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using ServicesShared;

namespace DiscountService.API.Services
{
    public class DiscountService : IDiscountService
    {
        readonly DiscountDbContext context;
        readonly IMapper _mapper;

        public DiscountService(IMapper mapper)
        {
            this.context = new DiscountDbContext();
            _mapper = mapper;
        }

        public async Task<Response<NoContent>> DeleteDiscount(int id)
        {
          Discount? discount =await  context.Discounts.FirstOrDefaultAsync(x => x.Id == id);
            if (discount is null)
                Response<NoContent>.Fail("Discount is not found", StatusCodes.Status500InternalServerError);
            context.Discounts.Remove(discount);
            context.SaveChanges();
            return Response<NoContent>.Success(StatusCodes.Status200OK);
        }

        public async Task<Response<List<Discount>>> GetAllDiscounts()
        {
            var discounts =await context.Discounts.ToListAsync();
            return Response<List<Discount>>.Success(discounts, StatusCodes.Status200OK);
        }

        public async Task<Response<Discount>> GetDiscountsByCodeAndUserId(string Code, string userId)
        {
          Discount? discount = await context.Discounts.FirstOrDefaultAsync(x => x.UserId == userId && x.Code == Code);
            if (discount is null)
                return Response<Discount>.Fail("Discount is not found", StatusCodes.Status404NotFound);
            return Response<Discount>.Success(discount, StatusCodes.Status200OK);
        }

        public async Task<Response<Discount>> GetDiscountsById(int id)
        {
           Discount? discount = await context.Discounts.FirstOrDefaultAsync(x => x.Id == id);
            if (discount is null)
                return Response<Discount>.Fail("Discount is not found", StatusCodes.Status404NotFound);
            return Response<Discount>.Success(discount, StatusCodes.Status200OK);
        }

        public async Task<Response<NoContent>> SaveDiscount(DiscountDTO discount)
        {
           Discount newDiscount = _mapper.Map<Discount>(discount);
            await context.Discounts.AddAsync(newDiscount);
            context.SaveChanges();
            return Response<NoContent>.Success(StatusCodes.Status201Created);
        }

        public async Task<Response<NoContent>> UpdateDiscount(DiscountDTO discountDto)
        {
            Discount? discount = await context.Discounts.FirstOrDefaultAsync(x => x.Id == discountDto.Id);
            if(discount is null)
                return Response<NoContent>.Fail("Discount is not found",StatusCodes.Status404NotFound);

            discount = _mapper.Map<Discount>(discountDto);
            context.SaveChanges();
            return Response<NoContent>.Success(StatusCodes.Status200OK);
        }
    }
}
