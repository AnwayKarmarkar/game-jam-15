using UnityEngine;

namespace Assets.Scripts {
    public class AlchemyMenu : MonoBehaviour {
        public static bool ShowAlchemyMenu;
        public GameObject AlchemyMenuPanel;

        // Start is called before the first frame update
        private void Start() {
            Debug.Log("START");
        }

        // Update is called once per frame
        private void Update() {
            if (Input.GetKey(KeyCode.LeftControl)) {
                if (!ShowAlchemyMenu) {
                    ShowAlchemyMenu = true;
                    OpenAlchemyMenu();
                }
                else {
                    if (Input.GetKeyDown(KeyCode.W)) {
                        Debug.Log("W");
                    }
                    else if (Input.GetKeyDown(KeyCode.A)) {
                        Debug.Log("A");
                    }
                    else if (Input.GetKeyDown(KeyCode.S)) {
                        Debug.Log("S");
                    }
                    else if (Input.GetKeyDown(KeyCode.D)) {
                        Debug.Log("D");
                    }
                }
                return;
            }

            if (Input.GetKeyUp(KeyCode.LeftControl)) {
                ShowAlchemyMenu = false;
                CloseAlchemyMenu();
            }
        }

        private void OpenAlchemyMenu() {
            AlchemyMenuPanel.SetActive(true);
        }

        private void CloseAlchemyMenu() {
            AlchemyMenuPanel.SetActive(false);
        }
    }
}