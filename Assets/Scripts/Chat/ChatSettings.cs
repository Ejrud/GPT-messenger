using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ChatSettings : MonoBehaviour
{
    public int maxTokens = 2000;
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationCurve _animCurve;

    [Header("UI")]
    [SerializeField] private Button _exitBtn;
    [SerializeField] private InputField _tokenInput;
    [SerializeField] private GameObject _settingWindowObj;
    [SerializeField] private Image _logoBg;
    [SerializeField] private TMP_Text _logo;

    public void SetActiveSettings(bool open)
    {
        _animator.SetBool("Active", open);
    }
    
    private void Start()
    {
        _exitBtn.onClick.AddListener(Exit);
        StartCoroutine(FadeLogo());
    }

    private void Exit()
    {
        Application.Quit();
    }

    private IEnumerator FadeLogo()
    {
        _logoBg.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        float timer = 2f;
        float maxValue = timer;
        Vector4 color = _logoBg.color;
        
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            float alpha = _animCurve.Evaluate(timer / maxValue);
            _logoBg.color = new Vector4(color.x, color.y, color.z, alpha);
            _logo.color = new Vector4(1, 1, 1, alpha);
            yield return new WaitForEndOfFrame();
        }

        _logoBg.gameObject.SetActive(false);

        yield return null;
    }
}