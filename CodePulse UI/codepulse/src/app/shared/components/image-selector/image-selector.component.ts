import { Component } from '@angular/core';
import { ImageService } from './image.service';

@Component({
  selector: 'app-image-selector',
  templateUrl: './image-selector.component.html',
  styleUrls: ['./image-selector.component.css']
})
export class ImageSelectorComponent {

  constructor(private imageService: ImageService) {}

  private file?: File;
  fileName: string = '';
  title: string = '';

  onFileUploadChane(event: Event):void{
    const element = event.currentTarget as HTMLInputElement;
    this.file = element.files?.[0];
  } 

  uploadImage(): void{
    if(this.file && this.fileName !== '' && this.title !== ''){
      //image service to upload image

      this.imageService.uploadImage(this.file, this.fileName, this.title)
      .subscribe({
        next: (response) => {
          console.log(response);
        }
      })
    }
  }
}
