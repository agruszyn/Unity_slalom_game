using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 movementVector;
    private Vector3 rotationVector;
    //private CharacterController characterController;
    //public CardboardHead viewPort;
    public int playerSpeed;
    public int rotationSpeedy;
    public int rotationSpeed;
    public Vector3 orientation;

    public float movementSpeed = 8;
    //private float jumpPower = 15;
    public float gravity = 40;

    void Start()
    {
        //characterController = GetComponent<CharacterController>();
        //viewPort = GetComponentInChildren<CardboardHead>();
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("pause") == 0)
        {
            //vector3.up (0,1,0)
            //vector3.left (-1,0,0)
            //vector3.right (1,0,0)
            Vector3 pos = transform.position;
            pos.z = Vector3.Dot(Input.gyro.gravity*90, Vector3.left); //small sides -> bottom down and top up is + (this is the important one)
            pos.y = Vector3.Dot(Input.gyro.gravity*90, Vector3.down); // long sides -> bottom left and top right is +
            pos.x = Vector3.Dot(Input.gyro.gravity*90, Vector3.back); // faces -> face down back up is +
            orientation = pos;
            movementVector.x = playerSpeed * (Input.GetAxis("Horizontal") * movementSpeed * Mathf.Cos(orientation.y * Mathf.PI / 180) + Input.GetAxis("Vertical") * movementSpeed * Mathf.Sin(orientation.y * Mathf.PI / 180));
            movementVector.z = playerSpeed * (Input.GetAxis("Vertical") * movementSpeed * Mathf.Cos(orientation.y * Mathf.PI / 180) - Input.GetAxis("Horizontal") * movementSpeed * Mathf.Sin(orientation.y * Mathf.PI / 180));
            rotationVector.x = Input.GetAxis("VerticalLook") * rotationSpeed;
            rotationVector.y = Input.GetAxis("HorizontalLook") * rotationSpeedy;


            //if (characterController.isGrounded)
            //{
            //    movementVector.y = 0;

            //    //if (Input.GetButtonDown("A"))
            //    //{
            //    //    movementVector.y = jumpPower;
            //    //}
            //}


            movementVector.y = 0;

            transform.position = transform.position + movementVector;
            transform.Rotate(rotationVector.x, 0, 0, relativeTo: Space.Self);
            transform.Rotate(0, rotationVector.y, 0, relativeTo: Space.World);

        }
    }
}
