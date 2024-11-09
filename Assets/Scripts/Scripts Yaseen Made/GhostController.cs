using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    public Transform[] waypoints;
    public float idleTime = 2f;
    public float walkSpeed = 2f;
    public float chaseSpeed = 4f;
    public float sightDistance = 10f;
    public AudioClip idleSound;
    public AudioClip walkingSound;
    public AudioClip chasingSound;
    public AudioClip screamSound;

    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;
    private Animator animator;
    private float idleTimer = 0f;
    private Transform player;
    private AudioSource audioSource;

    private enum GhostState { Idle, Walk, Scream, Chase }
    private GhostState currentState = GhostState.Idle;

    private bool hasScreamed = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        SetDestinationToWaypoint();
    }

    private void Update()
    {
        // Testing state switch with "Z" key
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CycleState();
        }

        switch (currentState)
        {
            case GhostState.Idle:
                idleTimer += Time.deltaTime;
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsChasing", false);
                PlaySound(idleSound);

                if (idleTimer >= idleTime)
                {
                    NextWaypoint();
                }

                CheckForPlayerDetection();
                break;

            case GhostState.Walk:
                idleTimer = 0f;
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsChasing", false);
                PlaySound(walkingSound);

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    currentState = GhostState.Idle;
                }

                CheckForPlayerDetection();
                break;

            case GhostState.Scream:
                animator.SetTrigger("Scream");
                PlaySound(screamSound);

                if (!audioSource.isPlaying) // Once scream is done, transition to Chase
                {
                    currentState = GhostState.Chase;
                    agent.speed = chaseSpeed;
                }
                break;

            case GhostState.Chase:
                agent.SetDestination(player.position);
                animator.SetBool("IsChasing", true);
                PlaySound(chasingSound);

                if (Vector3.Distance(transform.position, player.position) > sightDistance)
                {
                    currentState = GhostState.Walk;
                    agent.speed = walkSpeed;
                }
                break;
        }
    }

    private void CheckForPlayerDetection()
    {
        RaycastHit hit;
        Vector3 playerDirection = player.position - transform.position;

        if (Physics.Raycast(transform.position, playerDirection.normalized, out hit, sightDistance))
        {
            if (hit.collider.CompareTag("Player") && currentState != GhostState.Scream && !hasScreamed)
            {
                currentState = GhostState.Scream;
                hasScreamed = true;
                Debug.Log("Ghost spotted the player and is screaming!");
            }
        }
    }

    private void PlaySound(AudioClip soundClip)
    {
        if (!audioSource.isPlaying || audioSource.clip != soundClip)
        {
            audioSource.clip = soundClip;
            audioSource.Play();
        }
    }

    private void NextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        SetDestinationToWaypoint();
    }

    private void SetDestinationToWaypoint()
    {
        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentState = GhostState.Walk;
        agent.speed = walkSpeed;
        animator.enabled = true;
    }

    private void CycleState()
    {
        // Cycle through the states
        currentState = (GhostState)(((int)currentState + 1) % System.Enum.GetValues(typeof(GhostState)).Length);
        hasScreamed = (currentState == GhostState.Scream) ? false : hasScreamed; // Reset scream if transitioning to Scream state
        Debug.Log("Current State: " + currentState);
    }

    private void OnDrawGizmos()
    {
        if (player == null) return;

        if (currentState == GhostState.Chase)
            Gizmos.color = Color.red;
        else if (currentState == GhostState.Scream)
            Gizmos.color = Color.yellow;
        else
            Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, player.position);
    }
}

