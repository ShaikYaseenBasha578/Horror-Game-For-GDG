using UnityEngine;
using UnityEngine.AI;

public class GhostChase : MonoBehaviour
{
    public float chaseDuration = 5f;
    private Transform player;
    private NavMeshAgent agent;
    private float chaseTimer;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        chaseTimer = chaseDuration;
    }

    void Update()
    {
        if (chaseTimer > 0 && player != null)
        {
            // Follow the player
            agent.SetDestination(player.position);

            // Raycast to ensure the ghost stays on the floor
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 10f))
            {
                // Adjust ghost's position if it's not on the floor
                transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            }

            chaseTimer -= Time.deltaTime;
        }
    }
}

