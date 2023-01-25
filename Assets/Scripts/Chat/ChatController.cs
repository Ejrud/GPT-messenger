using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace OpenAI
{
    public class ChatController : MonoBehaviour
    {
        // Links
        private ChatSettings _chatSettings;
        private ChatBuilder _chatBuilder;
        private ChatHistory _chatHistory;

        [Header("UI")]
        [SerializeField] private TMP_InputField _userInputField;
        [SerializeField] private Button _sendBtn;

        private OpenAIApi openai = new OpenAIApi();
        private string _aiInput;
        private string _userInput;

        private void Start()
        {
            _sendBtn.onClick.AddListener(PrepareMessage);
            _chatSettings = GetComponent<ChatSettings>();
            _chatBuilder = GetComponent<ChatBuilder>();
            _chatHistory = GetComponent<ChatHistory>();

            if (string.IsNullOrEmpty(_chatHistory.userData.instruction))
                _chatHistory.userData.instruction = "Act as a random stranger in a chat room and reply to the questions.\nQ: ";
        }

        private void PrepareMessage()
        {
            _userInput = _userInputField.text;
            _chatHistory.userData.instruction += $"{_userInput}\nA: ";
            
            _aiInput = "...";
            _userInputField.text = "";

            _sendBtn.enabled = false;
            _userInputField.enabled = false;

            AddMessage(_userInput, true);
            SendReply();
        }

        private async void SendReply()
        {
            // Complete the instruction
            var completionResponse = await openai.CreateCompletion(new CreateCompletionRequest()
            {
                Prompt = _chatHistory.userData.instruction,
                Model = "text-davinci-003",
                MaxTokens = _chatSettings.maxTokens
            });

            _aiInput = completionResponse.Choices[0].Text;
            _chatHistory.userData.instruction += $"{completionResponse.Choices[0].Text}\nQ: ";
            
            AddMessage(_aiInput, false);
            
            _sendBtn.enabled = true;
            _userInputField.enabled = true;
        }

        private void AddMessage(string msg, bool isUser)
        {
            MessageData data = new MessageData();
            data.text = msg;
            data.isUser = isUser;

            _chatBuilder.AddMessage(data);
        }
    }
}
