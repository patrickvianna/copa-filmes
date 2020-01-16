import { Injectable } from '@angular/core';
import { BaseService } from './baseService';
import { Movie } from '../Core/Movie';

@Injectable()
export class ChampionshipService {
  private baseUrl = 'championship'; // essa é a string para fazer operações no backend

  constructor(private baseService: BaseService) {  }

  makeChampionship(movies : Array<Movie>): Promise<any> {
    return this.baseService.post(`${this.baseUrl}/GenerateChampionship`, JSON.stringify(movies));
  }
}
