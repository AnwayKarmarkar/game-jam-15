using System.Text;
using UnityEngine;

namespace Assets.Scripts.Alchemy {
    public class AlchemyMenu : MonoBehaviour {
        public static bool ShowAlchemyMenu;
        public GameObject AlchemyMenuPanel;

        private StringBuilder _stringBuilder;
        private const int StringBuilderMaxCapacity = 30;

        // Start is called before the first frame update
        private void Start() {
            _stringBuilder = new StringBuilder("", 30);
        }

        // Update is called once per frame
        private void Update() {
            if (Input.GetKey(KeyCode.LeftControl)) {
                if (!ShowAlchemyMenu) {
                    ShowAlchemyMenu = true;
                    OpenAlchemyMenu();
                }
                else {
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                        SafeAppend(_stringBuilder.Length, ("1"));
                    }
                    else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                        SafeAppend(_stringBuilder.Length, ("2"));
                    }
                    else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                        SafeAppend(_stringBuilder.Length, ("3"));
                    }
                    else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                        SafeAppend(_stringBuilder.Length, ("4"));
                    }
                }
                return;
            }

            if (!Input.GetKeyUp(KeyCode.LeftControl)) return;
            ShowAlchemyMenu = false;
            CloseAlchemyMenu();
        }

        // Stop people for filling up the string builder beyond capacity (unlikely to happen, but still)
        private void SafeAppend(int length, string val) {
            if (length >= StringBuilderMaxCapacity) return;
            _stringBuilder.AppendFormat(val);
        }

        private void OpenAlchemyMenu() {
            AlchemyMenuPanel.SetActive(true);
        }

        private void CloseAlchemyMenu() {
            AlchemyMenuPanel.SetActive(false);
            var formula = _stringBuilder.ToString();
            if (!string.IsNullOrEmpty(formula)) {
                AlchemyService.SetActiveCompound(formula);
            }
            _stringBuilder.Clear();
        }
    }
}