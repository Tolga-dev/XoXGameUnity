using System;
using System.Collections;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class CameraShakeController : Singleton<CameraShakeController>
    {
        public Camera camera;
        public Vector3 originalPosition;

        private void Start()
        {
            originalPosition= camera.transform.localPosition;
        }

        public IEnumerator Shake(float duration, float magnitude)
        {

            float elapsed = 0f;

            while (elapsed < duration)
            {
                float offsetX = Random.Range(-1f, 1f) * magnitude;
                float offsetY = Random.Range(-1f, 1f) * magnitude;

                camera.transform.localPosition = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

                elapsed += Time.deltaTime;

                yield return null;
            }

            camera.transform.localPosition = originalPosition;
        }
        public void VibratePhone()
        {
#if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();
#endif
        }
        public void TriggerShake(float duration, float magnitude)
        {
            StartCoroutine(Shake(duration, magnitude));
            VibratePhone();
        }
    }
}