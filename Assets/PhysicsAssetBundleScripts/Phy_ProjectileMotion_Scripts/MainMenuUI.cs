using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectileMotionDesktop
{
    public class MainMenuUI : MonoBehaviour
    {
        public MouseLook mouseLook;
        public playerMove playermove;
        public CannonInteraction cannonUI;
        public GameObject MainPanel;
        public GameObject ControlsPanel;
        public GameObject PointerCenterScreen;
        public bool enable = true;
        public bool active = false;
        AudioSource selectionSound;


        private void Start()
        {
            selectionSound = GetComponent<AudioSource>();
        }

        IEnumerator TakeScreenshotAndPause()
        {
            yield return new WaitForEndOfFrame();
            Texture2D screenImage = new Texture2D(Screen.width, Screen.height);
            screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            screenImage.Apply();
            UIEventController.OnModulePause?.Invoke(screenImage);
            PauseModule();
        }

        IEnumerator ResumeCallback()
        {
            yield return new WaitForEndOfFrame();
            UIEventController.OnModuleResume?.Invoke();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))// && cannonUI.cannonUIenable == false)
            {
                if (enable)
                {
                    active = !active;

                    if (active)
                        StartCoroutine(TakeScreenshotAndPause());
                    else
                        ResumeModule();
                }
            }
        }

        public void Controls()
        {
            ControlsPanel.SetActive(true);
            enable = false;
            MainPanel.SetActive(false);
        }

        public void ExitToDesktop()
        {
            Application.Quit();
        }

        public void Cancel()
        {
            active = false;
            ResumeModule();
        }

        public void PauseModule()
        {
            MouseLook.freeze_view = true;
            playermove.enabled = false;
            PointerCenterScreen.SetActive(false);
            selectionSound.Play();
            MainPanel.SetActive(active);
        }

        public void ResumeModule()
        {
            playermove.enabled = true;
            MouseLook.freeze_view = false;
            PointerCenterScreen.SetActive(true);
            selectionSound.Play();
            MainPanel.SetActive(false);
            StartCoroutine(ResumeCallback());
        }

        public void MenuVisibility()
        {
            if (active)                    
            {
                MouseLook.freeze_view = true;
                playermove.enabled = false;
                PointerCenterScreen.SetActive(false);
            }
            else
            {
                playermove.enabled = true;
                MouseLook.freeze_view = false;
                PointerCenterScreen.SetActive(true);
            }
            MainPanel.SetActive(active);
        }

        public void Mute()
        {
            AudioListener.volume = 0;
        }

        public void UnMute()
        {
            AudioListener.volume = 1;
        }

        public void PlaySelectionSound()
        {
            selectionSound.Play();
        }

    }
}
