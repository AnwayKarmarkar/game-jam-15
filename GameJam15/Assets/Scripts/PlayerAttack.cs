using Assets.Scripts.Alchemy;
using UnityEngine;

namespace Assets {
    public class PlayerAttack : MonoBehaviour {
        [SerializeField] private int _health = 100;

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

        public bool DisableMovement {
            get => AlchemyMenu.ShowAlchemyMenu;
            set { }
        }

        private void Start() {
            _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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
            if (Input.GetKeyDown(KeyCode.Mouse0) && _timeToLoad <= 0 && _isLoaded) {
                Instantiate(Ball, FirePoint.position, FirePoint.rotation);
                _timeToLoad = _loadingTime;
                _isLoaded = false;
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
            _health -= damage;
            if (_health <= 0) {
                //Destroy(gameObject);
            }
        }

        private void LookAtMouse() {
            _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            var rotation = _mousePos - _pivot.position;
            var rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            _pivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }

        private void ControlMenu() { }
    }
}