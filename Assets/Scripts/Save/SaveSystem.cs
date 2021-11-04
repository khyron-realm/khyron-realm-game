using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Save
{

    // System for saving and loading to BinaryFormat
    // It is to much repetitive code --> must me redone
    public static class SaveSystem
    {
        // ## Player data ##
        public static void SavePlayerData(PlayerValues values)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/playerCredentials.data";

            FileStream stream = new FileStream(path, FileMode.Create);

            PlayerData data = new PlayerData(values);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static PlayerData LoadPlayerData()
        {
            string path = Application.persistentDataPath + "/playerCredentials.data";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                PlayerData data = formatter.Deserialize(stream) as PlayerData;
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