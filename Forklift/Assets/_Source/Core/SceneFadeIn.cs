using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Core
{
    public class SceneFadeIn : MonoBehaviour
    {
        [SerializeField] private Image _fadeImage;
        [SerializeField] private float _fadeDuration = 2f;

        private void Awake()
        {
            _fadeImage.DOFade(0f, _fadeDuration);
        }
    }
}