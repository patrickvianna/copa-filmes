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

        public async Task<List<Movie>> SortListMoviesByName(List<Movie> moviesToSort)
        {
            return moviesToSort.OrderBy(x => x.titulo).ToList();
        }

        public async Task<List<Movie>> SortListMoviesByRank(List<Movie> moviesToSort)
        {
            return moviesToSort.OrderBy(x => x.rank).ToList();
        }
    }
}
