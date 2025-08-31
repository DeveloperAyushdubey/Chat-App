import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ServiceService } from '../../service.service';
import { abort } from 'node:process';

@Component({
  selector: 'app-profile',
  imports: [FormsModule,ReactiveFormsModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
selectedFile:File | null = null
previewUrl:string|ArrayBuffer|null=null
loginMobileNO:string| null=null
myform:FormGroup
constructor(private apiService:ServiceService) {
  this.loginMobileNO = localStorage.getItem("MobileNo");
  this.myform = new FormGroup({
    name: new FormControl('', [Validators.required]),
    about: new FormControl('', [Validators.required]),
    statusline: new FormControl('', [Validators.required]),
    mobileNo: new FormControl({ value: this.loginMobileNO, disabled: true }, [Validators.required]),
    Image: new FormControl(''),
    sno:new FormControl('')
  });
}


ngOnInit() {
  debugger;
  this.loginMobileNO = localStorage.getItem("MobileNo");

  this.apiService.uspTBL_UserPorfileViewDataMobileNoWise(this.loginMobileNO)
    .subscribe((res: any) => {
      if (res && res.length > 0) {
        const user = res[0];

        this.myform.patchValue({
          name: user.name,
          about: user.about,
          statusline: user.statusLine,
        sno:user.sno
          // You can save `sno` in a variable if needed
        });

  
//  console.log("Images :  " + user.image)
  if (user.image) {
  this.previewUrl = user.image;  

  if (user.image instanceof File) {
    const reader = new FileReader();
    reader.onload = e => {
      this.previewUrl = reader.result;
    };
    reader.readAsDataURL(user.image);
  }
} else {
  this.previewUrl = null;  
}

      }
    });
}


 onFileSelected(event: Event) {
  const input = event.target as HTMLInputElement;
  if (input.files && input.files.length > 0) {
    this.selectedFile = input.files[0];

     const reader = new FileReader();
      reader.onload = e => this.previewUrl = reader.result;
      reader.readAsDataURL(this.selectedFile);
  }
}


Postdata() {
   var flag="";
  const formData = new FormData();

  // 👇 Extract values safely
  const name = this.myform.get('name')?.value || '';
  const about = this.myform.get('about')?.value || '';
  const statusline = this.myform.get('statusline')?.value || '';
  const mobileNo = this.myform.get('mobileNo')?.value || '';
  const sno =  this.myform.get('sno')?.value || '';;  // default ID for insert

if(sno){
   flag = "U";
}else{
   flag = "I"; 
}
  // 👇 Append values to formData
  formData.append('Name', name);
  formData.append('About', about);
  formData.append('StatusLine', statusline);
  formData.append('MobileNo', mobileNo);
  formData.append('sno', sno);
  formData.append('Flag', flag);

  if (this.selectedFile) {
    formData.append('file', this.selectedFile, this.selectedFile.name);
  }

  for (let pair of formData.entries()) {
    console.log(`${pair[0]}:`, pair[1]);
  }


  this.apiService.uspTBL_UserProfileInsertUpdate(formData).subscribe({
    next: (res: any) => {
      console.log("✅ Insert Success:", res);
    },
    error: (err) => {
      console.error('❌ API Error:', err);

      
      if (err.error && err.error.errors) {
        const errors = err.error.errors;
        for (const key in errors) {
          console.error(`❌ ${key}: ${errors[key].join(', ')}`);
        }
      } else if (err.error && err.error.message) {
        console.error(`❌ Server Message: ${err.error.message}`);
      }
    }
  });
}

}
