import { Injectable } from '@angular/core';
import { AddBlogPost } from '../Models/add-blog-post.model';
import { Observable } from 'rxjs';
import { BlogPost } from '../Models/blog-post.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  constructor(private http: HttpClient) { }


  createBlogPost(data: AddBlogPost): Observable<BlogPost> {
    return this.http.post<BlogPost>(`${environment.apiBaseUrl}/api/BlogPosts`, data)
  }
}