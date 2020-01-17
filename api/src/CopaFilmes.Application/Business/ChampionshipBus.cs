using CopaFilmes.Domain.Entities;
using CopaFilmes.Domain.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.Application.Business
{
    public class ChampionshipBus
    {
        private readonly int numberParticipants = 8;

        public async Task<Boolean> ValidListQuantity(List<Movie> movies)
        {
            if (movies != null && movies.Count() == numberParticipants)
                return true;

            throw new InvalidOperationException(Message.ChampionshipMessage.NumberParticipantsShouldBeEqual(numberParticipants));
        }

        public async Task<Movie> GetWinner(Movie movieA, Movie movieB)
        {
            if (movieA.nota == movieB.nota)
                return await TieBreaker(movieA, movieB);

            return movieA.nota > movieB.nota ? movieA : movieB;
        }

        public async Task<List<Movie>> RankedList(List<Movie> movies)
        {
            MovieBus moviebus = new MovieBus();

            int rankNumber = await GetRankNumber(movies.Count());
            movies = await moviebus.SortListMoviesByName(movies);
            movies = await RankPositions(movies, rankNumber);
            movies = await moviebus.SortListMoviesByRank(movies);

            return movies;
        }

        private async Task<List<Movie>> RankPositions(List<Movie> movies, int rankNumber)
        {
            List<Movie> rankList = new List<Movie>();

            while (rankNumber != 1)
            {
                List<Movie> winnersMovies = await RankedDispute(movies);
                List<Movie> losersMovies = await GetLosers(movies, winnersMovies);
                
                losersMovies = await SetRankPosition(losersMovies, rankNumber);
                rankList.AddRange(losersMovies);

                movies = winnersMovies;
                rankNumber--;
            }
            List<Movie> champion = await SetRankPosition(movies, rankNumber);
            rankList.AddRange(champion);

            return rankList;
        }

        private async Task<List<Movie>> RankedDispute(List<Movie> movies)
        {
            int firstIndex = 0;
            int lastIndex = movies.Count() - 1;
            bool insertOnFirst = true;
            List<Movie> moviesNextStep = new List<Movie>();

            while(firstIndex != lastIndex + 1)
            {
                Movie winner = await GetWinner(movies[firstIndex], movies[lastIndex]);
                int indexOnInsert = insertOnFirst ? firstIndex : moviesNextStep.Count() - firstIndex;
                moviesNextStep.Insert(indexOnInsert, winner);

                insertOnFirst = !insertOnFirst;
                firstIndex++;
                lastIndex--;
            }

            return moviesNextStep;
        }

        private async Task<int> GetRankNumber(int quantityElements)
        {
            double logNumber = Math.Log(quantityElements, 2);
            if ((int)logNumber != logNumber)
                throw new InvalidOperationException("O número de candidatos deve ser multiplo de 2");

            return (int)logNumber + 1;
        }

        private async Task<List<Movie>> GetLosers(List<Movie> movies, List<Movie> winners)
        {
            return movies.Except(winners).ToList();
        }

        private async Task<Movie> TieBreaker(Movie movieA, Movie movieB)
        {
            return string.Compare(movieA.titulo, movieB.titulo) < 0
                ? movieA : movieB;
        }

        private async Task<List<Movie>> SetRankPosition(List<Movie> movies, int rankNumber)
        {
            movies.ForEach(x => x.rank = rankNumber);
            return movies;
        }
    }
}
