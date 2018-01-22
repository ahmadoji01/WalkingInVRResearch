using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class CharMove : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    void Update()
    {
        RigidbodyFirstPersonController controller = GetComponent<RigidbodyFirstPersonController>();
        //CharacterController controller = GetComponent<CharacterController>();
        if (controller.Grounded)
        {
            moveDirection = new Vector2(0, 0);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            moveDirection.y = speed;
            //if (Input.GetButton("Jump"))
               //moveDirection.y = jumpSpeed;

        }
        //moveDirection.y -= gravity * Time.deltaTime;
        controller.movementSettings.UpdateDesiredTargetSpeed(moveDirection);
    }
}
