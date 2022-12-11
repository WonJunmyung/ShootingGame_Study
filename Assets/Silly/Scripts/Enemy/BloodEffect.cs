using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class BloodEffect : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Destroy(this.gameObject, 1.0f);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
