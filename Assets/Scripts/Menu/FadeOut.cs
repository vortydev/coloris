using DG.Tweening;
using UnityEngine;

namespace Menu
{
    public class FadeOut : MonoBehaviour
    {
        [SerializeField] private float duration;
        [SerializeField] private float delay;
        
        private CanvasGroup _canvas;

        private void Awake()
        {
            _canvas = GetComponent<CanvasGroup>();
        }

        public void DoFade()
        {
            _canvas.DOFade(0, duration).SetDelay(delay);
        }
    }
}
