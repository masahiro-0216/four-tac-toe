using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;

    void Update()
    {
        float x = Input.GetAxis("Horizontal"); // A/D
        float z = Input.GetAxis("Vertical");   // W/S

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }
}

