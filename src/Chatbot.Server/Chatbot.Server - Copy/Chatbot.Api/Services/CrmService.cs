using Crm.Dynamics.Models;
using Crm.Dynamics.Services;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatbot.Api.Services
{
    public class CrmService
    {
        private TokenService _tokenService;
        private ChatLogService _chatLogService;
        private LeadService _leadService;
        private CrmSession _session;

        public CrmService(TokenService tokenService, LeadService leadService, ChatLogService chatlogService, CrmSession session)
        {
            _tokenService = tokenService;
            _chatLogService = chatlogService;
            _leadService = leadService;
            _session = session;
            _session.AuthenticationResult = Connect();
        }


        public AuthenticationResult Connect()
        {
            return _tokenService.GetAccessToken(_session);
        }


        public LeadModel[] GetLeads()
        {
            return _leadService.GetLeads(_session);
        }

        public LeadModel GetLead(string leadId)
        {
            return _leadService.GetLeads(_session).FirstOrDefault(l => l.LeadId == leadId);
        }

        public void CreateLead(LeadModel lead)
        {
            var leadService = new LeadService();
            leadService.CreateLead(lead, _session);
        }

        public ChatLogModel SaveChatlog(string sessionId, string log, string[] tags, string leadId = null)
        {
            var chatLog = new ChatLogModel
            {
                SessionId = sessionId,
                Log = log,
                Tags = GetTagKeys(tags),
                LeadIdOdataBind = string.IsNullOrEmpty(leadId) ? null: string.Format($"leads({leadId})")
            };

            _session.AuthenticationResult = Connect();
            var chatLogService = new ChatLogService();
            chatLogService.CreateChatLog(chatLog, _session);
            return chatLog;
        }

        private string GetTagKeys(string[] tags)
        {
            if (!tags.Any())
                return null;

            return string.Join(",", tags.Select(tag => {
                string tagKey;
                if (!ChatLogModel.ChatLogTags.TryGetValue(tag, out tagKey))
                    tagKey = ChatLogModel.ChatLogTags["其他"];
                return tagKey;
            }));
        }

    }
}