import { Component, Output, EventEmitter } from '@angular/core';
import { ServiceService } from '../../service.service';
import { NgFor, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  imports: [NgFor,FormsModule,NgIf,RouterModule],
  standalone:true,
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
previewUrlUser:string|ArrayBuffer|null=null
  mobile:any;
  data:any;
  searchdata:any;
  GetLoginMobilno:any;  

  MsgCount:any;
  
previewUrl:string|ArrayBuffer|null=null
  @Output() contactSelected = new EventEmitter<string>(); // ✅ Correct usage



  constructor(private api:ServiceService)
  {
    //this.getalldata();
    this.GetMsgCount();
  }


  GetMsgCount(){
    debugger;
  this.GetLoginMobilno = localStorage.getItem('MobileNo');
  this.api.uspNotificationCount(this.GetLoginMobilno).subscribe((res:any)=>{
this.MsgCount = res;
console.log(this.MsgCount)
console.log(res)
  })
  }




  selectContact(mobileNo: string) {
    debugger
    this.contactSelected.emit(mobileNo); // ✅ This now works

const data = {
  MsgSenderMobileNumber: mobileNo,
  MsgReciveMobileNumber: localStorage.getItem('MobileNo')

 
};

this.api.uspMarkMessagesAsUpdate(data).subscribe((res: any) => {
  console.log("Update");

  this.GetMsgCount()
});
  }
  //====== Search mobiledata =======\\


  inputsearchdata() {
    
    if (!this.mobile || this.mobile.trim() === '') {
     
      this.GetMsgCount();
    } else if(this.mobile===10){
      this.api.getsinglemobilenumber(this.mobile).subscribe((res:any)=>{
        this.MsgCount = res
      })
    }
    else {
     this.api.getbylikedata(this.mobile).subscribe((res: any) => {
        this.MsgCount = res;
     
        console.log(res)
      });
    }
  }
  


   selectedStory: any = null;

  openStory(story: any) {
    this.selectedStory = story;

    setTimeout(() => this.closeStory(), 1000);
  }

  closeStory() {
    this.selectedStory = null;
  }



}
