using Chatbot.Unit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Unit.Services
{
    public class RobotService
    {
        public Robot[] GetRobots()
        {
            return new Robot[]
            {
                new Robot
                {
                    Name = "盛小宝智能问答",
                    Id = "S20981"
                }
            };
        }

    }
}
