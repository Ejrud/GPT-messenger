using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;


public class HoldButton : MonoBehaviour, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Settings")]
    [SerializeField] private Slider _slider;
    [SerializeField] private float _timeToActivate = 3f;
    [SerializeField] private UnityEvent _onClick = new UnityEvent();

    private bool _isClicked;
    private float _timer;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isClicked = true;
        _slider.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!_isClicked) return;

        _timer += Time.deltaTime;
        _slider.value = _timer / _timeToActivate;

        if (_timer > _timeToActivate)
        {
            _isClicked = false;
            _slider.gameObject.SetActive(false);
            _onClick.Invoke();
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        UnsetButton();
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        UnsetButton();
    }

    private void UnsetButton()
    {
        _slider.gameObject.SetActive(false);
        _isClicked = false;
        _timer = 0;
    } 
}
