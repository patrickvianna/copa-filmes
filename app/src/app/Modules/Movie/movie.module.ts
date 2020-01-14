import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { BaseService } from '../../Services/baseService';
import { MovieService } from 'src/app/Services/MovieService';
import { ListMovieComponent } from './Pages/list/list-movie.component';
import { RankMovieComponent } from './Pages/rank/rank-movie.component';
import { CardMovieComponent } from './Components/card-movie/card-movie.component';

const routes: Routes = [
  { path: '', component: ListMovieComponent },
  { path: 'rank', component: RankMovieComponent }
];

@NgModule({
  imports: [
    CommonModule,
    HttpModule,
    FormsModule,
    RouterModule.forChild(routes),
  ],
  providers: [
    MovieService,
    BaseService
  ],
  declarations: [ListMovieComponent, RankMovieComponent, CardMovieComponent]
})
export class MovieModule { }
