using EasyAudioSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GPG212_02
{
    public class MainSceneNavigation : MonoBehaviour
    {
        [SerializeField] private CanvasGroup mainMenuCanvas;
        [SerializeField] private CanvasGroup characterCreationCanvas;
        [SerializeField] private CanvasGroup matchingCanvas;
        [SerializeField] private Light characterSpotlight;

        private readonly Vector3 position1 = new Vector3(-29.28f, 8.98f, 4.08f);
        private readonly Vector3 position2 = new Vector3(-19.8f, 2.45f, 1.09f);

        private readonly Vector3 rotation1 = new Vector3(27.19f, 130.97f, 0f);
        private readonly Vector3 rotation2 = new Vector3(8.3f, 178.26f, 0f);

        private readonly int fov1 = 65;
        private readonly int fov2 = 43;

        private Camera _camera;

        private void Start()
        {
            _camera = GetComponent<Camera>();
        }
        public void MoveToCharacterCreation()
        {
            AudioManager.PlayAudio("ButtonClick");

            StartCoroutine(FadeCanvas(mainMenuCanvas, 1f, 0f, 0.5f));
            StartCoroutine(MoveCameraWithFOV(transform, _camera, position1, position2, rotation1, rotation2, fov1, fov2, 1.5f, characterCreationCanvas));
            StartCoroutine(FadeLight(characterSpotlight, 0, 5.9f, 1.5f));
        }

        public void MoveToMainMenu()
        {
            AudioManager.PlayAudio("ButtonClick");

            StartCoroutine(FadeCanvas(matchingCanvas, 1f, 0f, 0.5f));
            StartCoroutine(MoveCameraWithFOV(transform, _camera, position2, position1, rotation2, rotation1, fov2, fov1, 1.5f, mainMenuCanvas));
            StartCoroutine(FadeLight(characterSpotlight, 5.9f, 0f, 1.5f));
        }

        public void StartMatching()
        {
            AudioManager.PlayAudio("ButtonClick");

            StartCoroutine(FadeCanvas(characterCreationCanvas, 1f, 0f, 0.5f));
            StartCoroutine(FadeCanvas(matchingCanvas, 0f, 1f, 0.5f));
        }

        private IEnumerator FadeCanvas(CanvasGroup canvas, float start, float end, float time)
        {
            if (!canvas.gameObject.activeSelf)
            {
                canvas.alpha = 0f;
                canvas.gameObject.SetActive(true);
            }
            float counter = 0f;

            while (counter < time)
            {
                counter += Time.deltaTime;
                canvas.alpha = Mathf.Lerp(start, end, counter / time);

                yield return null;
            }
            if (canvas.alpha == 0)
            {
                canvas.gameObject.SetActive(false);
            }
        }

        private IEnumerator MoveCameraWithFOV(Transform transform, Camera camera, Vector3 startPos, Vector3 endPos, Vector3 startRot, Vector3 endRot, float startFOV, float endFOV, float time, CanvasGroup canvas)
        {
            float counter = 0f;
            while (counter < time)
            {
                counter += Time.deltaTime;
                transform.position = Vector3.Lerp(startPos, endPos, counter / time);
                transform.eulerAngles = Vector3.Lerp(startRot, endRot, counter / time);
                camera.fieldOfView = Mathf.Lerp(startFOV, endFOV, counter / time);

                yield return null;
            }
            canvas.gameObject.SetActive(true);
            StartCoroutine(FadeCanvas(canvas, 0f, 1f, 0.5f));
        }

        private IEnumerator FadeLight(Light light, float start, float end, float time)
        {
            float counter = 0f;
            while (counter < time)
            {
                counter += Time.deltaTime;
                light.intensity = Mathf.Lerp(start, end, counter / time);

                yield return null;
            }
        }

        private void OnValidate()
        {
            
        }
    }
}