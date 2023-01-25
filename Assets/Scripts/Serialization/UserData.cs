using System.Collections.Generic;

[System.Serializable]
public struct UserData
{
    public List<MessageData> messages;
    public string instruction;
}
