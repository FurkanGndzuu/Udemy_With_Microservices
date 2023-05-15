using ClientForWeb.Abstractions;
using ClientForWeb.DTOs;
using ClientForWeb.Models;
using ServicesShared;
using System.Text.Json;
using System.Net.Http;
using System.Text;
using ClientForWeb.Helpers;

namespace ClientForWeb.Services
{
    public class CatalogService : ICatalogService
    {
        readonly HttpClient _httpClient;
        readonly PhotoHelper _photoHelper;

        public CatalogService(HttpClient httpClient, PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoHelper = photoHelper;
        }

        public async Task<bool> CreateCategory(CategoryViewModel category)
        {
            var response = await _httpClient.PostAsJsonAsync<CategoryViewModel>("categories", category);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<bool> CreateCourse(CreateCourseDTO createCourse)
        {
            createCourse.UserId = "";
            createCourse.Picture = "";

         

            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(createCourse,new Newtonsoft.Json.Formatting()
            //{
                
            //});   //using Newtonsoft.Json

            //StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsJsonAsync<CreateCourseDTO>("courses/save", createCourse);



            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<bool> DeleteCategory(string Id)
        {
           var response =  await _httpClient.DeleteAsync($"categories/{Id}");
            if(response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<bool> DeleteCourse(string CourseId)
        {
            var response = await _httpClient.DeleteAsync($"courses/{CourseId}");
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<CategoryViewModel>> GetAllCategories()
        {
            var response = await _httpClient.GetAsync("categories");
            Response<List<CategoryViewModel>>? responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>(new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true

            });
            return responseSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourse()
        {
            var response = await _httpClient.GetAsync("courses");
            if(response is null)
                return new List<CourseViewModel>();
            var returnResponse = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
            returnResponse.Data.ForEach(x =>
            {
                x.photoStockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
            });
            return returnResponse.Data;
        }

        public async Task<CourseViewModel> GetCourseById(string Id)
        {
            var response = await _httpClient.GetAsync($"courses/GetCoursesById/{Id}");
            if (response is null)
                return null;
            var returnResponse = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
            return returnResponse.Data;
        }

        public async Task<List<CourseViewModel>> GetCoursesByUserId(string userId)
        {
            var response = await _httpClient.GetAsync($"courses/{userId}");
            if (response is null)
                return null;
            var returnResponse = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
            return returnResponse.Data;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateDTO courseUpdateInput)
        {
          

            var response = await _httpClient.PutAsJsonAsync<CourseUpdateDTO>("courses", courseUpdateInput);
            if(response.IsSuccessStatusCode) 
                return response.IsSuccessStatusCode;
            return false;


        }
    }
}
