using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO; 

public static class SaveLoad 
{
   public static void SaveData(PlayerData PlayerSaveData)
   {
    Debug.Log(Application.persistentDataPath); 

    BinaryFormatter Bformatter = new BinaryFormatter();
    string path = Application.persistentDataPath + "/GameSaveData.save";

    FileStream fstream = new FileStream(path ,FileMode.Create);
    PlayerData Pxdata = new PlayerData(PlayerSaveData);
    Bformatter.Serialize(fstream, Pxdata);
    fstream.Close();
    Debug.Log("Kaydedildi.");
   }
   public static PlayerData LoadData()
   {
       string path = Application.persistentDataPath + "/GameSaveData.save";

       if(File.Exists(path))
       {
        BinaryFormatter Bformatter = new BinaryFormatter();
        FileStream fstream = new FileStream(path ,FileMode.Open);
        PlayerData Pxdata = Bformatter.Deserialize(fstream) as PlayerData;
        fstream.Close();
        Debug.Log("Başarılı.");
        return Pxdata;  
       }
       else
       {
           Debug.LogError("Kaydedilen Oyun Bulunamadı!"+path);
          return null; 
       }     
   }
}
