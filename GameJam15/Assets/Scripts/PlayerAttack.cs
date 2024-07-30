using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Alchemy;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace Assets {
    public class PlayerAttack : MonoBehaviour {
        [SerializeField]
        private int health = 100;

        public GameObject ball;
        public Transform firePoint;
        private Vector2 movement;
        public Rigidbody2D rb;
        private float InputX;
        private float InputY;
        [SerializeField]
        private float speed = 8;
        private Camera mainCam;
        private Vector3 mousePos;
        [SerializeField]
        private Transform pivot;
        [SerializeField]
        private float loadingTime = 2f;
        float timeToLoad;
        bool isLoading = false;
        bool isLoaded = false;
        public LayerMask attackMask;
        public GameObject FlarePrefab;

        public int FlareDuration = 1;
        GameObject flare;


        [SerializeField] private Slider playerHealthBar;
        public bool DisableMovement {
            get => AlchemyMenu.ShowAlchemyMenu;
            set { }
        }
        void Start() {
            playerHealthBar.maxValue = health;
            playerHealthBar.value = health;
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        void Update() {
            LookAtMouse();

            if (Input.GetKeyDown(KeyCode.E))
            {

                RaycastHit2D result = Physics2D.BoxCast(firePoint.position, new Vector2(0.75f, 0.75f), 0f, firePoint.right, 1, attackMask);
                if (result != null && result.collider.GetComponent<Enemy>())
                {

                    result.collider.GetComponent<Enemy>().TakeDamage(25,true, transform.position);
                }

            }
            //Add double tap
            if (Input.GetKeyDown(KeyCode.Mouse0) && timeToLoad <= 0 && isLoaded) {
                
                GameObject bolt = Instantiate(ball, firePoint.position, firePoint.rotation);
                if(flare != null)
                {
                    flare.transform.SetParent(bolt.transform);
                    flare.transform.localPosition = Vector3.zero;

                }
                timeToLoad = loadingTime;
                
                isLoaded = false;
            }
            if (Input.GetKeyDown(KeyCode.R) && !isLoading && !isLoaded) {
                timeToLoad = loadingTime;
                
                isLoading = true;
            }

            if (timeToLoad > 0 && isLoading) {
                timeToLoad -= Time.deltaTime;
            }
            else if (timeToLoad <= 0) {
                isLoaded = true;
                isLoading = false;
            }
        }

        private void FixedUpdate() {
            if (!DisableMovement) {
                InputX = Input.GetAxisRaw("Horizontal");
                InputY = Input.GetAxisRaw("Vertical");
                movement = new Vector2(InputX, InputY);

                rb.velocity = movement.normalized * speed;
            }else
            {

                rb.velocity = Vector2.zero;

            }
        }
        public void TakeDamage(int damage) {
            health -= damage;
            playerHealthBar.value = health;

            if (health <= 0) {
                //Destroy(gameObject);
            }
        }

        void LookAtMouse() {
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 rotation = mousePos - pivot.position;
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            pivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }
        public void CreateFlare(GameManager.Compound compound)
        {

            string tagName = "";
            flare = Instantiate(FlarePrefab, this.transform.position, this.transform.rotation, transform);

            switch (compound.Name)
            {
                case "NaCl":
                    tagName = "YellowFlare";
                    flare.GetComponentInChildren<FieldOfView>().SetLightId(1);

                    break;
                case "Sr(NO3)2":
                    tagName = "RedFlare";
                    flare.GetComponentInChildren<FieldOfView>().SetLightId(0);


                    break;
            }
            flare.tag = tagName;

            var light = flare.GetComponent<Light2D>();
            light.enabled = true;
            light.color = compound.Color;
        }
    }

     
    
}
