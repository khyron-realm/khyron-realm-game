namespace Networking.Chat
{
    public class ChatMessage
    {
        public string Sender { get; }
        public string Content { get; }
        public byte MessageType { get; }
        public string ChannelName { get; }
        public bool IsSender { get; }
        public bool IsServerMessage { get; }
        
        public ChatMessage(string sender, string content, byte messageType, string channelName, bool isSender = false, bool isServerMessage = false)
        {
            Sender = sender;
            Content = content;
            MessageType = messageType;
            ChannelName = channelName;
            IsSender = isSender;
            IsServerMessage = isServerMessage;
        }   
    }
}