import { Component, OnInit } from '@angular/core';
import { BlogPostService } from '../Services/blog-post.service';
import { Observable } from 'rxjs';
import { BlogPost } from '../Models/blog-post.model';

@Component({
  selector: 'app-blogpost-list',
  templateUrl: './blogpost-list.component.html',
  styleUrls: ['./blogpost-list.component.css']
})
export class BlogpostListComponent implements OnInit {


  blogPosts$?: Observable<BlogPost[]>;

  constructor(private blogPostService: BlogPostService) { }

  ngOnInit(): void {
    //get all blogposts form api
    this.blogPosts$ = this.blogPostService.getAllBlogPosts();

  }

}
