using UnityEngine;

namespace Assets.Scripts {
    public class PlayerMovement : MonoBehaviour {
        public float MoveSpeed;
        public Rigidbody2D Rb;
        public bool DisableMovement {
            get => AlchemyMenu.ShowAlchemyMenu;
            set { }
        }

        private Vector2 _movement;

        public PlayerMovement(float moveSpeed) {
            MoveSpeed = moveSpeed;
        }

        // Update is called once per frame
        private void Update() {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");
        }

        private void FixedUpdate() {
            if (!DisableMovement) {
                Rb.MovePosition(Rb.position + _movement * MoveSpeed * Time.fixedDeltaTime);
            }
        }
    }
}
