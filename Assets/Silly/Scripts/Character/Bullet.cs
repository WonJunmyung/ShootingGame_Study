using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class Bullet : MonoBehaviour
    {
        public string poolItemName = "Bullet";
        public float speed = 10.0f;
        public float endTime = 3.0f;            // �Ѿ��� ������� �ð�
        public float lifeTime = 0f;             // �Ѿ��� ��Ÿ���� ����� �ð�
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame

        void Update()
        {
            lifeTime += Time.deltaTime;
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            
            if(lifeTime >= endTime)
            {
                lifeTime = 0f;
                ObjectPool.Instance.PushToPool(poolItemName, gameObject);
            }
        }
    }
}
