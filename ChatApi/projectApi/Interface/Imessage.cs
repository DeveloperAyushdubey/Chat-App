using Microsoft.Data.SqlClient;
using projectApi.Models;

namespace projectApi.Interface
{
    public interface Imessage
    {
        public bool usptblMessageinsertandUpdate(Message message);

        SqlDataReader uspGetMessageAndSelectMobileNumberMessage(string MSGSendermobileno,string MsgReciveLoginmobileno);


        SqlDataReader uspGetMessageSender(string MSGSendermobileno, string MsgReciveLoginmobileno);

        SqlDataReader uspNotificationCount( string MsgReciveLoginmobileno);

        public bool uspMarkMessagesAsRead(string SenderMobileNo, string MsgReciveLoginmobileno);


        //==============  STORY ==============\\

        public bool usptblstatus_insert(string msgdesc, string createdby);


        public bool usp_DeleteOldStatus(string mobileno);

        SqlDataReader usptblstatusView(string LoginMobileno);
        SqlDataReader usptblstatusViewAllStory();


        public bool uspTBL_UserPorfileInsertUpdate(UserPorfileModel userPorfile);

        SqlDataReader uspTBL_UserPorfileViewDataMobileNoWise(string mobileno);

    }
}
