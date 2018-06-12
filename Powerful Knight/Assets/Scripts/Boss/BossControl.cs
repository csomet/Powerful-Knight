using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossControl : MonoBehaviour {


    private Transform playerTarget;
    private BossStateChecker stateChecker;
    private CharacterController characterController;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool finishedAttacking = false;
    private float currentAttackTime;
    [SerializeField]
    private float waitAttackTime = 1f;
    [SerializeField]
    private AudioClip[] attackSounds;
    [SerializeField]
    private AudioClip[] idleSounds;
    private AudioSource audioSource;
    private Coroutine soundIdleCoroutine;


    private void Start()
    {
        soundIdleCoroutine = StartCoroutine(PlayIdleSound());
    }

    void Awake () {

        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        stateChecker = GetComponent<BossStateChecker>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
	}
	


	void Update () {
		


        if (finishedAttacking){

            GetStateControl();


        } else {
            animator.SetInteger("GetHit", 0);
            animator.SetInteger("Attack", 0);

            if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")){
                finishedAttacking = true;
            }

        }
	}



    void GetStateControl(){



        if (stateChecker.bossStateInfo == BossState.DEATH){
            animator.SetInteger("GetHit", 0);
            navMeshAgent.isStopped = true;
            navMeshAgent.enabled = false;
            characterController.enabled = false;
            animator.SetBool("Dead", true);
            StopAllCoroutines();
            enabled = false;

        } else {

            if (stateChecker.bossStateInfo == BossState.PAUSE){
                
                navMeshAgent.isStopped = false;
                animator.SetBool("Walk", true);
                navMeshAgent.SetDestination(playerTarget.position);
            
            } else if (stateChecker.bossStateInfo == BossState.ATTACK){
                
                animator.SetBool("Walk", false);
                Vector3 target = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);

                transform.rotation = Quaternion.Slerp(transform.rotation, 
                                                      Quaternion.LookRotation(target - transform.position), 4f * Time.deltaTime);

                if (currentAttackTime >= waitAttackTime){
                    int atk = Random.Range(1, 6);

                    //play sound when attacking
                    SoundAttack(atk);

                    animator.SetInteger("Attack", atk);
                    currentAttackTime = 0f;
                    finishedAttacking = false;

                } else {
                    animator.SetInteger("Attack", 0);
                    currentAttackTime += Time.deltaTime;
                }

            } else {
                animator.SetBool("Walk", false);
                animator.SetBool("Run", false);
                navMeshAgent.isStopped = true;
                animator.SetInteger("Attack", 0);
                PlayIdleSound();
            }

        }




    }


    void SoundAttack(int attack){

        switch (attack)
        {
            case 1:
                audioSource.clip = attackSounds[0];
                audioSource.Play();
                break;
            case 2:
                audioSource.clip = attackSounds[1];
                audioSource.Play();
                break;
            case 3:
                audioSource.clip = attackSounds[2];
                audioSource.Play();
                break;
            case 4:
                audioSource.clip = attackSounds[3];
                audioSource.Play();
                break;
            case 5:
                audioSource.clip = attackSounds[4];
                audioSource.Play();
                break;
            case 6:
                audioSource.clip = attackSounds[5];
                audioSource.Play();
                break;
        }
    }


    IEnumerator PlayIdleSound()
    {
        yield return new WaitForSeconds(Random.Range(5f, 15f));

        int index = Random.Range(0, idleSounds.Length);
        audioSource.clip = idleSounds[index];
        audioSource.Play();

        StartCoroutine(PlayIdleSound());
    }
}
