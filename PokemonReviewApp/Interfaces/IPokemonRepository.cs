using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        decimal getPokemonRating(int pokeId);
        bool PokemonExists(int pokeId);
        bool CreatePokemon(int categoryId, int ownerId, Pokemon pokemon);
        bool UpdatePokemon(int categoryId, int ownerId, Pokemon pokemon);
        bool DeletePokemon(Pokemon pokemon);
        bool Save();
    }
}
