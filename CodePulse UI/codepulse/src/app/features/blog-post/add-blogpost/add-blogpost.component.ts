import { Component } from '@angular/core';
import { AddBlogPost } from '../Models/add-blog-post.model';
import { BlogPostService } from '../Services/blog-post.service';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css']
})
export class AddBlogpostComponent {
  model: AddBlogPost

  constructor(private blogPostService: BlogPostService,
    private router: Router){
    this.model = {
      title:'',
      shortDescription:'',
      urlHandel:'',
      content:'',
      featuredImageUrl:'',
      author:'',
      isVisible: true,
      publishDate: new Date()
    }
  }


  onFormSubmit(): void {
    this.blogPostService.createBlogPost(this.model)
    .subscribe({
      next: (response) => {
        this.router.navigateByUrl('admin/blogpost')
      }
    });
  }
}
