using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothSliderHealth : MonoBehaviour
{
    [SerializeField] private Health _hitpoints;
    [SerializeField] private Slider _smoothSlider;
    [SerializeField] private float _duration;

    private float _targetValue;
    private Coroutine _currentCoroutine;

    private void OnEnable()
    {
        _hitpoints.HealthChanged += UpdateUI;
    }

    private void OnDisable()
    {
        _hitpoints.HealthChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        _targetValue = (_hitpoints.CurrentHitPoints / (float)_hitpoints.MaxHitPoints) * _smoothSlider.maxValue;

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(ChangeSliderValue(_smoothSlider.value, _targetValue));
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

        _currentCoroutine = null;
    }
}
