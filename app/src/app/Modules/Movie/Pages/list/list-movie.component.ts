import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import {map, startWith} from 'rxjs/operators';

import { MovieService } from 'src/app/Services/MovieService';
import { Movie } from 'src/app/Core/Movie';

@Component({
  selector: 'app-list-movie',
  templateUrl: './list-movie.component.html',
  styleUrls: ['./list-movie.component.scss']
})
export class ListMovieComponent implements OnInit {

  public movies;
  public counterMovies = 0;
  public pickMovieList = new Array<Movie>();
  constructor(private movieService: MovieService, private router: Router) { }
  
  list() {
    this.movieService.all().then(result => {
      this.movies = result;
    }).catch(result => {
      console.log('result :', result);
    });
  }

  pickMovie(movie : Movie){
    this.pickMovieList.push(movie);
    this.counterMovies++;
    console.log('movie :', movie);
    console.log('this.pickMovieList :', this.pickMovieList);
  }
  
  unselectMovie(movie : Movie) {
    this.pickMovieList = this.pickMovieList.remove(movie);
    this.counterMovies--;
  }

  ngOnInit() {
    this.list();
  }


}
