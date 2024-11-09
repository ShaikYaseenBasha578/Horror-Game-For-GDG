using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private float zDistance;

    // The correct position where the magnet should be placed (set this in the Inspector)
    public Transform correctPosition;

    private void OnMouseDown()
    {
        // Calculate the distance from the camera to the object
        zDistance = Camera.main.WorldToScreenPoint(transform.position).z;
        // Calculate the offset between the mouse position and the object's position
        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        // Update the position of the magnet to follow the mouse in 3D space
        transform.position = GetMouseWorldPosition() + offset;
    }

    private void OnMouseUp()
    {
        // Check if the magnet is close enough to the correct position
        if (Vector3.Distance(transform.position, correctPosition.position) < 1.0f)
        {
            // Snap the magnet to the correct position
            transform.position = correctPosition.position;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Get the mouse position in the world
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zDistance;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
