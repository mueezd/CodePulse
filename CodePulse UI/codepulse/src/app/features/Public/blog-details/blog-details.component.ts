import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RouterTestingHarness } from '@angular/router/testing';
import { BlogPostService } from '../../blog-post/Services/blog-post.service';
import { Observable } from 'rxjs';
import { BlogPost } from '../../blog-post/Models/blog-post.model';

@Component({
  selector: 'app-blog-details',
  templateUrl: './blog-details.component.html',
  styleUrls: ['./blog-details.component.css']
})
export class BlogDetailsComponent implements OnInit {

  url: string | null = null;
  blogPost$?: Observable<BlogPost>;

  constructor(private route: ActivatedRoute, private blogPostService: BlogPostService) { }

  ngOnInit(): void {
    this.route.paramMap
    .subscribe({
      next: (params) => {
        this.url = params.get('url');
      }
    });
    //retch blog details by URL

    if(this.url){
      this.blogPost$ =  this.blogPostService.getBlogPostByUrlHandel(this.url);
    }


  }
}
