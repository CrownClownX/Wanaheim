import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  constructor(private http: HttpClient) {

  }

  upload(itemId, photo) : Observable<any> {
      var formData = new FormData();
      formData.append('file', photo);

      return this.http.post<any>(`/api/items/${itemId}/photos`, formData);
  }

  getPhotos(itemId) : Observable<any> {
      return this.http.get<any>(`/api/items/${itemId}/photos`);
  }
}
