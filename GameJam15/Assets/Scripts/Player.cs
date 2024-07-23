using UnityEngine;

namespace Assets {
    public class Player : MonoBehaviour
    {
        public static GameManager.Compound ActiveCompound { get; set; }

        public GameObject FlareGun;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update() {
            if (ActiveCompound == null) return;
            var gun = FlareGun.GetComponent<FlareGun>();
            if (gun == null) return;
            gun.CreateFlare(ActiveCompound);
            ActiveCompound = null;
        }
    }
}
