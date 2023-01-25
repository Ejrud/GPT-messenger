using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MessageFrame : MonoBehaviour
{
    [SerializeField] private int _charactersPerLine = 25;
    [SerializeField] private TMP_Text _message;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _image;


    [Header("Coloring")]
    [SerializeField] private Color _userColor;
    [SerializeField] private Color _openAIColor;

    public void SetMessage(string msg, bool isUser)
    {
        string inputString = msg;
        string outputString = "";;

        string[] words = inputString.Split(' ');
        int charactersCount = 0;

        for (int i = 0; i < words.Length; i++)
        {
            charactersCount += words[i].Length;
            
            if (charactersCount >= _charactersPerLine)
            {
                outputString += "<br>" + words[i] + " ";
                charactersCount = words[i].Length;
            }
            else
            {
                outputString += words[i] + " ";
            }
        }

        _rectTransform.localScale = new Vector3(1,1,1);
        _rectTransform.localPosition = new Vector3(0,0,0);

        if (isUser)
        {
            _rectTransform.pivot = new Vector2(1, 1);
            _image.color = _userColor;
        }
        else
        {
            _rectTransform.pivot = new Vector2(0, 1);
            _image.color = _openAIColor;
        }

        _message.text = outputString;
    }
}

/*
 Бенджамин Франклин был выдающимся американским Политиком, <br>изобретателем и писателем XVIII века. Он <br>был одним из старших участников <br>американской революции и первое <br>президентство Соединенных Штатов. Также <br>он был писателем и <br>изобретателем, знаменит своими цитатами и <br>утверждениями, а его учение охватывает <br>области физики, биологии, <br>психологии, морали, истории и другие. 
 */