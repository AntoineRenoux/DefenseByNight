import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ToasterService {

  private taostParameter = {
    positionClass: 'toast-bottom-right'
  };

  constructor(private toaster: ToastrService) { }

  success(message: string) {
    this.toaster.success(message, null, this.taostParameter);
  }

  warning(message: string){
    this.toaster.warning(message, null, this.taostParameter);
  }

  error(message: string){
    this.toaster.error(message, null, this.taostParameter);
  }

  info(message: string){
    this.toaster.info(message, null, this.taostParameter);
  }
  
}
