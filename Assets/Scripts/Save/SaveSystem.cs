using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Save
{

    // System for saving and loading to BinaryFormat
    public static class SaveSystem
    {
        public static void SaveMineData(MineValues mineData, GameObject temp)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/mines" + temp.name + ".data";

            FileStream stream = new FileStream(path, FileMode.Create);

            MineData data = new MineData(mineData);

            formatter.Serialize(stream, data);
            stream.Close();
        }


        public static MineData LoadMineData(GameObject temp)
        {
            string path = Application.persistentDataPath + "/mines" + temp.name + ".data";
            if(File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                MineData data = formatter.Deserialize(stream) as MineData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.Log("No File Found");
                return null;
            }
        }
    }
}