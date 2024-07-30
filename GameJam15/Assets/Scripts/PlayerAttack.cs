using Assets.Scripts.Alchemy;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine;

namespace Assets
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private int health = 100;
        private Camera mainCam;
        public GameObject FlarePrefab;
        public GameObject Ball;
        public Transform FirePoint;
        private Vector2 _movement;
        public Rigidbody2D Rb;
        private float _inputX;
        private float _inputY;
        [SerializeField] private float _speed = 4;
        private Vector3 _mousePos;
        [SerializeField] private Transform _pivot;
        [SerializeField] private readonly float _loadingTime = 2f;
        [SerializeField] private float _timeToLoad;
        [SerializeField] private bool _isLoading = false;
        private bool _isLoaded = false;
        public LayerMask AttackMask;

        public int FlareDuration = 1;
        GameObject flare;
        float meleeTime;
        [SerializeField] private float meleeCoolDown = 1;

        public bool DisableMovement
        {
            get => AlchemyMenu.ShowAlchemyMenu;
            set { }
        }
        void Start()
        {
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        }
        private void Update()
        {
            LookAtMouse();

            if (Input.GetKeyDown(KeyCode.E) && meleeTime <= 0)
            {
                var result = Physics2D.BoxCast(FirePoint.position, new Vector2(0.75f, 0.75f), 0f, FirePoint.right, 1,
                    AttackMask);
                if (result.collider.GetComponent<Enemy>())
                    result.collider.GetComponent<Enemy>().TakeDamage(25, true, transform.position);
                meleeTime = meleeCoolDown;
            }

            if(meleeCoolDown > 0)
            {

                meleeTime -= Time.deltaTime;

            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && _timeToLoad <= 0 && _isLoaded)
            {
                GameObject bolt = Instantiate(Ball, FirePoint.position, FirePoint.rotation);
                if (flare != null)
                {
                    flare.transform.SetParent(bolt.transform);
                    flare.transform.localPosition = Vector3.zero;
                    flare = null;
                }
                _timeToLoad = _loadingTime;

                _isLoaded = false;
            }
        
            if (Input.GetKeyDown(KeyCode.R) && !_isLoading && !_isLoaded)
            {
                _timeToLoad = _loadingTime;
                _isLoading = true;
            }
            switch (_timeToLoad)
            {
                case > 0 when _isLoading:
                    _timeToLoad -= Time.deltaTime;
                    break;
                case <= 0:
                    _isLoaded = true;
                    _isLoading = false;
                    break;
            }
        }

        private void FixedUpdate()
        {
            if (!DisableMovement)
            {
                _inputX = Input.GetAxisRaw("Horizontal");
                _inputY = Input.GetAxisRaw("Vertical");
                _movement = new Vector2(_inputX, _inputY);

                Rb.velocity = _movement.normalized * _speed;
            }
            else
            {
                Rb.velocity = Vector2.zero;
            }
        }

        public void TakeDamage(int damage)
        {
            health -= damage;

            if (health <= 0)
            {
                health -= damage;
                if (health <= 0)
                {
                }
            }
        }
        private void LookAtMouse()
        {
            _mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            var rotation = _mousePos - _pivot.position;
            var rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            _pivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }
        public void CreateFlare(GameManager.Compound compound)
        {

            string tagName = "";
            if(flare)
            {
                Destroy(flare);

            }
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
                case "CuSO4":
                    tagName = "GreenFlare";
                    flare.GetComponentInChildren<FieldOfView>().SetLightId(2);
                    break;
                case "CuCl2":
                    tagName = "BlueFlare";
                    flare.GetComponentInChildren<FieldOfView>().SetLightId(3);
                    break;
                case "KNO3KSO4":
                    tagName = "VioletFlare";
                    flare.GetComponentInChildren<FieldOfView>().SetLightId(4);
                    break;
            }

            flare.tag = tagName;

            var light = flare.GetComponent<Light2D>();
            light.enabled = true;
            light.color = compound.Color;
        }
    }
}







