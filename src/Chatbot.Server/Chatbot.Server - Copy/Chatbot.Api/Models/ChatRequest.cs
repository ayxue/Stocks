using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chatbot.Api.Models
{
    public class ChatRequest
    {
        public string ChatBotId { get; set; }

        [Required]
        public string Text { get; set; }

        public string LogId { get; set; }

        public string SessionId { get; set; }
    }
}