import { Injectable } from '@angular/core';
import { AddCategoryRequest } from '../../models/add-category-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { category } from '../../models/category.model';
import { environment } from 'src/environments/environment.development';
import { UpdateCategoryRequest } from '../../models/update-category-request.model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient,
    private cookieService: CookieService) { }

  addCategory(model: AddCategoryRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}/api/categories?addAuth=true`, model);
  }


  getAllCategories(): Observable<category[]> {
    return this.http.get<category[]>(`${environment.apiBaseUrl}/api/categories`);
  }

  getCategoryById(id: string): Observable<category> {
    return this.http.get<category>(`${environment.apiBaseUrl}/api/categories/${id}`)
  }

  updateCategory(id: string, updateCategoryRequest: UpdateCategoryRequest): Observable<category> {
    return this.http.put<category>(`${environment.apiBaseUrl}/api/categories/${id}?addAuth=true`, updateCategoryRequest);
  }

  deleteCategory(id: string): Observable<category> {
    return this.http.delete<category>(`${environment.apiBaseUrl}/api/categories/${id}?addAuth=true`);
  }

}
