using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChatBuilder : MonoBehaviour
{
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private ChatHistory _chatHistory;
    [SerializeField] private GameObject _framePrefab;
    [SerializeField] private List<GameObject> msgObjs = new List<GameObject>();

    private void Start()
    {
        SetUpHistory();
    }

    public void AddMessage(MessageData msg)
    {
        _chatHistory.AddMessage(msg);
        InstantiateMessage(msg);
    }

    public void SetUpHistory()
    {
        prepareLayoutGroup();
        void prepareLayoutGroup()
        {
            GameObject initObj = Instantiate(_framePrefab);
            initObj.transform.SetParent(_parentTransform);
            initObj.transform.localScale = new Vector3(1,1,1);
            initObj.transform.name = "plug message";
            Image plugImage = initObj.GetComponent<Image>();
            plugImage.color = new Vector4(0,0,0,0);
        }

        foreach (MessageData msg in _chatHistory.userData.messages)
        {
            InstantiateMessage(msg);
        }
    }

    public void DeleteHistory()
    {
        foreach(GameObject obj in msgObjs)
        {
            Destroy(obj);
        }

        msgObjs = new List<GameObject>();
    }

    private void InstantiateMessage(MessageData msg)
    {
        GameObject msgObj = Instantiate(_framePrefab);
        msgObj.transform.SetParent(_parentTransform);
        msgObj.GetComponent<MessageFrame>().SetMessage(msg.text, msg.isUser);
        msgObjs.Add(msgObj);
    }
}
