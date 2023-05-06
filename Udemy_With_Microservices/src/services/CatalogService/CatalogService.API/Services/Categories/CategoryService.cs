using AutoMapper;
using CatalogService.API.Abstractions;
using CatalogService.API.Config;
using CatalogService.API.DTOs.Categories;
using CatalogService.API.Models;
using MongoDB.Driver;
using ServicesShared;

namespace CatalogService.API.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        readonly IMongoCollection<Category> _categories;
        
        readonly IMapper _mapper;

        public CategoryService(IMapper mapper , IDatabaseOptions databaseOptions)
        {
            _mapper = mapper;
            var client = new MongoClient(databaseOptions.ConnectionString);
            var database = client.GetDatabase(databaseOptions.DatabaseName);
            _categories = database.GetCollection<Category>(databaseOptions.CategoriesCollection);
        }

        public async Task<Response<NoContent>> CreateCategoryAsync(CreateCategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categories.InsertOneAsync(category);

            return Response<NoContent>.Success(200);
        }

        public async Task<Response<NoContent>> DeleteCategoryById(string Id)
        {
          if(await _categories.Find(x => x.Id == Id).FirstOrDefaultAsync() is not null)
                await _categories.DeleteOneAsync<Category>(x => x.Id == Id);

          return Response<NoContent>.Success(StatusCode:StatusCodes.Status200OK);
        }

        public async Task<Response<List<GetAllCategoryDTO>>> GetAllCategoriesAsync()
        {
            var categories = await _categories.Find(x => true).ToListAsync();

            return  Response<List<GetAllCategoryDTO>>.Success(_mapper.Map<List<GetAllCategoryDTO>>(categories),
                StatusCode: StatusCodes.Status200OK);

        }

        public async Task<Response<List<GetCategoriesByIdDTO>>> GetCategoriesByIdAsync(string Id)
        {
          var categories = await _categories.FindAsync<Category>(x => x.Id == Id).Result.ToListAsync();

            return Response<List<GetCategoriesByIdDTO>>.Success(data:_mapper.Map<List<GetCategoriesByIdDTO>>(categories),
                StatusCode: StatusCodes.Status200OK);
        }
    }
}
