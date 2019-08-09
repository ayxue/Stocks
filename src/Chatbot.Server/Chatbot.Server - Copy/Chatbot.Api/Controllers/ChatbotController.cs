using Chatbot.Api.Models;
using Chatbot.Api.Services;
using Chatbot.Unit.Model;
using Chatbot.Unit.Services;
using Crm.Dynamics.Models;
using Crm.Dynamics.Services;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Chatbot.Api.Controllers
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
        private ChatLogCache _sessionCache;
        private ChatLogService _chatLogService;
        private TokenService _tokenService;
        private CrmService _crmService;

        public ChatbotController(BaiduSession session, ChatService chatService, RobotService robotService, CrmService crmService, ChatLogCache sessionCache)
        {
            _baiduSession = session;
            _chatService = chatService;
            _robotService = robotService;
            _sessionCache = sessionCache;
            _crmService = crmService;
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
        /// Get my lead info in CRM
        /// </summary>
        /// <param name="leadId"></param>
        /// <returns></returns>
        [HttpGet, Route("me")]
        public LeadModel Me()
        {
            var leadId = GetLeadId();
            return _crmService.GetLead(leadId);
        }      

        /// <summary>
        /// Get all the leads
        /// </summary>
        /// <param name="leadId"></param>
        [HttpGet, Route("leads")]
        public LeadModel[] Leads()
        {
            return _crmService.GetLeads();
        }

        /// <summary>
        /// Create a lead
        /// </summary>
        /// <param name="leadId"></param>
        [HttpPost, Route("leadme")]
        public void LeadMe([FromBody] LeadModel lead)
        {
            _crmService.CreateLead(lead);

            return;
        }

        /// <summary>
        /// Chat with the selected chatbot
        /// </summary>
        /// <param name="text">user input</param>
        /// <param name="botId">chatbot id</param>
        /// <returns>chat result</returns>
        [HttpGet, Route("chatbot/{botId}/chat/{text}")]

        public ChatResult Chat(string text, string botId)
        {
            var leadId = GetLeadId();
            if (string.IsNullOrEmpty(botId))
                botId = _robotService.GetRobots().First().Id;

            LogChat(text, "客户");
            var ret = _chatService.Answer(botId, text, _baiduSession);

            if (ret.Actions.Any())
                LogChat(ret, "盛小宝");

            return ret;
        }

        /// <summary>
        /// Chat with the default chatbot
        /// </summary>
        /// <param name="text">user input</param>
        /// <returns>chat result</returns>
        [HttpGet, Route("chat/{text}")]

        public ChatResult Chat(string text)
        {
            return Chat(text, string.Empty);
        }

        /// <summary>
        /// Chat with different robots
        /// </summary>
        /// <param name="chat"></param>
        /// <returns></returns>
        [HttpPost, Route("chat")]

        public ChatResult Chat([FromBody]ChatRequest chat)
        {
            return Chat(chat.Text, chat.ChatBotId);
        }

        /// <summary>
        /// Chat with robot and get the first answer in plain text
        /// </summary>
        /// <param name="chat"></param>
        /// <returns></returns>
        [HttpPost, Route("chatquick")]

        public string ChatQuick([FromBody]ChatRequest chat)
        {
            var result = Chat(chat.Text, chat.ChatBotId);
            var text = result.Actions.First().Say;
            var doc = new HtmlDocument();
            doc.OptionFixNestedTags = true;
            doc.LoadHtml(text);
            return doc.DocumentNode.InnerText;
        }


        /// <summary>
        /// Save the conversation when the client quits the chat
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("quit")]
        public ChatLogModel Quit()
        {
            var leadId = GetLeadId();
            var sessionCache = _sessionCache.GetSessionEntry(leadId);
            if (sessionCache == null)
                return null;

            var log = string.Join("\n", sessionCache.Chats.OrderBy(c => c.Item2).Select(c => c.Item1));
            var tags = sessionCache.Tags.Keys.ToArray();

            var chatlog = _crmService.SaveChatlog("chatbot_" + leadId, log, tags, sessionCache.LeadId);
            _sessionCache.Remove(leadId);

            return chatlog;
        }



        /// <summary>
        /// Return chats in this conversation 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("conversation")]
        public ChatLogCache Conversation()
        {
            return _sessionCache;
        }

        /// <summary>
        /// Service Availability
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("isalive")]
        public object IsAlive()
        {
            return new { IsAlive = true };
        }

        private void LogChat(string text, string by)
        {
            var leadId = GetLeadId();
            var logText = $"- {by}: {text}";
            _sessionCache.LogChat(leadId, logText, leadId);
        }

        private void LogChat(ChatResult result, string by)
        {
            var leadId = GetLeadId();
            _sessionCache.LogChat(leadId, result, leadId, by);
        }

        private string GetLeadId()
        {
            var leadId = HttpContext.Current.Request.Headers["leadId"];
            if (string.IsNullOrEmpty(leadId))
                return "fd2b1bd5-c2b8-e911-a9a2-00224800c5e8";

            return leadId;

        }

    }
}