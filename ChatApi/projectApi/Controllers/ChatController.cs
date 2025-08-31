using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projectApi.Interface;
using projectApi.Models;

namespace projectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private readonly ChatapiContext _context;
        private readonly Imessage _imessage;
        string ImagePath = "https://localhost:7229/uploads/";
        public ChatController(ChatapiContext context,Imessage imessage)
        {
            _context = context;
            _imessage = imessage;   
        }
        [HttpGet]
        public IActionResult Index()
        {
           
            var list = _context.Logins.ToList();   
            return Ok(list);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] Login obj)
        {
            // Check if user exists
            var user = _context.Logins.FirstOrDefault(x => x.MobileNo == obj.MobileNo && x.Password == obj.Password);

            if (user != null)
            {
                // User found — login successful
                return Ok(user);
            }
            else
            {
                // User not found — insert new user
                var existingMobile = _context.Logins.FirstOrDefault(x => x.MobileNo == obj.MobileNo);

                if (existingMobile != null)
                {
                    // Mobile number already exists but password didn't match
                    return Unauthorized("Incorrect password.");
                }

                // Insert new user (registration)
                var newUser = new Login
                {
                    MobileNo = obj.MobileNo,
                    Password = obj.Password
                };

                _context.Logins.Add(newUser);
                _context.SaveChanges();

                return Ok(newUser); // Return the newly created user
            }
        }



        [HttpPost]
        public IActionResult created(Login obj)
        {
            _context.Logins.Add(obj);
            _context.SaveChanges();
            return Ok();
        }


        [HttpGet("{mobile}")]
        public IActionResult getbymobile(string mobile)
        {
            try
            {
                var mob = _context.Logins
                    .Where(x => x.MobileNo == mobile)
                    .Select(x => new
                    {
                        MsgSenderMobileNumber = x.MobileNo,
                     
                    })
                    .FirstOrDefault();

                if (mob == null)
                    return NotFound();

                return Ok(mob);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        [HttpGet("getbynumberdata/{mobile}")]
        public IActionResult GetByNumberData(string mobile)
        {
            var data = _context.Logins
                .Where(x => x.MobileNo.StartsWith(mobile))
                .Select(x => new {
                    MsgSenderMobileNumber = x.MobileNo,
                    
                
                })
                .ToList();

            return Ok(data);
        }









        //// ==============   Message function ==================\\
        ///


        [HttpPost ("Message")]

        public IActionResult InsertMessage(Message message)
        {
            message.flag = "I";
            _imessage.usptblMessageinsertandUpdate(message);
            return Ok();
        }



        [HttpPut("Message")]
        public IActionResult PutMessage(Message message) 
        {
            message.flag = "U";
            _imessage.usptblMessageinsertandUpdate(message);
            return Ok();
        }



        [HttpGet ("SingleMobileNumber/{MSGSendermobileno}/{MsgReciveLoginmobileno}")]

        public IActionResult getsinglemobileNoAndMessage(string MSGSendermobileno, string MsgReciveLoginmobileno)
        {
            List<Message> list =new List<Message>();
           var data= _imessage.uspGetMessageAndSelectMobileNumberMessage(MSGSendermobileno, MsgReciveLoginmobileno);

            while (data.Read())
            {
                list.Add(new Message
                {
                    MsgDesc = data["MsgDesc"].ToString(),
                    Msgsendtime = Convert.ToDateTime(data["Sendertime"])

                });
              
            }
            data.Close();
            return Ok(list);
        }



        [HttpGet("SenderMobileNumber/{MSGSendermobileno}/{MsgReciveLoginmobileno}")]

        public IActionResult uspGetMessageSender(string MSGSendermobileno, string MsgReciveLoginmobileno)
        {
            List<Message> list = new List<Message>();
            var data = _imessage.uspGetMessageSender(MSGSendermobileno, MsgReciveLoginmobileno);

            while (data.Read())
            {
                list.Add(new Message
                {
                    MsgDesc = data["MsgDesc"].ToString(),
                    Msgsendtime = Convert.ToDateTime(data["Sendertime"])

                });

            }
            data.Close();
            return Ok(list);
        }


        [HttpGet("uspNotificationCount/{MsgRecivemobileno}")]

        public IActionResult uspNotificationCount(string MsgRecivemobileno)
        {
            List<Message> list = new List<Message>();
            var data = _imessage.uspNotificationCount(MsgRecivemobileno);

            while (data.Read())
            {

                string senderimage = data["SenderImage"].ToString();
                string finalimage = string.IsNullOrEmpty(senderimage) ? ImagePath + "default-profile.png" : ImagePath + senderimage;
                list.Add(new Message
                {
                    MsgCount = Convert.ToInt64(data["unreadmsg"]),
                    MsgSenderMobileNumber = data["MsgSenderMobileNumber"].ToString(),
                    Image = finalimage,
                    SenderName = data["SenderName"].ToString()
                });

            }
            data.Close();
            return Ok(list);
        }


        [HttpPost("uspNotificationUpdate")]
        public IActionResult uspNotificationUpdate(
  MessageRequestModel request)
        {
            var message = new Message
            {
                MsgSenderMobileNumber = request.MsgSenderMobileNumber,
                MsgReciveMobileNumber = request.MsgReciveMobileNumber,
                flag = "U"
                // set other defaults if necessary
            };
            _imessage.usptblMessageinsertandUpdate(message);
            return Ok();
        }



        ///===================== Story ========================\\
        ///

        [HttpPost("StatusUpload")]
        public IActionResult StatusInsert(string msgdesc, string createdby)
        {
            _imessage.usptblstatus_insert(msgdesc, createdby);
            return Ok();
        }

        [HttpPost("DeleteOldStatus/{mobileno}")]
        public IActionResult usp_DeleteOldStatus(string mobileno)
        {
            Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromMinutes(2));  
                _imessage.usp_DeleteOldStatus(mobileno);   
            });

            return Ok("Status will be deleted in 2 minutes");
        }


        [HttpGet ("ViewStoryGteByMobileNo/{loginmobileno}")]
        public IActionResult usptblstatusView(string loginmobileno)
        {
            List<StoryModel> liststory = new List<StoryModel>(); 
         var data=   _imessage.usptblstatusView(loginmobileno);
            while (data.Read())
            {
                liststory.Add(new StoryModel
                {
                    Story = data["status"].ToString()
                });
            }
            data.Close();
            return Ok(liststory);
        }



        [HttpGet ("usptblstatusViewAllStory")]
        public IActionResult usptblstatusViewAllStory()
        {
            List<StoryModel> liststory = new List<StoryModel>();
            var data = _imessage.usptblstatusViewAllStory();
            while (data.Read())
            {
                liststory.Add(new StoryModel
                {
                    mobileno = data["MobileNo"].ToString(),
                    Story = data["Stories"].ToString()
                });
            }
            data.Close();
            return Ok(liststory);
        }





        [HttpPost("uspTBL_UserProfileInsertUpdate")]
        public async Task<IActionResult> uspTBL_UserProfileInsertUpdate([FromForm] UserPorfileModel input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string fileName = null;

            try
            {
                if (input.file != null && input.file.Length > 0)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(input.file.FileName);
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await input.file.CopyToAsync(stream);
                    }
                }

                UserPorfileModel model = new UserPorfileModel
                {
                    sno = input.sno,
                    Name = input.Name,
                    MobileNo = input.MobileNo,
                    About = input.About,
                    StatusLine = input.StatusLine,
                    Image = fileName, // Save only file name
                    Flag = input.Flag
                };

                // 3. Save to DB (Mocked here)
                bool flag = _imessage.uspTBL_UserPorfileInsertUpdate(model); // Replace with real DB call

                if (flag)
                {
                    return Ok(new { message = "Profile saved successfully", sno = model.sno, image = fileName });
                }
                else
                {
                    return BadRequest(new { message = "Failed to save profile" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error", error = ex.Message });
            }
        }




        [HttpGet("uspTBL_UserPorfileViewDataMobileNoWise/{mobileno}")]
        public IActionResult uspTBL_UserPorfileViewDataMobileNoWise(string mobileno)
        {
            List<UserPorfileModel> list = new List<UserPorfileModel>();
            var data = _imessage.uspTBL_UserPorfileViewDataMobileNoWise(mobileno);
            if (data.Read())
            {
                list.Add(new UserPorfileModel
                {
                    sno = Convert.ToInt64(data["sno"]),
                    Name = data["Name"].ToString(),
                    About = data["about"].ToString(),
                    StatusLine = data["statusline"].ToString(),
                    Image = ImagePath + data["Images"].ToString()
                });


            }
            data.Close();
            return Ok(list);    
        }
    }


    public class MessageRequestModel
    {
        public string MsgSenderMobileNumber { get; set; }
        public string MsgReciveMobileNumber { get; set; }
    }


    public class StoryModel
    {
        public string mobileno { get; set; }
        public string Story{ get; set; }

    }
}
