using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrlByEvent : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction attackAction;

    private Animator playerAnim;
    private Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();

        moveAction = new InputAction("Move", InputActionType.Value);
        moveAction.AddCompositeBinding("2DVector")
        .With("Up", "<Keyboard>/w")
        .With("Down", "<Keyboard>/s")
        .With("Left", "<Keyboard>/a")
        .With("Right", "<Keyboard>/d");

        moveAction.performed += ctx => 
        {
            Vector2 dir = ctx.ReadValue<Vector2>();
            moveDir = new Vector3(dir.x, 0, dir.y);
            playerAnim.SetFloat("Movement", dir.magnitude);
        };

        moveAction.canceled += ctx =>
        {
            moveDir = Vector3.zero;
            playerAnim.SetFloat("Movement", 0.0f);
        };
        moveAction.Enable();

        attackAction = new InputAction("Attack", InputActionType.Button, "<Keyboard>/space");
        attackAction.performed += ctx =>
        {
            playerAnim.SetTrigger("Attack");
        };
        attackAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if(moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
            transform.Translate(Vector3.forward * Time.deltaTime * 4.0f);
        }
    }
}
