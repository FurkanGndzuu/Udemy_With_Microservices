using AutoMapper;
using CatalogService.API.Abstractions;
using CatalogService.API.Config;
using CatalogService.API.DTOs.Courses;
using CatalogService.API.Models;
using MongoDB.Driver;
using ServicesShared;

namespace CatalogService.API.Services.Courses
{
    public class CourseService : ICourseService
    {
        readonly IMongoCollection<Course> _courses;
        readonly IMapper _mapper;
        readonly IMongoCollection<Category> _categories;

        public CourseService(IMapper mapper , IDatabaseOptions databaseOptions)
        {
            _mapper = mapper;

            var client = new MongoClient(databaseOptions.ConnectionString);
            var database = client.GetDatabase(databaseOptions.DatabaseName);
            _courses = database.GetCollection<Course>(databaseOptions.CoursesCollection);
            _categories = database.GetCollection<Category>(databaseOptions.CategoriesCollection);
        }

        public async Task<Response<NoContent>> CreateCourseAsync(CreateCourseDTO createCourse)
        {
           Course  course = _mapper.Map<Course>(createCourse);
           await _courses.InsertOneAsync(course);
            return Response<NoContent>.Success(StatusCode: StatusCodes.Status201Created);
        }

        public async Task<Response<NoContent>> DeleteCourseById(string Id)
        {
            if(await _courses.Find(x => Id == Id).FirstOrDefaultAsync() is not null)
                await _courses.DeleteOneAsync(x => x.Id == Id);
            return Response<NoContent>.Success(StatusCode: StatusCodes.Status200OK);
        }

        public async Task<Response<List<CourseDTO>>> GetAllCoursesAsync()
        {
            var courses = await _courses.Find(course => true).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categories.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDTO>>.Success(_mapper.Map<List<CourseDTO>>(courses), 200);
        }
    

        public async Task<Response<CourseDTO>> GetCoursesByIdAsync(string Id)
        {
            var course = await _courses.Find<Course>(x => x.Id == Id).FirstOrDefaultAsync();

            if (course == null)
            {
                return Response<CourseDTO>.Fail("Course not found", 404);
            }
            course.Category = await _categories.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();

            return Response<CourseDTO>.Success(_mapper.Map<CourseDTO>(course), 200);
        }

        public async Task<Response<List<CourseDTO>>> GetCoursesByUserIdAsync(string userId)
        {
            var courses = await _courses.Find<Course>(x => x.userId == userId).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categories.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDTO>>.Success(_mapper.Map<List<CourseDTO>>(courses), 200);
        }

        public async Task<Response<NoContent>> UpdateCourseAsync(CourseUpdateDTO courseUpdateDto)
        {
            var updateCourse = _mapper.Map<Course>(courseUpdateDto);

            var result = await _courses.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updateCourse);

            if (result == null)
            {
                return Response<NoContent>.Fail("Course not found", 404);
            } 

            return Response<NoContent>.Success(204);
        }
    }
}
