using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothSliderHealth : MonoBehaviour
{
    [SerializeField] private Health _hitpoints;
    [SerializeField] private Slider _smoothSlider;
    [SerializeField] private float _duration;

    private float _targetValue;

    protected virtual void OnEnable()
    {
        _hitpoints.OnHealthChanged += UpdateUI;
    }

    protected virtual void OnDisable()
    {
        _hitpoints.OnHealthChanged += UpdateUI;
    }

    private void Update()
    {
        _smoothSlider.transform.rotation = Quaternion.identity;
    }

    private void UpdateUI()
    {
        _targetValue = (_hitpoints.CurrentHitPoints / (float)_hitpoints.MaxHitPoints) * _smoothSlider.maxValue;

        StartCoroutine(ChangeSliderValue(_smoothSlider.value, _targetValue));
    }

    private IEnumerator ChangeSliderValue(float startValue, float targetValue)
    {
        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            _smoothSlider.value = Mathf.Lerp(startValue, targetValue, elapsedTime / _duration);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _smoothSlider.value = targetValue;
    }
}
