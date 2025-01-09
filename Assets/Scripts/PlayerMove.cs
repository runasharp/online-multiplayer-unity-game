using UnityEngine;
using Mirror; // Only needed if you're using Mirror for multiplayer

public class PlayerMove : NetworkBehaviour // Use NetworkBehaviour if using Mirror; otherwise, MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player's movement

    void Update()
    {
        // Only allow the local player to control movement (for multiplayer with Mirror)
        if (!isLocalPlayer)
            return;

        // Get input for movement
        float moveX = Input.GetAxisRaw("Horizontal"); // Left (-1), Right (+1)
        float moveY = Input.GetAxisRaw("Vertical");   // Down (-1), Up (+1)

        // Create a movement vector based on input and speed
        Vector3 movement = new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;

        // Apply the movement to the Transform's position
        transform.position += movement;
    }
}