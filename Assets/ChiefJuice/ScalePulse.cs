using UnityEngine;

namespace ChiefJuice
{
    public class ScalePulse : MonoBehaviour
    {
        
        [SerializeField] private Vector3 min, max;

        private static float _currentTime;
        
        private void OnEnable()
        {
            AudioManager.UpdateAudio += AudioManagerOnUpdateAudio;
        }

        private void OnDisable()
        {
            AudioManager.UpdateAudio -= AudioManagerOnUpdateAudio;
        }

        private void AudioManagerOnUpdateAudio(float val)
        {
            var target = Vector3.Lerp(min, max, val);
            transform.localScale = target;
        }
    }
}
