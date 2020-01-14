import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import {map, startWith} from 'rxjs/operators';

import { MovieService } from 'src/app/Services/MovieService';

@Component({
  selector: 'app-rank-movie',
  templateUrl: './rank-movie.component.html',
  styleUrls: ['./rank-movie.component.scss']
})
export class RankMovieComponent implements OnInit {

  public contactFormGroup: FormGroup;
  constructor(private movieProvider: MovieService, private router: Router) { }
  
  cadastrar() {
    this.movieProvider.all().then(result => {
      this.router.navigate(['consulta/']);
      console.log('result :', result);
    }).catch(result => {
      console.log('result :', result);
    });
  }

  ngOnInit() {
  }


}
