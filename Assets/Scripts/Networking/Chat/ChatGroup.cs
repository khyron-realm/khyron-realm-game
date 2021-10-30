using System.Collections.Generic;
using System.Linq;
using DarkRift;

namespace Networking.Chat
{
    /// <summary>
    ///     A chat group of players
    /// </summary>
    public class ChatGroup : IDarkRiftSerializable
    {
        public string Name { get; set; }
        public List<string> Users { get; set; }

        public ChatGroup(string name)
        {
            Name = name;
        }

        public void Deserialize(DeserializeEvent e)
        {
            Name = e.Reader.ReadString();
            Users = e.Reader.ReadStrings().ToList();
        }

        public void Serialize(SerializeEvent e)
        {
        }
    }
}