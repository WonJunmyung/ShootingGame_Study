using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class PlayerController : MonoBehaviour
    {
        [Tooltip("캐릭터의 이동 속도")]
        public float speed = 2.0f;
        [Tooltip("캐릭터 점프 힘")]
        public float jumpForce = 4.0f;
        [Tooltip("캐릭터 중력")]
        public float gravity = 9.8f;

        

        private CharacterController charController;     // 캐릭터 컨트롤러(콜라이더)
        private Vector3 moveDir = Vector3.zero;                        // 이동 방향

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
        /// 이동에 대한 함수
        /// </summary>
        void Move()
        {
            // 캐릭터가 지면에 있는지 체크
            if (charController.isGrounded)
            {
                // 점프
                if (Input.GetButton("Jump"))
                {
                    moveDir.y = jumpForce;
                }
            }
            // 중력 적용
            if (moveDir.y > 0)
            {
                moveDir.y -= gravity * Time.deltaTime;
            }
            else
            {
                moveDir.y = 0;
            }
            // 캐릭터 조작의 방향을 받아온다.
            moveDir = new Vector3(Input.GetAxis("Horizontal"), moveDir.y, Input.GetAxis("Vertical"));

            this.transform.Translate(moveDir.normalized * speed * Time.deltaTime, Space.Self);

            // 캐릭터 실제 움직임
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

            // 마우스 좌표를 카메라로부터의 레이를 반환한다.
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            // 평면을 만들어낸다.
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            // 레이 길이를 받아올 변수
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
