import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription, subscribeOn } from 'rxjs';
import { BlogPostService } from '../Services/blog-post.service';
import { BlogPost } from '../Models/blog-post.model';
import { CategoryService } from '../../category/services/category.service';
import { category } from '../../models/category.model';
import { UpdateBlogPost } from '../Models/update-blog-post.model';

@Component({
  selector: 'app-edit-blogpost',
  templateUrl: './edit-blogpost.component.html',
  styleUrls: ['./edit-blogpost.component.css']
})
export class EditBlogpostComponent implements OnInit, OnDestroy {

  id: string | null = null;
  routeSubscription?: Subscription;
  updateBlogPostSubscription?: Subscription;
  getBlogPostSubscription?: Subscription;
  deleteBlogPostSubscription?: Subscription;



  model?: BlogPost;
  categories$?: Observable<category[]>;
  selectedCategories?: string[];


  constructor(private route: ActivatedRoute,
    private blogPostService: BlogPostService,
    private categoyrService: CategoryService,
    private router: Router) { }


  ngOnInit(): void {

    this.categories$ = this.categoyrService.getAllCategories();

    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        // Get blogbost From API
        if (this.id) {
          this.getBlogPostSubscription = this.blogPostService.getBlogPostById(this.id).subscribe({
            next: (response) => {
              this.model = response;
              this.selectedCategories = response.categories.map(x => x.id);
            }
          });
        }
      }
    })
  }

  onFormSubmit(): void {
    //Conver this model to request object 
    if (this.model && this.id) {
      var updateBlogPost: UpdateBlogPost = {
        author: this.model.author,
        content: this.model.content,
        shortDescription: this.model.shortDescription,
        featuredImageUrl: this.model.featuredImageUrl,
        isVisible: this.model.isVisable,
        publishDate: this.model.publishDate,
        title: this.model.title,
        urlHandel: this.model.urlHandel,
        categories: this.selectedCategories ?? []
      };

      this.updateBlogPostSubscription = this.blogPostService.updateBlogPost(this.id, updateBlogPost)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/blogpost');
          }
        });
    }
  }

  onDelete(): void {
    if (this.id) {
      // call service and delete blogpost

      this.deleteBlogPostSubscription = this.blogPostService.deleteBlogPost(this.id)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/blogpost');
          }
        });
    }
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updateBlogPostSubscription?.unsubscribe();
    this.getBlogPostSubscription?.unsubscribe();
    this.deleteBlogPostSubscription?.unsubscribe();
  }
}
