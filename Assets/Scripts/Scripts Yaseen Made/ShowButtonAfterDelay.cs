using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowButtonAfterDelay : MonoBehaviour
{
    public Button puzzleButton;  // Reference to the Button component
    public float delay = 5f;     // Delay in seconds

    void Start()
    {
        // Hide the button initially
        puzzleButton.gameObject.SetActive(false);

        // Start the coroutine to show the button after a delay
        StartCoroutine(ShowButton());
    }

    IEnumerator ShowButton()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Show the button
        puzzleButton.gameObject.SetActive(true);
    }
}
