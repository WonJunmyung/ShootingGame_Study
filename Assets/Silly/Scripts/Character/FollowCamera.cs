using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class FollowCamera : MonoBehaviour
    {
        public Transform target;
        
        public float smoothSpeed = 0.125f;
        public Vector3 offset;
        public Vector3 origin;
        public Vector3 offsetUp;
        private float timer = 3.0f;
        private float countTime = 0;


        //private Transform tr;
        Ray ray;

        //[Header("Wall Obstacle Setting")]
        


        // Start is called before the first frame update
        void Start()
        {
            origin = offset;
        }

        // Update is called once per frame
        void Update()
        {
            countTime += Time.deltaTime;

            if(countTime > timer)
            {
                timerUpdate();
                countTime = 0;
                
            }
        }

        private void LateUpdate()
        {
            Vector3 desiredPos = target.position + offset;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);

            this.transform.position = smoothedPos;

            this.transform.LookAt(target);


        }

        private void timerUpdate()
        {
            ray = new Ray();
            ray.origin = this.transform.position;
            ray.direction = this.transform.forward;


            RaycastHit raycastHit;

            if (Physics.Raycast(ray.origin, ray.direction, out raycastHit, Vector3.Distance(target.position, this.transform.position)))
            {
                if (raycastHit.collider.gameObject.name.Contains("Cube"))
                {
                    offset = offsetUp;
                }
                else
                {
                    offset = origin;
                }
            }
            
        }

        


        //private void OnDrawGizmos()
        //{
        //    ray = new Ray();
        //    ray.origin = this.transform.position;
        //    ray.direction = this.transform.forward;

        //    Debug.DrawRay(ray.origin, ray.direction * Vector3.Distance(target.position, this.transform.position), Color.red);
        //}
    }
}
