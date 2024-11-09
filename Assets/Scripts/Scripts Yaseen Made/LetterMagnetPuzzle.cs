using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LetterMagnetPuzzle : MonoBehaviour
{
    public GameObject[] magnets;          // Array of magnet letters
    public Transform[] magnetPositions;   // Correct positions for the letters (in order)
    public string correctWord = "cabinet";  // The word to spell
    public float positionTolerance = 0.5f; // Tolerance for position check
    public float shakeMagnitude = 0.05f;    // Shake intensity
    public float shakeDuration = 0.5f;      // Shake duration

    private bool puzzleCompleted = false;

    void Start()
    {
        // Ensure magnets are active at the start
        foreach (GameObject magnet in magnets)
        {
            magnet.SetActive(true);
        }
    }

    // Called when player clicks the fridge to enter the puzzle scene
    public void OnFridgeClick()
    {
        SceneManager.LoadScene("Puzzle"); // Load the puzzle scene
    }

    // Check if magnets are in the correct positions
    public void CheckPuzzleCompletion()
    {
        string currentWord = "";

        for (int i = 0; i < magnets.Length; i++)
        {
            // Check if magnet is close enough to correct position
            if (Vector3.Distance(magnets[i].transform.position, magnetPositions[i].position) < positionTolerance)
            {
                currentWord += magnets[i].name; // Add magnet's letter to current word
            }
            else
            {
                // Shake magnet if not in the correct position
                StartCoroutine(ShakeMagnet(magnets[i]));
            }
        }

        // Complete puzzle if the current word matches the target word
        if (currentWord == correctWord && !puzzleCompleted)
        {
            puzzleCompleted = true;
            Debug.Log("Puzzle completed!");
            SceneManager.LoadScene("Game"); // Load the next scene
        }
    }

    // Shake effect for misplaced magnets
    IEnumerator ShakeMagnet(GameObject magnet)
    {
        Vector3 originalPosition = magnet.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float shakeAmount = Mathf.Sin(elapsedTime * 10) * shakeMagnitude;
            magnet.transform.position = originalPosition + new Vector3(shakeAmount, 0, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        magnet.transform.position = originalPosition; // Reset position
    }
}
