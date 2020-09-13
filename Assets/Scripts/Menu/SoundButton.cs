using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu
{
    public class SoundButton : Button
    {
        [SerializeField] private AudioClip sound;
        
        private static AudioSource _source;
        
        protected override void Awake()
        {
            base.Awake();
            _source = GetComponentInParent<AudioSource>();
        }

        public override void OnSubmit(BaseEventData eventData)
        {
            _source.PlayOneShot(sound);
            base.OnSubmit(eventData);
        }
    }
}
