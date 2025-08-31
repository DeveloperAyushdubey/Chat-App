using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Data.SqlClient;
using projectApi.Interface;
using projectApi.Models;
using System.Data;

namespace projectApi.Implement
{
    public class MessageService : Imessage
    {

        private IConfiguration _configuration;
        private readonly string con_str;
        private readonly string staffing;
        SqlCommand objCommand;
        SqlConnection objConnection;
        SqlDataReader dataReader;


        public MessageService(IConfiguration configuration)
        {
            _configuration = configuration;
            con_str = _configuration.GetConnectionString("Const");
        }

        public bool usptblMessageinsertandUpdate(Message message)
        {
            objConnection = new SqlConnection(con_str);
            objConnection.ConnectionString = con_str;
            objConnection.Open();
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "usptblMessageinsertandUpdate";
            objCommand.CommandTimeout = 0;
            //  objCommand.Parameters.AddWithValue("@IBType", "Inbound");
            objCommand.Parameters.AddWithValue("@MsgSenderMobileNumber", message.MsgSenderMobileNumber);
            objCommand.Parameters.AddWithValue("@MsgReciveMobileNumber", message.MsgReciveMobileNumber ?? "");
            objCommand.Parameters.AddWithValue("@MsgDesc ", message.MsgDesc ?? "");
            objCommand.Parameters.AddWithValue("@flag ", message.flag ?? "");
            objCommand.Connection = objConnection;
            int temp = objCommand.ExecuteNonQuery();
            objConnection.Close();
            if (temp > 0)
            {
                return true;
            }
            return false;
        }



        public SqlDataReader uspGetMessageAndSelectMobileNumberMessage(string MSGSendermobileno, string MsgReciveLoginmobileno)
        {
            objConnection = new SqlConnection(con_str);
            objConnection.ConnectionString = con_str;
            objConnection.Open();
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "uspGetMessageAndSelectMobileNumberMessage";
            objCommand.CommandTimeout = 0;
            objCommand.Parameters.AddWithValue("@MSGSendermobileno", MSGSendermobileno);
            objCommand.Parameters.AddWithValue("@MsgReciveLoginmobileno", MsgReciveLoginmobileno);
            objCommand.Connection = objConnection;
            dataReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
            objCommand.Dispose();
            objCommand = null;
            objConnection = null;
            return dataReader;
        }



        public SqlDataReader uspGetMessageSender(string MSGSendermobileno, string MsgReciveLoginmobileno)
        {
            objConnection = new SqlConnection(con_str);
            objConnection.ConnectionString = con_str;
            objConnection.Open();

            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "uspGetMessageSender";
            objCommand.CommandTimeout = 0;
            objCommand.Parameters.AddWithValue("@MSGSendermobileno", MSGSendermobileno);
            objCommand.Parameters.AddWithValue("@MsgReciveLoginmobileno", MsgReciveLoginmobileno);
            objCommand.Connection = objConnection;
            dataReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
            objCommand.Dispose();
            objCommand = null;
            objConnection = null;
            return dataReader;
        }



        public SqlDataReader uspNotificationCount(string MsgReciveLoginmobileno)
        {

            objConnection = new SqlConnection(con_str);
            objConnection.ConnectionString = con_str;
            objConnection.Open();
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandTimeout = 0;
            objCommand.CommandText = "uspNotificationCount";

            objCommand.Parameters.AddWithValue("@MsgReciveLoginmobileno", MsgReciveLoginmobileno);
            objCommand.Connection = objConnection;
            dataReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
            objCommand.Dispose();
            objCommand = null;
            objConnection = null;
            return dataReader;
        }


        public bool uspMarkMessagesAsRead(string MSGSendermobileno, string MsgReciveLoginmobileno)
        {

            objConnection = new SqlConnection(con_str);
            objConnection.ConnectionString = con_str;
            objConnection.Open();
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "usptblMessageinsertandUpdate";
            objCommand.CommandTimeout = 0;
            //  objCommand.Parameters.AddWithValue("@IBType", "Inbound");
            objCommand.Parameters.AddWithValue("@MsgSenderMobileNumber", MSGSendermobileno);
            objCommand.Parameters.AddWithValue("@MsgReciveMobileNumber", MsgReciveLoginmobileno ?? "");
           
            objCommand.Connection = objConnection;
            int temp = objCommand.ExecuteNonQuery();
            objConnection.Close();
            if (temp > 0)
            {
                return true;
            }
            return false;
        }


