using AutoMapper;
using ExpenseTracker.Server.DTOs;
using ExpenseTracker.Server.Models;

namespace ExpenseTracker.Server.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();
        }
    }
}
