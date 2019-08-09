using Chatbot.Unit.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatbot.Api.Models
{
    public class SessionEntry
    {
        public SessionEntry()
        {
            Chats = new ConcurrentBag<Tuple<string, DateTime>>();
            Tags = new ConcurrentDictionary<string, string>();
        }

        public string SessionId { get; set; }

        public string LeadId { get; set; }

        public ConcurrentBag<Tuple<string,DateTime>> Chats { get; protected set; }

        public ConcurrentDictionary<string, string> Tags { get; protected set; }

        public DateTime BeginTime { get; set; }

        public DateTime? EndTime { get; set; }

    }


    public class ChatLogCache
    {
        private readonly ConcurrentDictionary<string, SessionEntry> _cache = new ConcurrentDictionary<string, SessionEntry>();

        public ConcurrentDictionary<string, SessionEntry> Cache
        {
            get
            {
                return _cache;
            }
        }


        public SessionEntry GetSessionEntry(string sessionId)
        {
            SessionEntry entry;
            _cache.TryGetValue(sessionId, out entry);
            return entry;
        }

        public SessionEntry Create(string sessionId, string leadId)
        {
            var entry = new SessionEntry
            {
                SessionId = sessionId,
                LeadId = leadId,
                BeginTime = DateTime.Now
            };

            _cache[sessionId] = entry;
            return entry;
        }

        public void Remove(string sessionId)
        {
            SessionEntry entry;
            _cache.TryRemove(sessionId, out entry);
        }


        public void LogChat(string sessionId, string text, string leadId)
        {
            var entry = GetSessionEntry(sessionId);
            if (entry == null)
                entry = this.Create(sessionId, leadId);

            entry.Chats.Add(new Tuple<string, DateTime>(text, DateTime.Now));
        }

        public void LogChat(string sessionId, ChatResult result, string leadId, string by)
        {
            var entry = GetSessionEntry(sessionId);
            if (entry == null)
                entry = this.Create(sessionId, leadId);

            var action = result.Actions.First();
            foreach (var tag in action.Tags)
                entry.Tags[tag] = tag;

            var logText = $"- {by}: {action.Say}";
            entry.Chats.Add(new Tuple<string, DateTime>(logText, DateTime.Now));
        }
    }

}