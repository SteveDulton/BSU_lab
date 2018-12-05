using System.Collections.Generic;

namespace ASP_Chat.Models.Entities
{
    public class Conversation
    {
        public List<ChatUser> UsersInvolved { get; set; }
        public List<ChatMessage> Messages { get; set; }

        public Conversation(List<ChatUser> users)
        {

        }
    }
}