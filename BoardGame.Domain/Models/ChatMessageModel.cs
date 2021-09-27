﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Domain.Models
{
    public class ChatMessageModel
    {
        public int ChatMessageId { get; set; }
        public int SessionId { get; set; }
        public int AppUserId { get; set; }
        public string Message { get; set; }
        public string Sender { get; set; }
    }
}
