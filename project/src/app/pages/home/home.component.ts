import { Component, ElementRef, ViewChild } from '@angular/core';
import { ServiceService } from '../../service.service';
import { CommonModule, NgClass, NgFor } from '@angular/common';
import { RouterLink } from '@angular/router';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  imports: [NgFor,RouterLink,SidebarComponent,FormsModule,NgClass,CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {


SendMsg:any
MsgDescription:any
Recivemsg:any



constructor(private api:ServiceService){}

selectedMobileNo: any ;
loginMobilno:any;


//==== Msg scrooler start to bottom  ====\\

@ViewChild('messagesBox') private messagesBox!: ElementRef;

scrollToBottom(): void {
  try {
    this.messagesBox.nativeElement.scrollToBottom = this.messagesBox.nativeElement.scrollHeight;
  } catch (err) {
    console.error('Scroll error:', err);
  }
}


allMessages: any[] = [];

onContactSelected(mobileNo: string) {
  this.selectedMobileNo = mobileNo;
  this.loginMobilno = localStorage.getItem('MobileNo');

  this.api.GetMsgWithMobileNumber(this.selectedMobileNo, this.loginMobilno).subscribe((receiveRes: any) => {
    const recMsgs = receiveRes.map((m: any) => ({
      ...m,
      type: 'receive',
      timestamp: new Date(m.msgsendtime)
    }));

    this.api.uspGetMessageSender(this.loginMobilno, this.selectedMobileNo).subscribe((sendRes: any) => {
      const sendMsgs = sendRes.map((m: any) => ({
        ...m,
        type: 'send',
        timestamp: new Date(m.msgsendtime)
      }));

      // Merge and sort
      this.allMessages = [...recMsgs, ...sendMsgs].sort(
        (a, b) => a.timestamp.getTime() - b.timestamp.getTime()
      );

      // Scroll to bottom
      setTimeout(() => {
      this.scrollToBottom();
    }, 0);
    });
  });
}

// @ViewChild('messagesBox') messagesBox!: ElementRef;

// scrollToBottom(): void {
//   try {
//     this.messagesBox.nativeElement.scrollTop = this.messagesBox.nativeElement.scrollHeight;
//   } catch (err) {}
// }



//   onContactSelected(mobileNo: string) {
//     debugger;
//     this.selectedMobileNo = mobileNo;
//      this.loginMobilno=localStorage.getItem('MobileNo');
//     //console.log(mobileNo)

//     //=== Send msg ===\\
//   this.api.GetMsgWithMobileNumber(this.selectedMobileNo,this.loginMobilno).subscribe((res:any)=>{
//  this.Recivemsg  =res;
//  setTimeout(() => {
//       this.scrollToBottom();
//     }, 0);

 
//   })

//    //=== Recive msg ===\\
//   this.api.uspGetMessageSender(this.loginMobilno,this.selectedMobileNo).subscribe((res:any)=>{
//     this.SendMsg = res
//      setTimeout(() => {
//       this.scrollToBottom();
//     }, 0);
   
//   })
//   }
  


  sendMessage(){

    if (!this.MsgDescription || this.MsgDescription.trim() === '') {
      
      return;
    }
    const msgpayload={
      //============== Insert Message ===========\\
      msgDesc : this.MsgDescription,
      msgSenderMobileNumber:localStorage.getItem('MobileNo'),
      flag:"I",
      msgReciveMobileNumber:this.selectedMobileNo
     }
 this.api.MsgSend(msgpayload).subscribe((res:any)=>{
  this.onContactSelected(this.selectedMobileNo);
this.MsgDescription = ''
 })

  }


}
