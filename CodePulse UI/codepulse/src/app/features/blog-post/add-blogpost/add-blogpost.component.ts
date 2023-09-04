import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddBlogPost } from '../Models/add-blog-post.model';
import { BlogPostService } from '../Services/blog-post.service';
import { Route, Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Observable, Subscribable, Subscription } from 'rxjs';
import { category } from '../../models/category.model';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css']
})
export class AddBlogpostComponent implements OnInit, OnDestroy{
  model: AddBlogPost
  isImageSelectorVisavle: boolean = false;
  categories$?: Observable<category[]>;
  imageSelectorSubscription?: Subscription;

  constructor(private blogPostService: BlogPostService,
    private router: Router,
    private categoryService: CategoryService,
    private imageService: ImageService){
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

    this.imageSelectorSubscription = this.imageService.onSelectImage()
    .subscribe({
      next:(selectecImage) => {
        this.model.featuredImageUrl =selectecImage.url;
        this.closeImageSelector();
      }
    })
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



  openImageSelector(): void {
    this.isImageSelectorVisavle = true;
  }

  closeImageSelector(): void {
    this.isImageSelectorVisavle = false;
  }

  ngOnDestroy(): void {
    this.imageSelectorSubscription?.unsubscribe();
  }
}
