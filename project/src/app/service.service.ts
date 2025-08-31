import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ServiceService {

  url="https://localhost:7229/api/Chat"
  constructor(private http:HttpClient) { }


  postdata(data:any){
    return this.http.post(this.url,data)
  }


  Login(data:any){
    return this.http.post('https://localhost:7229/api/Chat/Login',data)
  }


  getsinglemobilenumber(data:any){
    return this.http.get(this.url,data)
  }


  GetAllData(){ 
    return this.http.get(this.url)
  }


  getbylikedata(mobile:string){
    return this.http.get('https://localhost:7229/api/Chat/getbynumberdata/'+mobile)
  }



  //======== Msg Api =========== \\

  GetMsgWithMobileNumber(MSGSendermobileno:string,MsgReciveLoginmobileno:string){
    return this.http.get('https://localhost:7229/api/Chat/SingleMobileNumber/'+MSGSendermobileno+'/'+MsgReciveLoginmobileno)
  }


  //============ Msg Sender =========\\

  uspGetMessageSender(MSGSendermobileno:string,MsgReciveLoginmobileno:string){
    // return this.http.get('https://localhost:7229/api/Chat/uspGetMessageSender/'+MSGSendermobileno+'/'+MsgReciveLoginmobileno)
    return this.http.get('https://localhost:7229/api/Chat/SenderMobileNumber/' + MSGSendermobileno + '/' + MsgReciveLoginmobileno);

  }

  //====== MSG Send ======\\

  MsgSend(data:any){
 return this.http.post('https://localhost:7229/api/Chat/Message',data)
  }


  //==== Notification Msg count ====\\

  uspNotificationCount(MsgReciveLoginmobileno:string){
    return this.http.get('https://localhost:7229/api/Chat/uspNotificationCount/'+MsgReciveLoginmobileno)
  }


  //======== Update MsgCount =====\\
 uspMarkMessagesAsUpdate(data: any) {
  
  return this.http.post('https://localhost:7229/api/Chat/uspNotificationUpdate',data)
}




//============ Story upload =========\\
usptblstatus_insert(msgdesc: string, createdby: string) {
  const url = `https://localhost:7229/api/Chat/StatusUpload?msgdesc=${msgdesc}&createdby=${createdby}`;
  return this.http.post(url, {}); // Empty body
}


usp_DeleteOldStatus(mobile: string) {
  return this.http.post(`https://localhost:7229/api/Chat/DeleteOldStatus/${mobile}`, {});
}


usptblstatusView(loginmobile:any){
  return this.http.get('https://localhost:7229/api/Chat/ViewStoryGteByMobileNo/'+ loginmobile)
}



usptblstatusViewAllStory(){
  return this.http.get('https://localhost:7229/api/Chat/usptblstatusViewAllStory')
}




//============ PROFILE INSERT ===================\\

  uspTBL_UserProfileInsertUpdate(data:any){
    return this.http.post('https://localhost:7229/api/Chat/uspTBL_UserProfileInsertUpdate',data)
  }


  uspTBL_UserPorfileViewDataMobileNoWise(mobileno:any){
    return this.http.get('https://localhost:7229/api/Chat/uspTBL_UserPorfileViewDataMobileNoWise/'+mobileno)
  }
}
