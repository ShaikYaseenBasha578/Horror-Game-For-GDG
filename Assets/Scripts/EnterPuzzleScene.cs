using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterPuzzleScene : MonoBehaviour
{
    // This method will be called when the object is clicked
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Puzzle");  // Replace "Puzzle" with the actual name of your puzzle scene
    }
}
