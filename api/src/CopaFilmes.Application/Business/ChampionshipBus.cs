using CopaFilmes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.Application.Business
{
    public class ChampionshipBus
    {
        private readonly int numberParticipants = 8;

        public Boolean ValidListQuantity(List<Movie> movies)
        {
            if (movies != null && movies.Count() == numberParticipants)
                return true;

            throw new Exception("Campeonato deve conter 8 participantes");
        }

        public Movie GetWinner(Movie movieA, Movie movieB)
        {
            if (movieA.nota == movieB.nota)
                return TieBreaker(movieA, movieB);

            return movieA.nota > movieB.nota ? movieA : movieB;
        }

        public async Task<List<Movie>> RankedList(List<Movie> movies)
        {
            MovieBus moviebus = new MovieBus();

            int rankNumber = GetRankNumber(movies.Count());
            movies = moviebus.SortListMovies(movies);

            return await RankPositions(movies, rankNumber);
        }

        private async Task<List<Movie>> RankPositions(List<Movie> movies, int rankNumber)
        {
            List<Movie> rankList = new List<Movie>();

            while (rankNumber != 1)
            {
                List<Movie> winnersMovies = RankedDispute(movies);
                List<Movie> losersMovies = GetLosers(movies, winnersMovies);
                
                losersMovies = SetRankPosition(losersMovies, rankNumber);
                rankList.AddRange(losersMovies);

                movies = winnersMovies;
                rankNumber--;
            }
            List<Movie> champion = SetRankPosition(movies, rankNumber);
            rankList.AddRange(champion);

            return rankList;
        }

        private async List<Movie> RankedDispute(List<Movie> movies)
        {
            int firstIndex = 0;
            int lastIndex = movies.Count() - 1;
            bool insertOnFirst = true;
            List<Movie> moviesNextStep = new List<Movie>();

            while(firstIndex != lastIndex + 1)
            {
                Movie winner = GetWinner(movies[firstIndex], movies[lastIndex]);
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
            if ((quantityElements % 2) != 0)
                throw new Exception("O número de candidatos deve ser multiplo de 2");
            int rankNumber = 0;

            while(quantityElements != 0)
            {
                quantityElements = quantityElements / 2;
                rankNumber++;
            }

            return rankNumber++;
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

        private Task<List<Movie>> SetRankPosition(List<Movie> movies, int rankNumber)
        {
            movies.ForEach(x => x.rank = rankNumber);
            return movies;
        }
    }
}
