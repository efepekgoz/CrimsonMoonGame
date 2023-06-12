using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform

    private void Update()
    {
        if (player != null)
        {
            // Set the camera position to the player's position
            transform.position = new Vector3(player.position.x, player.position.y+0.3f, transform.position.z);
        }
    }
}