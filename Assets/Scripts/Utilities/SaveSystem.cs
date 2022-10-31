using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayer(Player player)
    {
        PlayerData playerData = new PlayerData(player);
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(GetDataPath(), FileMode.Create);

        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(GetDataPath()))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(GetDataPath(), FileMode.Open);
            PlayerData playerData =  formatter.Deserialize(stream) as PlayerData;

            stream.Close();

            return playerData;
        }
        else
        {
            return null;
        }
    }

    private static string GetDataPath()
    {
        return Application.persistentDataPath + "/player.data";
    }
}