namespace Backend.Startup
{
    public static class DatabaseConfig
    {
        private static string GetDatabasePath(string fileName)
        {
            var rootDirectory = AppContext.BaseDirectory;

            var projectRoot = Path.GetFullPath(Path.Combine(rootDirectory, "../../../")); 
            
            return Path.Combine(projectRoot, fileName);
        }

        public static string ConnectionString =>
            $"Data Source={GetDatabasePath("Memora.db")}";
    }
}