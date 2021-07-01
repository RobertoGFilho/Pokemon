namespace Core.Databases
{
    using Microsoft.EntityFrameworkCore.Design;
    using System.IO;

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Database>
    {
        public Database CreateDbContext(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + @"\Config.db";
            return new Database(path);
        }
    }
}
