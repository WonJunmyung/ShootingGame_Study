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

        private Transform tr;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void LateUpdate()
        {
            Vector3 desiredPos = target.position + offset;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);

            this.transform.position = smoothedPos;

            this.transform.LookAt(target);
        }
    }
}
