import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { min } from 'rxjs';
import { ServiceService } from '../../service.service';
import { Router } from '@angular/router';
import { error } from 'console';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule,NgIf],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  showLoader = false;


  constructor(private api:ServiceService,private router:Router){}
  loginForm=new FormGroup({
    mobileNo:new FormControl('',[Validators.required,Validators.min(10),Validators.max(10)]),
    password:new FormControl('',[Validators.required])
  })

  
  get mobile(){
    return this.loginForm.get('mobileNo')
  }

  get password(){
    return this.loginForm.get('password')
  }


  onSubmit() {
  this.api.Login(this.loginForm.value).subscribe(
    (res: any) => {
      localStorage.setItem('MobileNo', res.mobileNo);
      localStorage.setItem('Password', res.password);

      // Loader show karna (agar aapne loader variable bana rakha hai)
      this.showLoader = true;

      // 5 second ke baad redirect karo
      setTimeout(() => {
        this.showLoader = false; // Loader hatao
        this.router.navigate(['Home']);
      }, 5000);
    },
    (error) => {
      alert('Login failed. Please try again.');
    }
  );
}

}
