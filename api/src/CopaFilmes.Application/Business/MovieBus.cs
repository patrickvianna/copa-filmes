using CopaFilmes.Domain.Entities;
using CopaFilmes.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.Application.Business
{
    public class MovieBus
    {
        public async Task<List<Movie>> Get()
        {
            try
            {
                return await new MoviesService().GetMoviesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Movie> SortListMovies(List<Movie> moviesToSort)
        {
            return moviesToSort.OrderBy(x => x.titulo).ToList();
        }
    }
}
