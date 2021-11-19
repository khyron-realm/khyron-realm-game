using System;
using DarkRift;

namespace Networking.Headquarters
{
    /// <summary>
    ///     Background task for player data
    /// </summary>
    public class BackgroundTask : IDarkRiftSerializable
    {
        public long Time { get; set; }
        public byte Type { get; set; }
        public uint ValueId { get; set; }
        public string ValueDescription { get; set; }

        public BackgroundTask()
        { }

        /// <summary>
        ///     Deserialization method for build task
        /// </summary>
        /// <param name="e">Deserialize event</param>
        public void Deserialize(DeserializeEvent e)
        {
            Time = e.Reader.ReadInt64();
            Type = e.Reader.ReadByte();
            ValueId = e.Reader.ReadUInt32();
            ValueDescription = e.Reader.ReadString();
        }
        
        /// <summary>
        ///     Serialization method for build task
        /// </summary>
        /// <param name="e">Serialize event</param>
        public void Serialize(SerializeEvent e)
        { }
    }
}