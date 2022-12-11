using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Silly
{
    public class Enemy : MonoBehaviour
    {
        Transform Player;
        NavMeshAgent agent;
        NavMeshPath path;
        private Animator animator;
        private bool isNav = true;
        public GameObject bloodEffect;

        public int totalHp = 2;
        public int hp = 2;
        public bool isLife = true;

        

        // Start is called before the first frame update
        void Start()
        {
            Player = GameObject.Find("Player").transform;
            agent = this.GetComponent<NavMeshAgent>();
            path = new NavMeshPath();
            animator = this.GetComponent<Animator>();
            animator.SetBool("isWalk", true);
        }

        // Update is called once per frame
        void Update()
        {
            if (isLife)
            {
                if (Player != null)
                {
                    agent.CalculatePath(Player.position, path);

                    agent.destination = Player.position;

                    if (isNav && (Vector3.Distance(transform.position, Player.position) < agent.stoppingDistance + 0.1f || Vector3.Distance(transform.position, Player.position) > 30.0f))
                    {
                        isNav = false;
                        agent.isStopped = true;
                        animator.SetBool("isWalk", false);
                        //transform.LookAt(Player.position);

                        StartCoroutine(Attack());
                    }
                   

                    transform.LookAt(Player.position);
                }
            }
        }

        IEnumerator Attack()
        {
            yield return new WaitForSeconds(1.0f);
            animator.SetTrigger("isAttack");

            yield return new WaitForSeconds(1.0f);

            if (Vector3.Distance(transform.position, Player.position) < agent.stoppingDistance + 0.1f)
            {
                //agent.isStopped = false;
                yield return StartCoroutine(Attack());
            }
            else
            {
                agent.isStopped = false;
                animator.SetBool("isWalk", true);
                isNav = true;
                yield return null;
            }
            
        }

        public void Hit()
        {
            if (isLife)
            {
                Instantiate(bloodEffect, transform.position + Vector3.up * 1.5f, transform.rotation);
                this.transform.position -= agent.transform.forward * 0.1f;
                hp--;

                Renderer[] enemyColor = this.GetComponentsInChildren<Renderer>();

                if (hp > 0)
                {
                    for (int i = 0; i < enemyColor.Length; i++)
                    {
                        enemyColor[i].material.color = enemyColor[i].material.color * ((float)hp / (float)totalHp);
                    }
                }
                else
                {
                    isLife = false;
                    agent.isStopped = true;
                    animator.SetTrigger("die");
                    Destroy(this.gameObject, 2.0f);

                }
            }

            
        }


    }
}
