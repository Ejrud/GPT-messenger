using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationCurve _fadeBack;
    [SerializeField] private AnimationCurve _fadeText;

    [SerializeField] private Image _logoBack;
    [SerializeField] private TMP_Text _logoText;

    private void Start()
    {
        StartCoroutine(FadeLogo());
    }

    public void SetActiveSettings(bool open)
    {
        _animator.SetBool("Active", open);
    }

    private IEnumerator FadeLogo()
    {
        _logoBack.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        float maxTime = 1.5f;
        float timer = 0f;
        Vector4 color = _logoBack.color;
        
        while (timer < maxTime)
        {
            float alphaImage = _fadeBack.Evaluate(timer / maxTime);
            float alphaText = _fadeText.Evaluate(timer / maxTime);

            _logoBack.color = new Vector4(color.x, color.y, color.z, alphaImage);
            _logoText.color = new Vector4(1, 1, 1, alphaText);
            Debug.Log(alphaText);
            
            timer += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        _logoBack.gameObject.SetActive(false);

        yield return null;
    }
}
