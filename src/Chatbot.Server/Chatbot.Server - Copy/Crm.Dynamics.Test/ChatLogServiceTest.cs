using Crm.Dynamics.Models;
using Crm.Dynamics.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Dynamics.Test
{
    [TestClass]
    public class ChatLogServiceTest
    {
        [TestMethod]
        public void _01_CreateChatLogs()
        {
            var session = CrmSession.CreateDefault();
            var service = new TokenService();
            var authResult = service.GetAccessToken(session);
            Assert.IsFalse(string.IsNullOrEmpty(authResult.AccessToken));

            var chatLog = new ChatLogModel
            {
                SessionId = "session id xxxx",
                Log = @"再多教它一些东西
                通过对话模板让技能模型学习用户意图的多种表达方式
                也可尽量多地告诉它用户的真实问句（对话样本），同时标出用户的意图和实现意图的关键信息
                对话模型就像个儿童，您教得越多，它越能领会您的意思，而且还能举一反三呢~
                这部分在【效果优化--训练数据】里完成",
                Tags = "100000000,100000001,100000002,100000003",
                //LeadIdOdataBind = createdLeadGuid
            };

            var chatLogService = new ChatLogService();
            chatLogService.CreateChatLog(chatLog, session);
            Assert.IsFalse(string.IsNullOrEmpty(chatLog.CreatedChatLogGuid));
        }
    }
}
