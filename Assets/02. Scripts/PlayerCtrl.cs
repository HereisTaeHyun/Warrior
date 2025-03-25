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

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        transform = GetComponent<Transform>();
    }
    void Update()
    {
        if(moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
            transform.Translate(Vector3.forward * Time.deltaTime * 4.0f);
        }
    }
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
}
