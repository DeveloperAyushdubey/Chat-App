import { CommonModule, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { ServiceService } from '../../service.service';
import { FormGroup, FormsModule, NgModel } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-story',
  imports: [NgIf,FormsModule,RouterLink,CommonModule],
  templateUrl: './story.component.html',
  styleUrl: './story.component.css'
})
export class StoryComponent {
showTextStory: boolean = false;
msgDesc:any;
createdby:any;
MyStory:any;
UserStorys: any[] = [];  

constructor(private service:ServiceService)
{
  this.createdby = localStorage.getItem('MobileNo');
  setInterval(() => {
    
  this.service.usp_DeleteOldStatus(this.createdby).subscribe((res: any) => {
  
    this.MyStoryFun();
    this.ViewUserStory();
   });
 }, 200000); 
 this.MyStoryFun();
 this.ViewUserStory();

  
}

MyStoryFun(){

  this.createdby = localStorage.getItem('MobileNo');
  this.service.usptblstatusView(this.createdby).subscribe((res:any)=>{
    this.MyStory = res;
   // console.log(res);
  })
}

toggleTextStory() {
  this.showTextStory = !this.showTextStory;
}



postStatus(){
  if(this.msgDesc!=null){
this.createdby=localStorage.getItem('MobileNo'),
this.service.usptblstatus_insert(this.msgDesc,this.createdby).subscribe((res:any)=>{
  this.MyStoryFun();
   this.ViewUserStory();
  this.msgDesc= null;
})
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




  //========= OTHER STORY ======\\

  ViewUserStory(){
  debugger;
    this.service.usptblstatusViewAllStory().subscribe((res:any)=>{
      this.UserStorys = res
    console.log( this.UserStorys)
    })
  }

  
}
