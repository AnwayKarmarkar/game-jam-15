using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets {
    public class Ball : MonoBehaviour {
        public int damage = 25;
        public bool isEnemy = false;
        [SerializeField]
        private Rigidbody2D rb;
        [SerializeField]
        private float speed = 2;
        [SerializeField]
        private float aliveTime = 4;
        void Start() {
            Destroy(gameObject, aliveTime);
            rb.AddForce(transform.right * speed);
        }




        private void OnTriggerEnter2D(Collider2D collision) {

            if (!isEnemy && collision.GetComponent<Enemy>()) {

                collision.GetComponent<Enemy>().TakeDamage(damage, true, transform.position);
                rb.velocity = Vector3.zero;

            }
            if (collision.gameObject.layer == 8)
            {


                rb.velocity = Vector3.zero;

            }

            if (isEnemy && collision.GetComponent<PlayerAttack>()) {

                collision.GetComponent<PlayerAttack>().TakeDamage(damage);
                Destroy(gameObject);


            }

        }
    }

}
