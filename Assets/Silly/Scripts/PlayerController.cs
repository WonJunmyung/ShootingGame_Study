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
            bool isWalk = false;
            // ��
            if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow)))
            {
                charController.Move(Vector3.Normalize(this.transform.forward - (this.transform.right)) * Time.deltaTime);
                isWalk = true;
            }
            // ��
            else if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow)))
            {
                charController.Move(Vector3.Normalize(this.transform.forward - (-this.transform.right)) * Time.deltaTime);
                isWalk = true;
            }
            // ��
            else if ((Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow)))
            {
                charController.Move(Vector3.Normalize(-this.transform.forward - (this.transform.right)) * Time.deltaTime);
                isWalk = true;
            }
            // ��
            else if ((Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow)))
            {
                charController.Move(Vector3.Normalize(-this.transform.forward - (-this.transform.right)) * Time.deltaTime);
                isWalk = true;
            }
            // ��
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                charController.Move(this.transform.forward * Time.deltaTime);
                isWalk = true;
            }
            // ��
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                charController.Move(-this.transform.forward * Time.deltaTime);
                isWalk = true;
            }
            // ��
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                charController.Move(-this.transform.right * Time.deltaTime);
                isWalk = true;
            }
            // ��
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

            // ����� ������.
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            // ���� ���̸� �޾ƿ� ����
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
            ///////////////////////////// Ray�� ���� ĳ������ �ٶ󺸴� ���� ���ϱ� /////////////////////////////////////////
            // ���콺 ��ǥ�� ī�޶�κ����� ���̸� ��ȯ�Ѵ�.
            //Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            //// ����� ������.
            //Plane plane = new Plane(Vector3.up, Vector3.zero);
            //// ���� ���̸� �޾ƿ� ����
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

            /////////////////////////////// ��ġ ���� ���� ĳ������ �ٶ󺸴� ���� ���ϱ� ////////////////////////////////////
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
