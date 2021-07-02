using Core.Helpers;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Databases
{

    /// <summary>
    /// Add-Migration Initial -P Core -S CoreDatabaseConfiguration
    /// Remove-Migration -P Core -S CoreDatabaseConfiguration
    /// update-database -P Core -S CoreDatabaseConfiguration
    /// </summary>

    public class Database : DbContext
    {
        #region Fields
        string path;
        static Database instance;
        #endregion
        public static Database Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Database(Constants.DatabasePath);
                    instance.Initialize();
                }

                return instance;
            }

        }

        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<PokemonType> PokemonTypes { get; set; }
        public DbSet<PokemonTypePokemon> PokemonTypesPokemons { get; set; }

        public Database(DbContextOptions<Database> options) : base(options)
        { }

        // contrutor used to build migration on project time
        public Database(string path) : base()
        {
            this.path = path;
        }

        public void Initialize()
        {
            //Database.EnsureDeleted();
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(path))
            {
                optionsBuilder.UseSqlite($"Filename={path}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pokemon>().HasMany(pokemon => pokemon.PokemonTypes).WithOne(type => type.Pokemon).HasForeignKey(type => type.PokemonId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PokemonType>().HasMany(type => type.Pokemons).WithOne(type => type.PokemonType).HasForeignKey(type => type.PokemonTypeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
