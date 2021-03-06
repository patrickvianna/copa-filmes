import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

import { MovieService } from 'src/app/Services/MovieService';
import { DataService } from 'src/app/Services/dataService';

@Component({
  selector: 'app-rank-movie',
  templateUrl: './rank-movie.component.html',
  styleUrls: ['./rank-movie.component.scss']
})
export class RankMovieComponent implements OnInit {

  public contactFormGroup: FormGroup;
  public rankChampionship;
  
  constructor(private movieProvider: MovieService, private router: Router, private _dataService: DataService) {
    this.rankChampionship = this._dataService.getOption()["RankChampionship"];

    if(!this.rankChampionship)
      this.router.navigateByUrl('/');

    console.log('this.rankChampionship    :', this.rankChampionship   );


  }

  ngOnInit() {
  }


}
