using EasyAudioSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GPG212_02
{
    public class GameManager : MonoBehaviour
    {
        public static ColourChangeHandler currentHandler;

        [SerializeField] private Slider loadingBar;
        [SerializeField] private GameObject loadingPanel;
        [SerializeField] private GameObject victoryPanel;
        [SerializeField] private GameObject defeatPanel;

        private void Start()
        {
            AudioManager.PlayAudio("BackgroundMusic");
        }

        public void StartMatching()
        {
            ResetMatchPanel();
            AudioManager.StopAudio();
            AudioManager.PlayAudio("WaitingMusic");
            StartCoroutine(Loading(loadingBar, 5f));
        }

        private IEnumerator Loading(Slider slider, float time)
        {
            yield return new WaitForSeconds(0.6f);

            float counter = 0f;

            while (counter < time)
            {
                counter += Time.deltaTime;
                slider.value = Mathf.Lerp(0f, 1f, counter/time);

                yield return null;
            }
            
            loadingPanel.SetActive(false);
            Matched();
        }

        private void Matched()
        {
            if (Random.Range(1, 6) == 1)
            {
                // MATCHED
                AudioManager.StopAudio();
                AudioManager.PlayAudio("VictoryMusic");
                victoryPanel.SetActive(true);
            }
            else
            {
                // NOT MATCHED
                AudioManager.StopAudio();
                AudioManager.PlayAudio("DefeatMusic");
                defeatPanel.SetActive(true);
            }
        }

        private void ResetMatchPanel()
        {
            victoryPanel.SetActive(false);
            defeatPanel.SetActive(false);
            loadingBar.value = 0f;
            loadingPanel.SetActive(true);
        }
        public void ChangeCurrentColourHandler(ColourChangeHandler handler)
        {
            currentHandler = handler;
        }
    }
}