using System;
using System.Collections;
using Managers;
using So;
using SoScripts;
using UnityEngine;

namespace Controllers
{
    [Serializable]
    public class SoundController : ControllerBase
    {
        public AudioSource mainGameSound;
        public GameObject gameEffectSoundPlayer;

        private GamePlayConfigurations _gamePlayConfigurations;
        public override void Starter(GameManager gameManagerInGame)
        {
            GameManager = gameManagerInGame;
            _gamePlayConfigurations = gameManagerInGame.gamePlayConfigurations;
            Debug.Log("PlayerInGameController Starter");
        }
        
        public void PlayMainGameSound(string newClipName, float fadeDuration = 1.0f)
        {
            var newClip = _gamePlayConfigurations.GetSound(newClipName);
            GameManager.StartCoroutine(FadeToNewMainGameSound(newClip, fadeDuration));
        }

        private IEnumerator FadeToNewMainGameSound(AudioClip newClip, float fadeDuration)
        {
            if (mainGameSound.isPlaying)
            {
                // Fade out the current sound.
                for (float t = 0; t < fadeDuration; t += Time.deltaTime)
                {
                    mainGameSound.volume = Mathf.Lerp(1.0f, 0.0f, t / fadeDuration);
                    yield return null;
                }
                mainGameSound.Stop();
            }

            // Set the new clip and fade it in.
            mainGameSound.clip = newClip;
            mainGameSound.Play();
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                mainGameSound.volume = Mathf.Lerp(0.0f, 1.0f, t / fadeDuration);
                yield return null;
            }
            mainGameSound.volume = 1.0f; // Ensure volume is fully restored.
        }

        public void RunASound(string soundName)
        {
            if (_gamePlayConfigurations.gameEffectSound == false)
                return;
            
            var sound = _gamePlayConfigurations.GetSound(soundName);
            var createdSound = Instantiate(gameEffectSoundPlayer);
            var source = createdSound.GetComponent<AudioSource>();
            
            if (sound != null && source != null)
            {
                source.PlayOneShot(sound);
                Destroy(createdSound, sound.length);
            }
            else
            {
                Debug.LogWarning($"Sound {soundName} not found or gameEffectSound is not assigned.");
            }
            
        }

    }
}