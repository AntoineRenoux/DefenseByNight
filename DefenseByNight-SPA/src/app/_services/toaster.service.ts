import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ToasterService {

  constructor(private toaster: ToastrService) { }

  success(message: string) {
    this.toaster.success(message);
  }

  warning(message: string) {
    this.toaster.warning(message);
  }

  error(message: string) {
    this.toaster.error(message);
  }

  info(message: string) {
    this.toaster.info(message);
  }
  
}
