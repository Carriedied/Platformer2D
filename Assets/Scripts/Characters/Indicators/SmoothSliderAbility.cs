using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Characters.Indicators
{
    public class SmoothSliderAbility : MonoBehaviour
    {
        [SerializeField] private Slider _smoothSlider;

        private Coroutine _currentCoroutine;
        private float _minValueSlider = 0;

        public void StartAbilityUsage(float abilityDuration)
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            _currentCoroutine = StartCoroutine(ChangeSliderValue(_smoothSlider.maxValue, _minValueSlider, abilityDuration));
        }

        public void StartAbilityCooldown(float cooldownTime)
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            _currentCoroutine = StartCoroutine(ChangeSliderValue(_minValueSlider, _smoothSlider.maxValue, cooldownTime));
        }

        private IEnumerator ChangeSliderValue(float startValue, float targetValue, float duration)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                _smoothSlider.value = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            _smoothSlider.value = targetValue;
            _currentCoroutine = null;
        }
    }
}
