using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

public class PlayerCtrl : MonoBehaviour
{
    private Animator playerAnim;
    private new Transform transform;
    private Vector3 moveDir;

    private PlayerInput playerInput;
    private InputActionMap mainActionMap;
    private InputAction moveAction;
    private InputAction attackAction;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        playerInput = GetComponent<PlayerInput>();

        mainActionMap = playerInput.actions.FindActionMap("PlayerAction");

        moveAction = mainActionMap.FindAction("Move");
        attackAction = mainActionMap.FindAction("Attack");

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

        attackAction.performed += ctx =>
        {
            playerAnim.SetTrigger("Attack");
        };
    }
    void Update()
    {
        if(moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
            transform.Translate(Vector3.forward * Time.deltaTime * 4.0f);
        }
    }
#region SendMessage
    private void OnMove(InputValue inputValue)
    {
        Vector2 dir = inputValue.Get<Vector2>();
        moveDir = new Vector3(dir.x, 0, dir.y);
        playerAnim.SetFloat("Movement", dir.magnitude);
    }

    private void OnAttack()
    {
        playerAnim.SetTrigger("Attack");
    }
    #endregion

    #region UnityEvents
    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 dir = ctx.ReadValue<Vector2>();
        moveDir = new Vector3(dir.x, 0, dir.y);
        playerAnim.SetFloat("Movement", dir.magnitude);
    }
    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            playerAnim.SetTrigger("Attack");
        }
    }
    #endregion
}
