import { Component, OnInit } from '@angular/core';
import { AddBlogPost } from '../Models/add-blog-post.model';
import { BlogPostService } from '../Services/blog-post.service';
import { Route, Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Observable } from 'rxjs';
import { category } from '../../models/category.model';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css']
})
export class AddBlogpostComponent implements OnInit{
  model: AddBlogPost

  categories$?: Observable<category[]>;

  constructor(private blogPostService: BlogPostService,
    private router: Router,
    private categoryService: CategoryService){
    this.model = {
      title:'',
      shortDescription:'',
      urlHandel:'',
      content:'',
      featuredImageUrl:'',
      author:'',
      isVisible: true,
      publishDate: new Date(),
      categories: []
    }
  }


  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
  }


  onFormSubmit(): void {
    console.log(this.model);
    this.blogPostService.createBlogPost(this.model)
    .subscribe({
      next: (response) => {
        this.router.navigateByUrl('admin/blogpost')
      }
    });
  }
}
