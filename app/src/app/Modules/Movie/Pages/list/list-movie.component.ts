import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

import { ChampionshipService } from 'src/app/Services/championShipService';
import { MovieService } from 'src/app/Services/MovieService';
import { Movie } from 'src/app/Core/Movie';
import { DataService } from 'src/app/Services/dataService';

@Component({
  selector: 'app-list-movie',
  templateUrl: './list-movie.component.html',
  styleUrls: ['./list-movie.component.scss']
})
export class ListMovieComponent implements OnInit {

  public movies;
  public counterMovies = 0;
  public pickMovieList = new Array<Movie>();
  constructor(private movieService: MovieService, private championshipService: ChampionshipService, private toastr: ToastrService, private router: Router, private _dataService: DataService) { }

  list() {
    this.movieService.all().then(result => {
      this.movies = result;
    }).catch(result => {
      console.log('result :', result);
    });
  }

  generateChampionship() {
    if (!this.validNumberOfParticipants()) {
      this.toastr.warning("O número de participantes deve ser igual a 8.", "Atenção")
      return;
    }

    this.championshipService.makeChampionship(this.pickMovieList).then(result => {
      this.toastr.success("Campeonato gerado com sucesso", "Sucesso!");
      console.log(result);
      this._dataService.setOption("RankChampionship", result);
      this.router.navigateByUrl('/rank');
    }).catch(error => {
    })
  }

  pickMovie(movie: Movie) {
    this.pickMovieList.push(movie);
    this.counterMovies++;
    console.log('movie :', movie);
    console.log('this.pickMovieList :', this.pickMovieList);
  }

  unselectMovie(movie: Movie) {
    const indexMovie = this.pickMovieList.indexOf(movie);
    this.pickMovieList.splice(indexMovie, 1);
    this.counterMovies--;
  }

  private validNumberOfParticipants() {
    return this.pickMovieList.length === 8;
  }

  ngOnInit() {
    this.list();
  }

}
