using Core.Models;
using Core.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Databases
{
    public class PokemonTypesManager : BaseManager<PokemonType>
    {
        public override async Task<IQueryable<PokemonType>> GetAll()
        {
            if (Database.PokemonTypes.Count() == 0)
            {
                var newPokemonTypes = await Service.GetPokemonTypesAsync();

                if (newPokemonTypes != null)
                {
                    Database.PokemonTypes.AddRange(newPokemonTypes);
                    Database.SaveChanges();
                }
            }

            return (await base.GetAll()).OrderBy(o => o.Name);
        }
    }
}
