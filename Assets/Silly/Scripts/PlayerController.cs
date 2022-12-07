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
            bool isWalk = false;
            // ↖
            if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow)))
            {
                charController.Move(Vector3.Normalize(this.transform.forward - (this.transform.right)) * Time.deltaTime);
                isWalk = true;
            }
            // ↗
            else if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow)))
            {
                charController.Move(Vector3.Normalize(this.transform.forward - (-this.transform.right)) * Time.deltaTime);
                isWalk = true;
            }
            // ↙
            else if ((Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow)))
            {
                charController.Move(Vector3.Normalize(-this.transform.forward - (this.transform.right)) * Time.deltaTime);
                isWalk = true;
            }
            // ↘
            else if ((Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow)))
            {
                charController.Move(Vector3.Normalize(-this.transform.forward - (-this.transform.right)) * Time.deltaTime);
                isWalk = true;
            }
            // ↑
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                charController.Move(this.transform.forward * Time.deltaTime);
                isWalk = true;
            }
            // ↓
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                charController.Move(-this.transform.forward * Time.deltaTime);
                isWalk = true;
            }
            // ←
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                charController.Move(-this.transform.right * Time.deltaTime);
                isWalk = true;
            }
            // →
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                charController.Move(this.transform.right * Time.deltaTime);
                isWalk = true;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.SetTrigger("isShoot");
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

        Vector3 LookPos()
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 평면을 만들어낸다.
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            // 레이 길이를 받아올 변수
            float rayLength;
            Vector3 look = Vector3.zero;
            // 
            if (plane.Raycast(cameraRay, out rayLength))
            {
                look = cameraRay.GetPoint(rayLength);

                this.transform.LookAt(new Vector3(look.x, this.transform.position.y, look.z));
                
            }
            return look;
        }

        void Rotation()
        {
            ///////////////////////////// Ray를 통한 캐릭터의 바라보는 방향 구하기 /////////////////////////////////////////
            // 마우스 좌표를 카메라로부터의 레이를 반환한다.
            //Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            //// 평면을 만들어낸다.
            //Plane plane = new Plane(Vector3.up, Vector3.zero);
            //// 레이 길이를 받아올 변수
            //float rayLength;
            //// 
            //if (plane.Raycast(cameraRay, out rayLength))
            //{
            //    Vector3 look = cameraRay.GetPoint(rayLength);

            //    this.transform.LookAt(new Vector3(look.x, this.transform.position.y, look.z));
            //}
            /////////////////////////////////////////////////////////////////////////////////////////////////
            ///
            Vector3 lookPos = LookPos();
            if(lookPos != Vector3.zero)
            {
                this.transform.LookAt(new Vector3(lookPos.x, this.transform.position.y, lookPos.z));
            }

            /////////////////////////////// 위치 값을 통한 캐릭터의 바라보는 방향 구하기 ////////////////////////////////////
            //Vector3 cameraToMousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z);
            //Vector3 worldMousePoint = Camera.main.ScreenToWorldPoint(cameraToMousePos);

            //worldMousePoint.y = this.transform.position.y;

            //Vector3 dir = worldMousePoint - this.transform.position;

            //Quaternion rot = Quaternion.LookRotation(dir.normalized);

            //this.transform.rotation = rot;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
    }
}
