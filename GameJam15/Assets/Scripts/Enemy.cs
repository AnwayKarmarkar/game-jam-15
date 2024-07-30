using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
namespace Assets {
    public class Enemy : MonoBehaviour {
        [SerializeField]
        private int health = 100;
        public enum EnemyType { Melee, Ranged }
        [SerializeField]
        private EnemyType type;
        [SerializeField]
        private NavMeshAgent agent;
        [SerializeField]
        private Transform playerTarget;
        [SerializeField]
        private float attackingRange = 1;
        [SerializeField]
        private float checkDistance = 10;

        public enum LightWeakness { Red, Yellow, Blue, Green, Violet }
        [SerializeField]
        private LightWeakness weakness;

        [Header("Ranged Enemy:")]
        [SerializeField]
        private float retreatRange = 4;
        [SerializeField]
        private GameObject projectile;
        [SerializeField]
        private Transform firePoint;
        private float attackRate = 2;
        float timeAttack;
        [SerializeField]
        private Transform backWard; 

        [SerializeField] private LayerMask attackMask;
        [SerializeField] private Transform meleePoint;
        [SerializeField] private int meleeDamage = 50;

        [SerializeField] private Slider healthBar;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private GameObject barObject;
        public float visibleTime;
        public bool isMarked = false;
        private int markDamage = 0;
        private int weaknessId;

        void Start() {
            spriteRenderer = GetComponent<SpriteRenderer>();    
            healthBar.value = health;
            agent.stoppingDistance = attackingRange;
            playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.SetDestination(playerTarget.position);


            switch (weakness)
            {
                case LightWeakness.Red:
                    weaknessId = 0;
                    break;
                case LightWeakness.Yellow:
                    weaknessId = 1;

                    break;
                case LightWeakness.Green:
                    weaknessId = 2;

                    break;
                case LightWeakness.Blue:
                    weaknessId = 3;

                    break;
                case LightWeakness.Violet:
                    weaknessId = 4;

                    break;
            }

        }

        void Update() {
            switch (type) {
                case EnemyType.Melee:
                    Melee();
                    break;
                case EnemyType.Ranged:
                    Ranged();
                    break;
            }

          


            if(visibleTime > 0)
            {
                visibleTime -= Time.deltaTime;
               
            }else
            {
                if (isMarked)
                    isMarked = false;
                barObject.SetActive(false);
                spriteRenderer.enabled = false;

            }
        }

/*        void SearchForLight(string tagName) {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tagName);

            foreach (GameObject obj in taggedObjects) {
                float distance = Vector3.Distance(transform.position, obj.transform.position);

                if (distance <= checkDistance) {
                    Debug.Log($"GameObject '{obj.name}' with tag '{tagName}' is within {checkDistance} units.");
                }
            }
        }*/
        void Melee() {
            agent.SetDestination(playerTarget.position);

            Vector3 rotation = playerTarget.position - transform.position;

            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            firePoint.rotation = Quaternion.Slerp(firePoint.rotation, Quaternion.Euler(0, 0, rotZ), 0.001f);

            if (agent.remainingDistance <= attackingRange) {

                timeAttack += Time.deltaTime;

                if (timeAttack >= attackRate) {
                    RaycastHit2D result = Physics2D.BoxCast(meleePoint.position, new Vector2(1, 1), 0f, meleePoint.right, 1, attackMask);
                    if (result.collider.GetComponent<PlayerAttack>() != null) {

                        result.collider.GetComponent<PlayerAttack>().TakeDamage(meleeDamage);

                    }

                    //print(result.collider.GetComponent<PlayerAttack>());
                    timeAttack = 0;
                }


            }



        }
        void Ranged() {

            if (Vector2.Distance(transform.position, playerTarget.position) <= retreatRange) {
                agent.SetDestination(backWard.position);
                RangedAttack();


            }
            else if (agent.remainingDistance <= attackingRange) {
                agent.SetDestination(playerTarget.position);
                RangedAttack();






            }
            else {


                agent.SetDestination(playerTarget.position);

            }




        }

        void RangedAttack() {

            Vector3 rotation = playerTarget.position - transform.position;

            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            firePoint.rotation = Quaternion.Euler(0, 0, rotZ);
            timeAttack += Time.deltaTime;

            if (timeAttack >= attackRate) {

                GameObject thisInstance = Instantiate(projectile, firePoint.position, firePoint.rotation);
                thisInstance.GetComponent<Ball>().isEnemy = true;
                timeAttack = 0;
            }

        }

        public void TakeDamage(int damage,bool isKnocked, Vector3 pos)
        {

            

            health -= damage;
            if(isMarked)
            {
                health -= markDamage;
                isMarked = false;   


            }
            if (health <= 0)
            {

                Destroy(gameObject);

            }
            if (isKnocked)
            {
                Vector2 difference = (transform.position - pos).normalized * 1;
                rb.velocity = difference * 5;
            agent.SetDestination(transform.position);

                Invoke("EndKnockBack", 0.3f);
            }
            healthBar.value = health;
        }

        void EndKnockBack()
        {
            rb.velocity = Vector3.zero;
                
            Invoke("SetAgent", 0.3f);

        }

        void SetAgent()
        {

            agent.SetDestination(playerTarget.position);

        }
        public void SetVisibility()
        {
            barObject.SetActive(true);
            spriteRenderer.enabled = true;
           
            visibleTime = 0.25f;


        }
        public void SetMark(int damage, int lightId)
        {

            if (lightId == weaknessId)
            {

                markDamage = damage;
                isMarked = true;
            }

        }

    }

   
}