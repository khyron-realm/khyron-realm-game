using System.Collections.Generic;
using System.Linq;
using DarkRift;
using DarkRift.Server;

namespace Networking.Auctions
{
    /// <summary>
    ///     
    /// </summary>
    public class AuctionRoom : IDarkRiftSerializable
    {
        public ushort Id { get; set; }
        public string Name { get; set; }
        public byte MinPlayers { get; set; }
        public byte MaxPlayers { get; set; }
        public bool HasStarted { get; set; }
        public bool IsVisible { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        
        public List<IClient> Clients = new List<IClient>();
        public List<Player> PlayerList = new List<Player>();
        
        public AuctionRoom(ushort id, string name, bool hasStarted, bool isVisible, long startTime, long endTime)
        {
            Id = id;
            Name = name;
            HasStarted = hasStarted;
            IsVisible = isVisible;
            StartTime = startTime;
            EndTime = endTime;
        }

        public void Deserialize(DeserializeEvent e)
        {
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
            e.Writer.Write(Name);
            e.Writer.Write(HasStarted);
            e.Writer.Write(IsVisible);
            e.Writer.Write(StartTime);
            e.Writer.Write(EndTime);
        }

        internal bool AddPlayer(Player player, IClient client)
        {
            if (PlayerList.Count >= MaxPlayers || HasStarted)
                return false;
            
            PlayerList.Add(player);
            Clients.Add(client);
            return true;
        }

        internal bool RemovePlayer(IClient client)
        {
            if (PlayerList.All(p => p.Id != client.ID) && !Clients.Contains(client))
                return false;

            PlayerList.Remove(PlayerList.Find(p => p.Id == client.ID));
            Clients.Remove(client);
            return true;
        }
    }
}