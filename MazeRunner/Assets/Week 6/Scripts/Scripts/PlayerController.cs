using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] InputAction moveAction;
    [SerializeField] InputAction jumpAction;
    [SerializeField] InputAction lookAction;

    PlayerControls mappings;

    Rigidbody rb;

    [SerializeField] float jumpForce = 100f;

    const float SPEED = 5.5f;


    private void Awake()
    {
        mappings = new PlayerControls();

        moveAction = mappings.Player.Move;
        jumpAction = mappings.Player.Jump;
        lookAction = mappings.Player.Jump;

        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
       
        moveAction.Enable();
        jumpAction.Enable();
        lookAction.Enable();

        jumpAction.performed += OnJump;
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        lookAction.Disable();

        jumpAction.performed -= OnJump;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 input = moveAction.ReadValue<Vector2>();
        input *= SPEED * Time.deltaTime;

        transform.position = new Vector3(transform.position.x + input.x, transform.position.y, transform.position.z + input.y);


        
    }

    void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Yipee!");

        rb.AddForce(Vector3.up * jumpForce);
    }


    bool IsGrounded()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 3;

        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up * -1), out hit, 1.1f, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up * -1) * hit.distance, Color.yellow);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            return false;
        }
    }

}
