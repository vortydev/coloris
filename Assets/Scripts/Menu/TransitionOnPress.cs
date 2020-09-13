using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Menu
{
    public class TransitionOnPress : MonoBehaviour
    {
        [SerializeField] private RectTransform parent;
        [SerializeField] private RectTransform transitionObject;
        [SerializeField] private CanvasGroup menu;
        
        [SerializeField] private float padding = 50;

        [SerializeField] private float transitionTime = 0.75f;
        [SerializeField] private Ease transitionEase = Ease.OutQuint;

        [SerializeField] private UnityEvent OnBack;
        
        public void Transition(RectTransform obj)
        {
            transitionObject.position = obj.position;

            var leftEdge = parent.rect.xMin;
            var rightEdge = parent.rect.xMax;
            var topEdge = parent.rect.yMax;
            var bottomEdge = parent.rect.yMin;

            var distanceLeft = (obj.anchoredPosition.x + obj.rect.xMax ) - leftEdge;
            var distanceRight = (obj.anchoredPosition.x + obj.rect.xMin ) - rightEdge;
            var distanceTop = (obj.anchoredPosition.y + obj.rect.yMin ) - topEdge;
            var distanceBottom = (obj.anchoredPosition.y + obj.rect.yMax ) - bottomEdge;

            var maxSize = Mathf.Max(Mathf.Abs(distanceLeft), Mathf.Abs(distanceRight), Mathf.Abs(distanceTop), Mathf.Abs(distanceBottom)) * 2 + padding;
            
            transitionObject.DOSizeDelta(new Vector2(maxSize, maxSize), transitionTime).SetEase(transitionEase).OnComplete(
                () => { menu.blocksRaycasts = false; });
            EventSystem.current.SetSelectedGameObject(null);
        }

        public void Back()
        {
            transitionObject.DOSizeDelta(Vector2.zero, transitionTime).SetEase(transitionEase).OnComplete(
                () =>
                {
                    menu.blocksRaycasts = true; 
                    OnBack?.Invoke();
                });
        }
    }
}
