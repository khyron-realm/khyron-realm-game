namespace Networking.Chat
{
    public class Filter
    {
        public byte MessageType { get; }
        public string ChannelName { get; }
        
        public Filter(byte messageType, string channelName)
        {
            MessageType = messageType;
            ChannelName = channelName;
        }   
    }
}