        public bool usptblstatus_insert(string msgdes,string createdby) 
        {
            objConnection = new SqlConnection(con_str);
            objConnection.ConnectionString = con_str;
            objConnection.Open();
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "usptblstatus_insert";
            objCommand.CommandTimeout = 0;
            objCommand.Parameters.AddWithValue("@Status", msgdes);
            objCommand.Parameters.AddWithValue("MobileNo", createdby);
            objCommand.Connection = objConnection;
            int temp = objCommand.ExecuteNonQuery();
            objConnection.Close();
            if (temp > 0)
            {
                return true;
            }
            return false;
        }

        public bool usp_DeleteOldStatus(string mobileno)
        {
            objConnection = new SqlConnection(con_str);
            objConnection.ConnectionString = con_str;
            objConnection.Open();
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "usp_DeleteOldStatus";
            objCommand.Parameters.AddWithValue("@mobileno", mobileno);
            objCommand.CommandTimeout = 0;
         
            objCommand.Connection = objConnection;
            int temp = objCommand.ExecuteNonQuery();
            objConnection.Close();
            if (temp > 0)
            {
                return true;
            }
            return false;
        }

        public SqlDataReader usptblstatusView(string loginmobileno)
        {
            objConnection = new SqlConnection(con_str);
            objConnection.ConnectionString = con_str;
            objConnection.Open();
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandTimeout = 0;
            objCommand.CommandText = "usptblstatusView";
            objCommand.Parameters.AddWithValue("@mobileno", loginmobileno);
            objCommand.Connection = objConnection;
            dataReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
            objCommand.Dispose();
            objCommand = null;
            objConnection = null;
            return dataReader;
        }


        public SqlDataReader usptblstatusViewAllStory()
        {
            objConnection = new SqlConnection(con_str);
            objConnection.ConnectionString = con_str;
            objConnection.Open();
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandTimeout = 0;
            objCommand.CommandText = "usptblstatusViewAllStory";
            objCommand.Connection = objConnection;
            dataReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
            objCommand.Dispose();
            objCommand = null;
            objConnection = null;
            return dataReader;
        }



        public SqlDataReader uspTBL_UserPorfileViewDataMobileNoWise(string loginmobileno)
        {
            objConnection = new SqlConnection(con_str);
            objConnection.ConnectionString = con_str;
            objConnection.Open();
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandTimeout = 0;
            objCommand.CommandText = "uspTBL_UserPorfileViewDataMobileNoWise";
            objCommand.Parameters.AddWithValue("@mobileno", loginmobileno);
            objCommand.Connection = objConnection;
            dataReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
            objCommand.Dispose();
            objCommand = null;
            objConnection = null;
            return dataReader;
        }
     

        public bool uspTBL_UserPorfileInsertUpdate(UserPorfileModel model)
        {
            objConnection = new SqlConnection(con_str);
            objConnection.ConnectionString = con_str;
            objConnection.Open();
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "uspTBL_UserPorfileInsertUpdate";
            objCommand.CommandTimeout = 0;
              objCommand.Parameters.AddWithValue("@sno", model.sno??0);
            objCommand.Parameters.AddWithValue("@Name", model.Name ?? "");
            objCommand.Parameters.AddWithValue("@MobileNo", model.MobileNo?? "");
            objCommand.Parameters.AddWithValue("@About ", model.About?? "");
            objCommand.Parameters.AddWithValue("@StatusLine ", model.StatusLine?? "");
            objCommand.Parameters.AddWithValue("@Images ", model.Image ?? "");
            objCommand.Parameters.AddWithValue("@flag ", model.Flag ?? "");
            objCommand.Connection = objConnection;
            int temp = objCommand.ExecuteNonQuery();
            objConnection.Close();
            if (temp > 0)
            {
                return true;
            }
            return false;
        }


    

    }
}
