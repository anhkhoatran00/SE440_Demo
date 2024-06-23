using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HP : MonoBehaviour
{
    private Slider _slider;

    [SerializeField, Range(0, 100)]
    private int _currentHP;
    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnValidate()
    {
        if (!Application.isPlaying) return;

        HpChangeTo(_currentHP);
    }

    void HpChangeTo(int to)
    {
        var filder = _slider.fillRect.GetComponent<Image>();
        // _slider.value = 50;
        _slider.DOValue(to, 1f)
            .SetEase(Ease.InCirc)
            .OnUpdate(() =>
            {
                if (_slider.value < 30)
                {
                    filder.DOColor(Color.red, 0.5f);
                }
                else if (_slider.value < 70)
                {
                    filder.DOColor(Color.yellow, 0.5f);
                }
                else
                {
                    filder.DOColor(Color.green, .5f);
                }
            });
    }
}
