using DG.Tweening;
using UnityEngine;

namespace Menu
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] private Transform game;
        [SerializeField] private Transform pause;

        private float _xScale;

        private void Awake()
        {
            _xScale = game.localScale.x;
        }

        public void DoPause()
        {
            DOTween.Sequence()
                .Append(game.DOScaleX(0, 0.33f).SetUpdate(true))
                .Append(pause.DOScaleX(1, 0.33f).SetUpdate(true))
                .SetUpdate(true);
            
            Time.timeScale = 0;
        }

        public void DoUnpause()
        {
            DOTween.Sequence()
                .Append(pause.DOScaleX(0, 0.33f).SetUpdate(true))
                .Append(game.DOScaleX(_xScale, 0.33f).SetUpdate(true))
                .SetUpdate(true)
                .OnComplete(() => { Time.timeScale = 1; });
        }
    }
}
