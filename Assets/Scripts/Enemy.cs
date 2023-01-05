using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Transform player;
    Animator animator;
    public bool isAttackCheck = false;
    int maxHp = 2;
    int hp = 2;
    bool isStop = false;
    Renderer[] renderers;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        navMeshAgent.destination = player.position;
        renderers = this.GetComponentsInChildren<Renderer>();
        Debug.Log(renderers.Length);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isStop)
        {
            return;
        }
        if(!navMeshAgent.isStopped)
        {
            if (Vector3.Distance(this.transform.position, player.position) < navMeshAgent.stoppingDistance + 0.1f)
            {
                navMeshAgent.isStopped = true;
                StartCoroutine("Attack");
            }
            else
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.destination = player.position;
            }
        }

        this.transform.LookAt(player.position);

    }

    IEnumerator Attack()
    {

        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("isAttack");
        isAttackCheck= true;

        yield return new WaitForSeconds(0.5f);
        isAttackCheck = false;
        if (Vector3.Distance(this.transform.position, player.position) < navMeshAgent.stoppingDistance + 0.1f)
        {
                StartCoroutine("Attack");
        }
        else
        {
            navMeshAgent.isStopped = false;
        }
    }
    public void SetHp(int damage)
    {
        if (!isStop)
        {
            hp -= damage;
            if (hp <= 0)
            {
                hp = 0;
                animator.SetTrigger("Death");
                isAttackCheck = false;
                isStop = true;
                navMeshAgent.isStopped = true;
            }
            else
            {
                foreach(Renderer render in renderers)
                {
                    render.material.color = render.material.color * hp / maxHp;
                }
            }
        }
    }

    
}
