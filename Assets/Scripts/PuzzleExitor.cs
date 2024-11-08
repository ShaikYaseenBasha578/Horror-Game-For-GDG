using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleExitor : MonoBehaviour
{
    // Method to be called when the button is clicked
    public void ExitToGameScene()
    {
        // Replace "GameScene" with the actual name of your game scene
        SceneManager.LoadScene("Game");
    }
}

