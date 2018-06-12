using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyControlSimplified : MonoBehaviour {

    //Attributes
    [SerializeField]
    private Transform[] walkPoint;
    private int walkIndex = 0;
    private Animator animator;
    private Transform playerTarget;
    private NavMeshAgent navMeshAgent;
    private CharacterController characterController;
    [SerializeField]
    private float attackDistance = 2f;
    [SerializeField]
    private float alertDistance = 8f;
    private float currentAttackTime;
    [SerializeField]
    private float waitAttackTime = 1f;

    public float health = 100f;
    private Image healthImg;

    //We need it to see when player is dead.
    private PlayerHealth playerHealth;

    //Distance to player
    private float distance;

    //Initialization
	void Awake () {

        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        healthImg = GameObject.Find("Health FG").GetComponent<Image>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

	}
	

	void Update () {

        //let's always see what's current distances between player and Enemy continuously.
         distance = Vector3.Distance(transform.position, playerTarget.position);
      
        if(health > 0){

            if (distance > alertDistance)
                WalkAround();
            else{
                if (playerHealth.health > 0)
                    AlertPlayerSeen();
                else{
                    Idle();
                }
                   
            }
               
            
        } else 
            Death();

	}


    /*
     * Enemy stop chasing player when he dies. Enemy goes for idle status.
     */
    private void Idle(){

        navMeshAgent.isStopped = true;
        animator.SetBool("Walk", false);
        animator.SetInteger("Atk", 0);
        animator.SetBool("Run", false);
        animator.SetBool("Idle", true);
        characterController.enabled = false;
    }

    /* We don't see the player near yet.
     */
    private void WalkAround() {
      
        //This is to be sure you reach destination and then pick up the next destination.
        //Otherwise enemy would be stuck in one place.
        if (navMeshAgent.remainingDistance <= 0.5)
        {

            navMeshAgent.isStopped = false;
            animator.SetBool("Walk", true);
            animator.SetBool("Run", false);


            navMeshAgent.SetDestination(walkPoint[walkIndex].position);


            if (walkIndex == walkPoint.Length - 1)
                walkIndex = 0;
            else
                walkIndex++;
            
        }
    }



    /* The player has been seen by the enemy while player is alive.
     */
    private void AlertPlayerSeen(){


        if (distance > attackDistance)
        {

            navMeshAgent.isStopped = false;
            animator.SetBool("Walk", false);
            animator.SetBool("Run", true);

            //Lets go for the player:
            navMeshAgent.SetDestination(playerTarget.position);

        } else
            AttackPlayer();
    }



    /* Lets attack the player
     */
    private void AttackPlayer(){

        navMeshAgent.isStopped = true;
        animator.SetBool("Run", false);

        Vector3 target = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);

        transform.rotation = Quaternion.Slerp(transform.rotation,
                                              Quaternion.LookRotation(target - transform.position), 4f * Time.deltaTime);

        //Let's wait for each attack and avoid attacking continuously.
        if (currentAttackTime >= waitAttackTime)
        {

            int atkIndex = Random.Range(1, 3);
            animator.SetInteger("Atk", atkIndex);

        }
        else
        {
            animator.SetInteger("Atk", 0);
            currentAttackTime += Time.deltaTime;

        }


    }


    /*
     * Player's death
     */
    private void Death() {

        characterController.enabled = false;
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;

        //deactivate health bar
        GameObject.Find("Enemy Health Bar").GetComponent<Canvas>().enabled = false;

        animator.SetBool("Run", false);
        animator.SetBool("Walk", false);
        animator.SetInteger("Atk", 0);

        animator.SetBool("Death", true);

        //We don't wanna execute this script anymore. 
        enabled = false;

    }



    public void ReceiveDamage(float damage){

        health -= damage;

        healthImg.fillAmount = health / 100f;
    }

}
