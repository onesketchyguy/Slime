using UnityEngine;

namespace Utility.Audio
{
    public class PlayClip : MonoBehaviour
    {
        /// <summary>
        /// Clip played by source
        /// </summary>
        public AudioClip clipToPlay;

        /// <summary>
        /// Wether or not to play this clip on start.
        /// </summary>
        public bool PlayOnStart = true;

        private void OnEnable()
        {
            if (PlayOnStart) Play();
        }

        /// <summary>
        /// Activates Source
        /// </summary>
        public void Play()
        {
            AudioManager.PlayClip(clipToPlay, transform.position, AudioManager.GetClipVolume(transform.position));
        }
    }
}