using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePokemon(int categoryId, int ownerId, Pokemon pokemon)
        {
            var PokemonOwnerEntity = _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
            var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            var PokemonOwner = new PokemonOwner()
            {
                Owner = PokemonOwnerEntity,
                Pokemon = pokemon,
            };

            _context.Add(PokemonOwner);

            var PokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon
            };

            _context.Add(PokemonCategory);

            _context.Add(pokemon);
            return Save();
        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            _context.Remove(pokemon);
            return Save();
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemon.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemon.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal getPokemonRating(int pokeId)
        {
            var review = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);
            if (review.Count() <= 0) return 0;
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemon.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int pokeId)
        {
            return _context.Pokemon.Any(p => p.Id == pokeId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePokemon(int categoryId, int ownerId, Pokemon pokemon)
        {
            _context.Update(pokemon);
            return Save();
        }
    }
}
