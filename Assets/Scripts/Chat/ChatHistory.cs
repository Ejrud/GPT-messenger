using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ChatHistory : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string _fileName;
    [SerializeField] private string _path;
    public UserData userData;
    
    public void AddMessage(MessageData msg)
    {
        userData.messages.Add(msg);

        if (!msg.isUser)
            SaveData();
    }

    public void SaveData()
    {
        BinarySerializer.Serialize(_path, userData);
    }

    public void DeleteData()
    {
        userData = new UserData();
        userData.messages = new List<MessageData>();
        userData.instruction = "Act as a random stranger in a chat room and reply to the questions.\nQ: ";
    }
    
    private void Awake()
    {
        _path = Path.Combine(Application.persistentDataPath, _fileName);
        
        if (File.Exists(_path))
            userData = BinarySerializer.Deserialize<UserData>(_path);
        else
        {
            userData = new UserData();
            userData.messages = new List<MessageData>();
            userData.instruction = "Act as a random stranger in a chat room and reply to the questions.\nQ: ";
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
