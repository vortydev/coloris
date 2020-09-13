using System;
using UnityEngine;

namespace ChiefJuice
{
    public class AudioManager : MonoBehaviour
    {
        public static event Action<float> UpdateAudio;
        
        [SerializeField] private AudioTrack track;
        
        private float _currentTime;
        
        private void Update()
        {
            if (_currentTime < track.LeadInTime)
            {
                _currentTime += Time.deltaTime;
                return;
            }
            
            UpdateAudio?.Invoke(Mathf.Cos(((Time.time * Mathf.PI) * (track.BPM / 60f) + (track.Offset / 60)) % Mathf.PI));
        }
    }
}
