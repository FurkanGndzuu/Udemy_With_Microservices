using AutoMapper;
using CatalogService.API.DTOs.Categories;
using CatalogService.API.DTOs.Courses;
using CatalogService.API.Models;

namespace CatalogService.API.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
           CreateMap<Category , CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, GetCategoriesByIdDTO>().ReverseMap();
            CreateMap<Category, GetAllCategoryDTO>().ReverseMap();

            CreateMap<Course ,CourseDTO>().ReverseMap();
            CreateMap<Course, CourseUpdateDTO>().ReverseMap();
            CreateMap<Course, CreateCourseDTO>().ReverseMap();
            CreateMap<Feature, FeatureDTO>().ReverseMap();

        }
    }
}
