import { Component, OnDestroy } from '@angular/core';
import { AddCategoryRequest } from '../../models/add-category-request.model';
import { CategoryService } from '../services/category.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent implements OnDestroy {

  model: AddCategoryRequest;

  constructor(private categoryService : CategoryService) {
    this.model = {
      name: '',
      urlHandel: ''
    };
  }
  

private addCategorySubscription?: Subscription

  onFormSubmit() {
    this.addCategorySubscription = this.categoryService.addCategory(this.model)
    .subscribe({
        next: (response) => {
          console.log('This was successful');
        }
    });
  }


  ngOnDestroy(): void {
    this.addCategorySubscription?.unsubscribe();
  }
}



