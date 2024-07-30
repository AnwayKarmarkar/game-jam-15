using UnityEngine;

namespace Assets {
    public class Player : MonoBehaviour {
        public static GameManager.Compound ActiveCompound { get; set; }

        public PlayerAttack FlareGun;

        private Camera mainCam;
        private Vector3 mousePos;
        // Start is called before the first frame update
        void Start() {
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update() {
            //mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

            //var rotation = mousePos - transform.position;

            //var rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            //transform.rotation = Quaternion.Euler(0, 0, rotZ);

            if (ActiveCompound == null) return;
            if (FlareGun == null) return;
            FlareGun.CreateFlare(ActiveCompound);
            ActiveCompound = null;
        }
    }
}
