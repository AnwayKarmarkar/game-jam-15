using UnityEngine;

namespace Assets.Scripts.Alchemy {
    public class AlchemyService : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public static void SetActiveCompound(string formula) {
            if (GameManager.ViableCompounds.ContainsKey(formula)) {
                Player.ActiveCompound = GameManager.ViableCompounds[formula];
            }
        }
    }
}
