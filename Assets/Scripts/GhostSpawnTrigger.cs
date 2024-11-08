using UnityEngine;

public class GhostSpawnTrigger : MonoBehaviour
{
    public GameObject ghostPrefab; // Assign the ghost prefab in the inspector
    public Transform spawnPoint;   // Assign a specific spawn location for the ghost

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assumes the player has a "Player" tag
        {
            Instantiate(ghostPrefab, spawnPoint.position, spawnPoint.rotation);
            Destroy(gameObject); // Optionally, remove the trigger after spawning
        }
    }
}
