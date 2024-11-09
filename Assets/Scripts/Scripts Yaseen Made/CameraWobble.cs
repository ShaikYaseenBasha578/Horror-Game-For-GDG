using UnityEngine;

public class CameraWobble : MonoBehaviour
{
    public float wobbleIntensity = 0.1f; // Intensity of the wobble
    public float wobbleSpeed = 2.0f;     // Speed of the wobble
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            float wobbleOffset = Mathf.Sin(Time.time * wobbleSpeed) * wobbleIntensity;
            Vector3 wobblePosition = new Vector3(initialPosition.x, initialPosition.y + wobbleOffset, initialPosition.z);
            Quaternion wobbleRotation = initialRotation * Quaternion.Euler(0, 0, wobbleOffset * 10);

            transform.localPosition = wobblePosition;
            transform.localRotation = wobbleRotation;
        }
        else
        {
            transform.localPosition = initialPosition;
            transform.localRotation = initialRotation;
        }
    }
}
