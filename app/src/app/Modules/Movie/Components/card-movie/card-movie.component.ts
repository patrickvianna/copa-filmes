import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Movie } from 'src/app/Core/Movie';

@Component({
  selector: 'card-movie',
  templateUrl: './card-movie.component.html',
  styleUrls: ['./card-movie.component.scss']
})
export class CardMovieComponent implements OnInit {
  @Input() movie: Movie;
  @Output() addMovie: EventEmitter<any> = new EventEmitter();
  @Output() removeMovie: EventEmitter<any> = new EventEmitter();

  public checked: boolean;

  constructor() { }

  ngOnInit() {
  }
  addMovieWasClicked(event): void {
    this.addMovie.emit([event]);
  }

  removeMovieWasClicked(event): void {
    this.removeMovie.emit([event]);
  }

  onClickCheckbox(event) {
    this.checked = !this.checked;
    if (this.checked)
      this.addMovieWasClicked(event);
    else
      this.removeMovieWasClicked(event);
  }
}
