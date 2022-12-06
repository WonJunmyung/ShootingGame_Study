using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class PlayerController : MonoBehaviour
    {
        [Tooltip("ĳ������ �̵� �ӵ�")]
        public float speed = 2.0f;
        [Tooltip("ĳ���� ���� ��")]
        public float jumpForce = 4.0f;
        [Tooltip("ĳ���� �߷�")]
        public float gravity = 9.8f;

        

        private CharacterController charController;     // ĳ���� ��Ʈ�ѷ�(�ݶ��̴�)
        private Vector3 moveDir = Vector3.zero;                        // �̵� ����

        private Animator animator;
        private void Awake()
        {
            charController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }
        void Start()
        {
            
        }

        void Update()
        {
            Move();

            Rotation();
        }
        /// <summary>
        /// �̵��� ���� �Լ�
        /// </summary>
        void Move()
        {
            // ĳ���Ͱ� ���鿡 �ִ��� üũ
            if (charController.isGrounded)
            {
                // ����
                if (Input.GetButton("Jump"))
                {
                    moveDir.y = jumpForce;
                }
            }
            // �߷� ����
            if (moveDir.y > 0)
            {
                moveDir.y -= gravity * Time.deltaTime;
            }
            else
            {
                moveDir.y = 0;
            }
            // ĳ���� ������ ������ �޾ƿ´�.
            moveDir = new Vector3(Input.GetAxis("Horizontal"), moveDir.y, Input.GetAxis("Vertical"));

            this.transform.Translate(moveDir.normalized * speed * Time.deltaTime, Space.Self);

            // ĳ���� ���� ������
            charController.Move(moveDir * Time.deltaTime);
            //charController.



            if (moveDir != Vector3.zero)
            {
                animator.SetFloat("Speed", moveDir.magnitude);
            }
            else
            {
                animator.SetFloat("Speed", 0f);
            }
        }

        void Rotation()
        {

            // ���콺 ��ǥ�� ī�޶�κ����� ���̸� ��ȯ�Ѵ�.
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            // ����� ������.
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            // ���� ���̸� �޾ƿ� ����
            float rayLength;
            // 
            if(plane.Raycast(cameraRay, out rayLength))
            {
                Vector3 look = cameraRay.GetPoint(rayLength);

                this.transform.LookAt(new Vector3(look.x, this.transform.position.y, look.z));
            }
        }
    }
}
