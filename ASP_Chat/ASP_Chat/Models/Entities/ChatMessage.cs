namespace ASP_Chat.Models.Entities
{
    public class ChatMessage
    {
        public ChatUser ChatUser { get; set; }
        public string Content { get; set; }

        public void Send()
        {

        }

    }
}