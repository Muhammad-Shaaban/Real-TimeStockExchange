import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor() { }

  showSuccess = (message: string) => {
    Swal.fire({
      position: "top-end",
      icon: "success",
      title: message,
      showConfirmButton: false,
      timer: 1500
    });
  }

  showError = (message: string) => {
    Swal.fire({
      position: "top-end",
      icon: "error",
      title: message,
      showConfirmButton: false,
      timer: 1500
    });
  }

  showWarning = (message: string) => {
    Swal.fire({
      position: "top-end",
      icon: "warning",
      title: message,
      showConfirmButton: false,
      timer: 1500
    });
  }
}
