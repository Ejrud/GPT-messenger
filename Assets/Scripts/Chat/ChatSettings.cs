using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ChatSettings : MonoBehaviour
{
    public int maxTokens = 2000;


    [Header("UI")]
    [SerializeField] private Button _exitBtn;
    [SerializeField] private InputField _tokenInput;
    [SerializeField] private GameObject _settingWindowObj;

    
    private void Start()
    {
        _exitBtn.onClick.AddListener(Exit);
    }

    private void Exit()
    {
        Application.Quit();
    }

}