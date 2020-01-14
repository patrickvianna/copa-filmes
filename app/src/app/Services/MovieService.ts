import { Injectable } from '@angular/core';
import { BaseService } from './baseService';

@Injectable()
export class MovieService {
  baseUrl = 'movie'; // essa é a string para fazer operações no backend

  constructor(private baseService: BaseService) {  }

  all(): Promise<any> {
    return this.baseService.get(this.baseUrl);
  }
}
