namespace CatalogService.API.Config
{
    public interface IDatabaseOptions
    {
        public string CoursesCollection { get; set; }
        public string CategoriesCollection { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
