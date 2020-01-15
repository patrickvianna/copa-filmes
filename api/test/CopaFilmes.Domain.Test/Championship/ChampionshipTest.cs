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
        public void ListHaveCounterEqualEight(int quantityElements)
        {
            List<Movie> movies = new List<Movie>();
            for (int i = 0; i < quantityElements; i++)
            {
                movies.Add(new Movie());
            }
            MovieBus movieBus = new MovieBus();

            bool validate = movieBus.validListQuantity(movies);

            Assert.False(validate);
        }

        [Fact]
        public void ListMustBeSort()
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

            moviesToSort = movieBus.sortListMovies(moviesToSort);

            moviesToSort.ToExpectedObject().ShouldMatch(moviesExpected);
        }

        [Fact]
        public void HaveToWinWhoHasHighestGrade()
        {
            Movie movieA = new Movie() { nota = 2 };
            Movie movieB = new Movie() { nota = 5 };
            MovieBus movieBus = new MovieBus();

            Movie winningMovie = movieBus.getWinner(movieA, movieB);

            movieB.ToExpectedObject(winningMovie);
        }

        [Fact]
        public void HaveToWinWhoHasHighestGrade()
        {
            Movie movieA = new Movie() { nota = 3, titulo = "Beethoven" };
            Movie movieB = new Movie() { nota = 3, titulo = "Assassins Creed" };
            MovieBus movieBus = new MovieBus();

            Movie winningMovie = movieBus.getWinner(movieA, movieB);

            movieB.ToExpectedObject(winningMovie);
        }
    }
}
