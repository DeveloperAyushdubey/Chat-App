using System;
using System.Collections.Generic;

namespace projectApi.Models;

public partial class Message
{
    public int? Id { get; set; }

    public string? MsgSenderMobileNumber { get; set; } = null!;

    public string? MsgReciveMobileNumber { get; set; } = null!;
    public DateTime? Msgsendtime { get; set; }

    public string? flag { get; set; }

    public long? MsgCount { get; set; }
    public string? Image { get; set; }
    public string? SenderName{ get; set; }

    public string? MsgDesc { get; set; } = null!;
}
