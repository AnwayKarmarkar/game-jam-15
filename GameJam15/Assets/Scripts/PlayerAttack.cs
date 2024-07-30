using Assets.Scripts.Alchemy;
<<<<<<< HEAD
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
=======
using UnityEngine;
>>>>>>> 0879f2e6ab6b5671717625d42c2400c7862ba728

namespace Assets {
    public class PlayerAttack : MonoBehaviour {
        [SerializeField] private int _health = 100;

<<<<<<< HEAD
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
=======
        public GameObject Ball;
        public Transform FirePoint;
        private Vector2 _movement;
        public Rigidbody2D Rb;
        private float _inputX;
        private float _inputY;
        [SerializeField] private readonly float _speed = 8;
        private Camera _mainCam;
        private Vector3 _mousePos;
        [SerializeField] private Transform _pivot;
        [SerializeField] private readonly float _loadingTime = 2f;

        private float _timeToLoad;
        private bool _isLoading;
        private bool _isLoaded;
        public LayerMask AttackMask;
>>>>>>> 0879f2e6ab6b5671717625d42c2400c7862ba728

        public int FlareDuration = 1;
        GameObject flare;


        [SerializeField] private Slider playerHealthBar;
        public bool DisableMovement {
            get => AlchemyMenu.ShowAlchemyMenu;
            set { }
        }
<<<<<<< HEAD
        void Start() {
            playerHealthBar.maxValue = health;
            playerHealthBar.value = health;
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
=======

        private void Start() {
            _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
>>>>>>> 0879f2e6ab6b5671717625d42c2400c7862ba728
        }

        private void Update() {
            LookAtMouse();

            if (Input.GetKeyDown(KeyCode.E)) {
                var result = Physics2D.BoxCast(FirePoint.position, new Vector2(0.75f, 0.75f), 0f, FirePoint.right, 1,
                    AttackMask);
                if (result.collider.GetComponent<Enemy>())
                    result.collider.GetComponent<Enemy>().TakeDamage(25, true, transform.position);
            }

            //Add double tap
<<<<<<< HEAD
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
=======
            if (Input.GetKeyDown(KeyCode.Mouse0) && _timeToLoad <= 0 && _isLoaded) {
                Instantiate(Ball, FirePoint.position, FirePoint.rotation);
                _timeToLoad = _loadingTime;
                _isLoaded = false;
>>>>>>> 0879f2e6ab6b5671717625d42c2400c7862ba728
            }

            if (Input.GetKeyDown(KeyCode.R) && !_isLoading && !_isLoaded) {
                _timeToLoad = _loadingTime;
                _isLoading = true;
            }

            switch (_timeToLoad) {
                case > 0 when _isLoading:
                    _timeToLoad -= Time.deltaTime;
                    break;
                case <= 0:
                    _isLoaded = true;
                    _isLoading = false;
                    break;
            }
        }

        private void FixedUpdate() {
            if (!DisableMovement) {
                _inputX = Input.GetAxisRaw("Horizontal");
                _inputY = Input.GetAxisRaw("Vertical");
                _movement = new Vector2(_inputX, _inputY);

                Rb.velocity = _movement.normalized * _speed;
            }
            else {
                Rb.velocity = Vector2.zero;
            }
        }

        public void TakeDamage(int damage) {
<<<<<<< HEAD
            health -= damage;
            playerHealthBar.value = health;

            if (health <= 0) {
=======
            _health -= damage;
            if (_health <= 0) {
>>>>>>> 0879f2e6ab6b5671717625d42c2400c7862ba728
                //Destroy(gameObject);
            }
        }

        private void LookAtMouse() {
            _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            var rotation = _mousePos - _pivot.position;
            var rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            _pivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }
<<<<<<< HEAD
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
=======

        private void ControlMenu() { }
    }
}
>>>>>>> 0879f2e6ab6b5671717625d42c2400c7862ba728
