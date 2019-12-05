﻿using UnityEngine;

namespace Utility.Audio
{
    /// <summary>
    /// A single instance to manage all sounds in the game.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        private void Awake()
        {
            instance = this;
        }

        /// <summary>
        /// Plays an Audioclip at a position.
        /// </summary>
        /// <param name="clip">Which clip to play.</param>
        /// <param name="position">Where the clip will originate.</param>
        /// <param name="volume">How loud to play the clip. (Whithin the range of 0 and 1.)</param>
        public static void PlayClip(AudioClip clip, Vector3 position, float volume = 1)
        {
            if (volume > 1 || volume < 0)
            {
                Debug.LogError($"Volume of {volume} is outside of volume range!(0, 1)");

                volume = 0.5f;
            }

            AudioSource.PlayClipAtPoint(clip, position, volume * PlayerPrefsManager.SFXVolume);
        }

        /// <summary>
        /// Returns the distance to the camera as a volume.
        /// </summary>
        /// <param name="playedFrom"></param>
        /// <returns></returns>
        public static float GetClipVolume(Vector2 playedFrom)
        {
            return Mathf.Clamp(Vector3.Distance(Camera.main.transform.position, playedFrom), 0, 1f);
        }

        public AudioClip[] EatSounds;

        public void PlayEat(Vector3 fromPoint)
        {
            float distToCam = GetClipVolume(fromPoint);

            AudioClip clip = EatSounds[Random.Range(0, EatSounds.Length)];

            PlayClip(clip, fromPoint, distToCam);
        }

        public AudioClip[] DrinkSounds;

        public void PlayDrink(Vector3 fromPoint)
        {
            float distToCam = GetClipVolume(fromPoint);

            AudioClip clip = DrinkSounds[Random.Range(0, DrinkSounds.Length)];

            PlayClip(clip, fromPoint, distToCam);
        }

        public AudioClip[] JumpSounds;

        public void PlayJumpSound(Vector3 fromPoint)
        {
            float distToCam = GetClipVolume(fromPoint);

            AudioClip clip = JumpSounds[Random.Range(0, JumpSounds.Length)];

            PlayClip(clip, fromPoint, distToCam);
        }

        public AudioClip[] DashSounds;

        public void PlayDashSound(Vector3 fromPoint)
        {
            float distToCam = GetClipVolume(fromPoint);

            AudioClip clip = DashSounds[Random.Range(0, DashSounds.Length)];

            PlayClip(clip, fromPoint, distToCam);
        }

        public AudioClip[] PlayerAttack1Sounds;

        public void PlayPlayerAttack1Sound(Vector3 fromPoint)
        {
            float distToCam = GetClipVolume(fromPoint);

            AudioClip clip = PlayerAttack1Sounds[Random.Range(0, PlayerAttack1Sounds.Length)];

            PlayClip(clip, fromPoint, distToCam);
        }

        public AudioClip[] PickupCoinSounds;

        public void PlayPickupCoinSound(Vector3 fromPoint)
        {
            float distToCam = GetClipVolume(fromPoint);

            AudioClip clip = PickupCoinSounds[Random.Range(0, PickupCoinSounds.Length)];

            PlayClip(clip, fromPoint, distToCam);
        }

        public AudioClip[] ProjectileHitWallSounds;

        public void PlayProjectileHitWallSound(Vector3 fromPoint)
        {
            float distToCam = GetClipVolume(fromPoint);

            AudioClip clip = ProjectileHitWallSounds[Random.Range(0, ProjectileHitWallSounds.Length)];

            PlayClip(clip, fromPoint, distToCam);
        }
    }
}