using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // The player object the camera will follow
    public float smoothSpeed = 0.125f; // The speed at which the camera follows
    public Vector3 offset; // Offset to position the camera at a certain distance behind the player

    private void LateUpdate()
    {
        // Define the desired position of the camera based on the player's position and the offset
        Vector3 desiredPosition = player.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera's position to the smoothed position
        transform.position = smoothedPosition;
    }
}

