using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int health = 100;
    public enum EnemyType {Melee, Ranged}
    [SerializeField]
    private EnemyType type;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Transform playerTarget;
    [SerializeField]
    private float attackingRange = 1;
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

    [SerializeField]  private LayerMask attackMask;
    [SerializeField]  private Transform meleePoint;
    [SerializeField] private int meleeDamage = 50;

    [SerializeField] private Rigidbody2D rb;


    void Start()
    {
        agent.stoppingDistance = attackingRange;
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(playerTarget.position);

    }

    void Update()
    {


        switch (type)
        {
            case EnemyType.Melee:

                if(agent.enabled)
                Melee();
                break;
            case EnemyType.Ranged:
                Ranged();
                break;




        }
    }


    void Melee()
    {
        agent.SetDestination(playerTarget.position);

        Vector3 rotation = playerTarget.position - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        firePoint.rotation = Quaternion.Slerp(firePoint.rotation, Quaternion.Euler(0, 0, rotZ), 0.001f);

        if (agent.remainingDistance <= attackingRange)
        {
           
            timeAttack += Time.deltaTime;

            if (timeAttack >= attackRate)
            {
                RaycastHit2D result = Physics2D.BoxCast(meleePoint.position, new Vector2(0.75f, 0.75f), 0f, meleePoint.right, 1, attackMask);
                if(result.collider.GetComponent<PlayerAttack>())
                {

                    result.collider.GetComponent<PlayerAttack>().TakeDamage(meleeDamage);
                    print("Attack");
                }

                //print(result.collider.GetComponent<PlayerAttack>());
                timeAttack = 0;
            }


        }



    }
    void Ranged()
    {

        if (Vector2.Distance(transform.position, playerTarget.position) <= retreatRange)
        {
            agent.SetDestination(backWard.position);
            RangedAttack();


        }
        else if (agent.remainingDistance <= attackingRange)
        {
            agent.SetDestination(playerTarget.position);
            RangedAttack();






        }
        else
        {
         
            
            agent.SetDestination(playerTarget.position);

        }




    }

    void RangedAttack()
    {

        Vector3 rotation = playerTarget.position - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        firePoint.rotation = Quaternion.Euler(0, 0, rotZ);
        timeAttack += Time.deltaTime;

        if (timeAttack >= attackRate)
        {

            GameObject thisInstance = Instantiate(projectile, firePoint.position, firePoint.rotation);
            thisInstance.GetComponent<Ball>().isEnemy = true;
            timeAttack = 0;
        }

    }

    public void TakeDamage(int damage, Vector3 pos)
    {

        health -= damage;

        if(health <= 0)
        {

            Destroy(gameObject);

        }

        Vector2 difference = (transform.position - pos).normalized * 1;
        rb.velocity = difference * 5;
        agent.enabled = false;
        Invoke("EndKnockBack", 0.5f);


    }

    void EndKnockBack()
    {
        rb.velocity = Vector3.zero;
        agent.enabled = true;
    }
}
