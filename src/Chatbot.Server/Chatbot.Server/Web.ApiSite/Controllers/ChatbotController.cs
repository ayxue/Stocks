using Chatbot.Unit.Model;
using Chatbot.Unit.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Web.ApiSite.Controllers
{
    /// <summary>
    /// Chatbot controller
    /// </summary>
    [Route("chatbots")]
    public class ChatbotController : ApiController
    {
        private BaiduSession _baiduSession;
        private ChatService _chatService;
        private RobotService _robotService;

        //public ChatbotController()
        //{         
        //    _baiduSession = HttpContext.Current.Application["BaiduSession"] as BaiduSession;
        //    _chatService = new ChatService();
        //    _robotService = new RobotService();
        //}

        public ChatbotController(BaiduSession session, ChatService chatService, RobotService robotService)
        {
            _baiduSession = session;
            _chatService = chatService;
            _robotService = robotService;
        }

        /// <summary>
        /// Get all the chatbots
        /// </summary>
        /// <returns></returns>
        public Robot[] Get()
        {
            return _robotService.GetRobots();
        }

        /// <summary>
        /// Chat with the selected chatbot
        /// </summary>
        /// <param name="text">user input</param>
        /// <param name="botId">chatbot id</param>
        /// <returns>chat result</returns>
        [HttpGet, Route("{botId}/chat/{text}")]

        public ChatResult Chat(string text, string botId)
        {
            if (string.IsNullOrEmpty(botId))
                botId = _robotService.GetRobots().First().Id;

            return _chatService.Answer(botId, text, _baiduSession);
        }

        /// <summary>
        /// Chat with the default chatbot
        /// </summary>
        /// <param name="text">user input</param>
        /// <returns>chat result</returns>
        [HttpGet, Route("chat/{text}")]

        public ChatResult Chat(string text)
        {
            return Chat(text, null);
        }


        [HttpGet, Route("test")]
        // GET api/<controller>/5
        public string Test(int id)
        {
            return "Service is running " + id;
        }
    }
}