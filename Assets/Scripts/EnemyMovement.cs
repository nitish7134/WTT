using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    public int health = 2;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isDead = false;
    private bool isAttacking = false;
    private GameManager gameManager;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start () 
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //Health = maxHealth;
    }
  
    private void Update()
    {
        if (gameManager.isPlayerDead)
            Destroy(this.gameObject);
        if(!isDead){
            transform.LookAt(target);
            agent.SetDestination(target.position);
            if (health <= 0)
            {
                isDead = true;
                GetComponent<Animator>().SetBool("Dead",true);
                gameManager.enemyDown();
                StartCoroutine("AddDelay"); 
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && isAttacking == false)
        {   
            isAttacking = true;
            animator.SetBool("Attack", true);
            collision.gameObject.GetComponent<PlayerHandler>().GiveDamage(1);
        }
    }

    private void OnCollisionExit(Collision collision)
    {   
        if (collision.gameObject.tag == "Player")
        {
            isAttacking = false;
            animator.SetBool("Attack", false);
        }
    }

    IEnumerator AddDelay(){
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject); 
    }

}
