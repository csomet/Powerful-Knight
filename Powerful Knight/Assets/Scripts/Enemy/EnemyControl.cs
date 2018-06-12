using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour {


    public enum EnemyState{

        IDLE,
        RUN,
        WALK,
        PAUSE,
        DEATH,
        GOBACK,
        ATTACK

    }


    private float attackDistance = 1.9f;
    private float attackAlertDistance = 8f;
    private float followDistance = 12f;

    private float enemyToPlayerDistance;

    [HideInInspector]
    public EnemyState enemyCurrentState = EnemyState.IDLE;

    private EnemyState enemyLastState = EnemyState.IDLE;
    private Transform playerTarget;
    private Vector3 initialPosition;
    private float speedWalk = 1f;
    private float speedRun = 2f;
    private CharacterController characterController;
    private Vector3 whereToMove = Vector3.zero;
    private float currentTime;
    private float waitTime = 1f;
    private Animator animator;
    private bool finishedAnimation = true;
    private bool finishedMovement = true;
    private NavMeshAgent navAgent;
    private Vector3 whereToNav;




	void Awake () {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
        whereToNav = transform.position;

	}
	

	void Update () {

        //if health = 0

        if (enemyCurrentState != EnemyState.DEATH){

            enemyCurrentState = SetEnemyState(enemyCurrentState, enemyLastState, enemyToPlayerDistance);

           
            if(finishedMovement){
                GetEnemyCurrentState(enemyCurrentState);

            }else {
                if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("idle")){
                    finishedMovement = true;
                }else if (!animator.IsInTransition(0) 
                          && animator.GetCurrentAnimatorStateInfo(0).IsTag("Atk1") || animator.GetCurrentAnimatorStateInfo(0).IsTag("Atk2")){

                    animator.SetInteger("Atk", 0);
                }
            }

        }else {
            animator.SetBool("Death", true);
            characterController.enabled = false;
            navAgent.enabled = false;

            if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Death")
                && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f ){

                Destroy(gameObject, 2f);
                
            }
        }
		
	}


    private EnemyState SetEnemyState(EnemyState current, EnemyState last, float enemyToPlayerDistance){

        enemyToPlayerDistance = Vector3.Distance(transform.position, playerTarget.position);
        float initialDistance = Vector3.Distance(initialPosition, transform.position);

        if (initialDistance >= followDistance) {
            last = current;
            current = EnemyState.GOBACK;
        }else if (enemyToPlayerDistance <= attackDistance){
            last = current;
            current = EnemyState.ATTACK;
            
        } else if(enemyToPlayerDistance >= attackAlertDistance && last == EnemyState.PAUSE || last == EnemyState.ATTACK){
            last = current;
            current = EnemyState.PAUSE;

        } else if(enemyToPlayerDistance <= attackAlertDistance && enemyToPlayerDistance > attackDistance){
            if(current != EnemyState.GOBACK || current == EnemyState.WALK){
                last = current;
                current = EnemyState.PAUSE;
            }

        } else if (enemyToPlayerDistance > attackAlertDistance && last != EnemyState.GOBACK && last != EnemyState.PAUSE){
            last = current;
            current = EnemyState.WALK;
        }

        return current;
       
    }

    void GetEnemyCurrentState(EnemyState current){

        if (current == EnemyState.RUN || current == EnemyState.PAUSE){

            if (current != EnemyState.ATTACK){
                Vector3 target = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);

                if (Vector3.Distance(transform.position, target) >= 2f){
                    animator.SetBool("Run", true);
                    animator.SetBool("Walk", false);

                    navAgent.SetDestination(target);
                }
            }

        } else if (current == EnemyState.ATTACK){
            
            animator.SetBool("Run", false);
            whereToMove.Set(0f, 0f, 0f);
            //we don't to move anymore so we use transform.position (current position)
            navAgent.SetDestination(transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(playerTarget.position - transform.position), 4f * Time.deltaTime);

            if (currentTime >= waitTime){
                int atk = Random.Range(1, 3);
                animator.SetInteger("Atk", atk);
                finishedAnimation = false;
                currentTime = 0f;
            }else {
                animator.SetInteger("Atk", 0);
                currentTime += Time.deltaTime;
            }

        } else if(current == EnemyState.GOBACK){

            animator.SetBool("Run", true);

            Vector3 target = new Vector3(initialPosition.x, transform.position.y, initialPosition.z);
            navAgent.SetDestination(target);

            if(Vector3.Distance(target, initialPosition) >= 3.5f){
                enemyLastState = enemyCurrentState;
                enemyCurrentState = EnemyState.WALK;
            }


        }else if (current == EnemyState.WALK){
            animator.SetBool("Run", false);
            animator.SetBool("Walk", true);

            if (Vector3.Distance(transform.position, whereToNav) <= 2f){
                whereToNav.x = Random.Range(initialPosition.x - 5f, initialPosition.x + 5f);
                whereToNav.z = Random.Range(initialPosition.z - 5f, initialPosition.z + 5f);
            } else{
                navAgent.SetDestination(whereToNav);
            }

        } else {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            whereToNav.Set(0f, 0f, 0f);
            navAgent.isStopped = true;


        }
    }
}
