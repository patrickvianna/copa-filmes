using CopaFilmes.Application.Business;
using CopaFilmes.Domain.Entities;
using ExpectedObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CopaFilmes.Domain.Test.Championship
{
    public class ChampionshipTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(7)]
        [InlineData(9)]
        public void ChampionshipHasToHaveEqualEightMovies(int quantityElements)
        {
            List<Movie> movies = new List<Movie>();
            for (int i = 0; i < quantityElements; i++)
            {
                movies.Add(new Movie());
            }
            ChampionshipBus championshipBus = new ChampionshipBus();

            bool validate = championshipBus.ValidListQuantity(movies);

            Assert.False(validate);
        }

        [Fact]
        public void MovieListMustBeSort()
        {
            List<Movie> moviesExpected = new List<Movie>()
            {
                new Movie() {titulo = "Avengers"},
                new Movie() {titulo = "Beyblade"},
                new Movie() {titulo = "Cars"},
                new Movie() {titulo = "Destructive"}
            };
            List<Movie> moviesToSort = new List<Movie>()
            {
                new Movie() {titulo = "Beyblade"},
                new Movie() {titulo = "Destructive"},
                new Movie() {titulo = "Avengers"},
                new Movie() {titulo = "Cars"}
            };
            MovieBus movieBus = new MovieBus();

            moviesToSort = movieBus.SortListMovies(moviesToSort);

            moviesToSort.ToExpectedObject().ShouldMatch(moviesExpected);
        }

        [Fact]
        public void HaveToWinWhoHasHighestGrade()
        {
            Movie movieA = new Movie() { nota = 2 };
            Movie movieB = new Movie() { nota = 5 };
            ChampionshipBus championshipBus = new ChampionshipBus();

            Movie winningMovie = championshipBus.GetWinner(movieA, movieB);

            movieB.ToExpectedObject().ShouldMatch(winningMovie);
        }

        [Fact]
        public void HaveToWinWhoHasTitleFirstInAlphabeticalOrder()
        {
            Movie movieA = new Movie() { nota = 3, titulo = "Beethoven" };
            Movie movieB = new Movie() { nota = 3, titulo = "Assassins Creed" };
            ChampionshipBus championshipBus = new ChampionshipBus();

            Movie winningMovie = championshipBus.GetWinner(movieA, movieB);

            movieB.ToExpectedObject().ShouldMatch(winningMovie);
        }

        [Fact]
        public void HaveChampion()
        {
            List<Movie> moviesExpected = new List<Movie>()
            {
                new Movie() {titulo = "Beyblade", nota = 2},
                new Movie() {titulo = "Destructive", nota = 4},
                new Movie() {titulo = "Avengers", nota = 3},
                new Movie() {titulo = "Cars", nota = 5}
            };
            ChampionshipBus championshipBus = new ChampionshipBus();

            championshipBus.RankedList(moviesExpected);
        }
    }
}
