using AutoMapper;
using ExpenseTracker.Server.DTOs;
using ExpenseTracker.Server.Models;

namespace ExpenseTracker.Mapping
{
    public class ExpenseMapperProfile : Profile
    {
        public ExpenseMapperProfile()
        {
            CreateMap<Expense, ReadExpenseDTO>()
                .ReverseMap().ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<Expense, CreateExpenseDTO>()
                .ReverseMap();
        }
    }
}
