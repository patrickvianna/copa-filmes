import { Component, OnInit, Input } from '@angular/core';
import { Movie } from 'src/app/Core/Movie';

@Component({
  selector: 'card-rank',
  templateUrl: './card-rank.component.html',
  styleUrls: ['./card-rank.component.scss']
})
export class CardRankComponent implements OnInit {

  @Input() movie: Movie;
  constructor() { }

  ngOnInit() {
  }

}
