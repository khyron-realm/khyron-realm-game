using DarkRift;

namespace Networking.Mine
{
    /// <summary>
    ///     Mine seed values
    /// </summary>
    public class MineGenerationValues : IDarkRiftSerializable
    {
        public ResourcesData Global { get; set; }
        public ResourcesData Silicon { get; set; }
        public ResourcesData Lithium { get; set; }
        public ResourcesData Titanium { get; set; }
        
        public MineGenerationValues()
        {
            Global = new ResourcesData();
            Silicon = new ResourcesData();
            Lithium = new ResourcesData();
            Titanium = new ResourcesData();
        }

        public void Deserialize(DeserializeEvent e)
        {
            Global = e.Reader.ReadSerializable<ResourcesData>();
            Silicon = e.Reader.ReadSerializable<ResourcesData>();
            Lithium = e.Reader.ReadSerializable<ResourcesData>();
            Titanium = e.Reader.ReadSerializable<ResourcesData>();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Global);
            e.Writer.Write(Silicon);
            e.Writer.Write(Lithium);
            e.Writer.Write(Titanium);
        }
    }
}