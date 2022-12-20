using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController characterController;
    public float speed = 2.0f;
    Animator animator;
    bool isWalk = false;

    // Start is called before the first frame update
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        Attack();
    }

    void Walk()
    {
        isWalk = false;

        if (Input.GetKey(KeyCode.W)) {
            characterController.Move(this.transform.forward * Time.deltaTime * speed);
            isWalk = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            characterController.Move(-this.transform.forward * Time.deltaTime * speed);
            isWalk = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            characterController.Move(-this.transform.right * Time.deltaTime * speed);
            isWalk = true;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            characterController.Move(this.transform.right * Time.deltaTime * speed);
            isWalk = true;
        }

        if (isWalk)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
        
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("isAttack");
        }
    }
}
