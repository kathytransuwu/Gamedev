using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform Player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    [Header("Aggro Settings")]
    public float aggroRange = 10f;
    public float stopDistance = 1.5f;
    public float rotationSpeed = 10f;

    [Header("Attack Settings")]
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    private float _attackTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.stoppingDistance = stopDistance; //Set stopping distance to the value defined in the inspector
        navMeshAgent.updateRotation = false; //We will handle rotation manually in FacePlayer method
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate distance to player
        float distance = Vector3.Distance(transform.position, Player.position);
        bool playerSpotted = distance < aggroRange; //If player distance is less than aggro range, we have spotted the player
        bool inAttackRange = distance < attackRange; //If player distance is less than attack range then we are in attack range

        animator.SetBool("PlayerNearby", playerSpotted); 
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);


        _attackTimer -= Time.deltaTime; //Reduce attack timer by time since last frame
        if(inAttackRange && _attackTimer <= 0f)
        {
            animator.SetBool("IsPunching", true); //Trigger punch animation
            _attackTimer = attackCooldown; //Reset attack timer
        }
        else
        {
            animator.SetBool("IsPunching", false); //Stop punch animation if not in attack range
        }
        //If playerSpotted is true
        if (playerSpotted)
        {

            FacePlayer(); //Rotate to face player
            //Then checks if player is out of attack range, if so then it moves towards player
            if (distance > stopDistance)
            {
                navMeshAgent.SetDestination(Player.position);
            }
            else
            {
                navMeshAgent.ResetPath(); //Stop moving when in attack range
            }
        }
        else
        {
            navMeshAgent.ResetPath(); //Stop moving if player is not spotted
        }
    }
    void FacePlayer()
    {
        Vector3 direction = Player.position - transform.position; //Calculate direction to player
        direction.y = 0; //Keep only horizontal direction
        if (direction != Vector3.zero) //If direction is not zero, rotate towards player
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation *= Quaternion.Euler(0, 90f, 0); //Rotate 90 degrees to face player with the correct side
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed); //Smoothly rotate towards player
        }
    }
}
