using UnityEngine;

namespace Assets.Scripts {
    public class PauseMenu : MonoBehaviour {
        private bool GamePaused;
        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private GameObject controlsMenuUI;
        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (GamePaused) {
                    GamePaused = false;
                    ResumeGame();
                }
                else {
                    GamePaused = true;
                    PauseGame();
                }
            }
        }

        public void PauseGame() {
            pauseMenuUI.SetActive(true);
            controlsMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }

        public void ResumeGame() {
            pauseMenuUI.SetActive(false);
            controlsMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }
        public void QuitGame() {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}
