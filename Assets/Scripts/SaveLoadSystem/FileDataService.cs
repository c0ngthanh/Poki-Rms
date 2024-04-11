using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataService : IDataService
{
    ISerializer serializer;
    string dataPath;
    string fileExtension;
    public FileDataService(ISerializer serializer){
        this.serializer = serializer;
        dataPath = Application.persistentDataPath;
        fileExtension = "json";
    }
    string GetPathToFile(string fileName){
        return Path.Combine(dataPath, string.Concat(fileName,".",fileExtension));
    }
    public void Save(GameData data, bool overwrite = true)
    {
        string fileLocation = GetPathToFile(data.Name);
        if(!overwrite && File.Exists(fileLocation)){
            throw new IOException($"The file  '{data.Name}.{fileExtension}' +  already exists and cannot be overwritten.");
        }
        Debug.Log(fileLocation);
        File.WriteAllText(fileLocation, serializer.Serialize(data));
    }
    public GameData Load(string name)
    {
        string fileLocation = GetPathToFile(name);
        if(!File.Exists(fileLocation)){
            throw new IOException($"The file  '{name}.{fileExtension}' +  not exists.");
        }
        return serializer.Deserialize<GameData>(File.ReadAllText(fileLocation));
    }
    public void Delete(string name)
    {
        string fileLocation = GetPathToFile(name);
        if(File.Exists(fileLocation)){
            File.Delete(fileLocation);
        }
    }

    public void DeleteAll()
    {
        Debug.Log("Deleting all");
    }

    public IEnumerable<string> ListSaves()
    {
        foreach (string path in Directory.EnumerateFiles(dataPath))
        {
            if(Path.GetExtension(path) == fileExtension){
                yield return Path.GetFileNameWithoutExtension(path);
            }
        }
    }


}
