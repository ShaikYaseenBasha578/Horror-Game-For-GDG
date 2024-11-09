using UnityEngine;

public class ReadNotes : MonoBehaviour
{
    public GameObject player;
    public GameObject noteUI;
    public GameObject hud;
    public GameObject inv;
    public GameObject pickUpText;
    public GameObject handUI;  // Reference to hand UI
    public GameObject crosshair;  // Reference to crosshair

    public AudioSource pickUpSound;

    private bool inReach;

    void Start()
    {
        noteUI.SetActive(false);
        hud.SetActive(true);
        inv.SetActive(true);
        pickUpText.SetActive(false);
        handUI.SetActive(false);  // Hand UI is initially hidden
        crosshair.SetActive(true);  // Crosshair is initially visible

        inReach = false;

        // Make sure the cursor is locked and hidden at the start
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            pickUpText.SetActive(true);
            handUI.SetActive(true);  // Show hand UI when near the note
            crosshair.SetActive(false);  // Hide crosshair when near the note
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            pickUpText.SetActive(false);
            handUI.SetActive(false);  // Hide hand UI when no longer near the note
            crosshair.SetActive(true);  // Show crosshair when not near the note
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact") && inReach)
        {
            noteUI.SetActive(true);
            pickUpSound.Play();
            hud.SetActive(false);
            inv.SetActive(false);
            player.GetComponent<PlayerController>().enabled = false;

            // Show the cursor when reading the note
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ExitButton()
    {
        noteUI.SetActive(false);
        hud.SetActive(true);
        inv.SetActive(true);
        player.GetComponent<PlayerController>().enabled = true;

        // Hide the cursor when exiting the note screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
