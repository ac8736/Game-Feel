using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour
{
    public float shakeDuration = 0.5f; // Duration of the screen shake
    public float shakeIntensity = 0.1f; // Intensity of the screen shake

    private Vector3 originalPosition; // Original position of the camera

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Damage")) // Assuming the collider is attached to the player
        {
            originalPosition = Camera.main.transform.position; // Store the original position of the camera
            StartCoroutine(Shake());
        }
    }
     
    IEnumerator Shake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            // Generate random offset for the camera position within a range
            Vector3 randomOffset = Random.insideUnitSphere * shakeIntensity;

            // Apply the random offset to the camera position
            Camera.main.transform.position = originalPosition + randomOffset;

            // Increment time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Reset the camera position to its original position after the shake
        Camera.main.transform.position = originalPosition;
    }
}
