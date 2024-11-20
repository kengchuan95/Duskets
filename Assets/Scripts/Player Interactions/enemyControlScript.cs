using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyControlScript : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    public GameObject enemy;

    [SerializeField]
    private Target enemyValues;
    [SerializeField]
    private Transform player;
    public LayerMask isGround, isPlayer;
    //patrol
    [SerializeField]
    private Vector3 walkPoint;
    [SerializeField]
    private bool walkPointSet;
    [SerializeField]
    private float walkPointRange;
    //attack
    public float timeBetweenAttacks;
    [SerializeField]
    private bool hasAttacked;
    [SerializeField]
    private EnemyWeaponScript enemyWeapon;
    //states
    public float sightRange, attackRange;
    [SerializeField]
    private float currentSightRange;
    [SerializeField]
    private bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemyWeapon = GetComponent<EnemyWeaponScript>();
        enemyValues = GetComponent<Target>();
    }
    // Start is called before the first frame update
    private void Update()
    {
        if (enemyValues.currentHealth < enemyValues.maxHealth)
            currentSightRange = 3 * sightRange;
        else
            currentSightRange = sightRange;
        playerInSightRange = Physics.CheckSphere(transform.position, currentSightRange, isPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);
        if (!playerInSightRange && !playerInAttackRange) Patrol();
        if (playerInSightRange && !playerInAttackRange) Chase();
        if (playerInSightRange && playerInAttackRange) Attack();
    }
    private void Patrol()
    {
        agent.speed = 2.5f;
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);
        Vector3 distanceToPoint = transform.position - walkPoint;
        if (distanceToPoint.magnitude < 1f)
            walkPointSet = false;
    }
    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, isGround))
        {
            walkPointSet = true;
        }
    }
    private void Attack()
    {
        //walk
        agent.speed = 1f;
        transform.LookAt(player);
        if (!hasAttacked)
        {
            enemyWeapon.ShootAtPlayer(attackRange, isPlayer);
            hasAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        hasAttacked = false;
    }
    private void Chase()
    {
        agent.speed = 4.5f;
        agent.SetDestination(player.position);
    }
}
