using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class TerrainControl : MonoBehaviour
    {
        public GameObject enemy;
        public List<Transform> response;
        // Start is called before the first frame update
        void Start()
        {
            response = new List<Transform>();
            for(int i = 0; i < this.transform.childCount; i++)
            {
                if (this.transform.GetChild(i).name.Contains("Floor_response"))
                {
                    response.Add(this.transform.GetChild(i));
                }
            }

            for(int i=0; i < response.Count; i++)
            {
                Instantiate(enemy, response[i].position, enemy.transform.rotation);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
