using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {


    public float damageAmount = 11f;
    private Animator animator;
    private Transform playerTarget;
    private bool finishedAttacking = true;
    private float damageDistance = 2f;

    [SerializeField]
    private AudioClip[] damageHitSounds;


    private PlayerHealth playerHealth;

	void Awake () {

        animator = GetComponent<Animator>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = playerTarget.GetComponent<PlayerHealth>();
	}
	
	
	void Update () {
		
        if(finishedAttacking){
            Damage(CheckPlayerAttacking());

        }else {
            if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")){
                finishedAttacking = true;
            }
        }
	}


    private bool CheckPlayerAttacking(){
        bool isAttacking = false;

        if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Atk1") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Atk2")){

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f){

                isAttacking = true;
                finishedAttacking = false;
            }
        }

        return isAttacking;


    }

    void Damage(bool isAttacking){

        if (isAttacking){
            if(Vector3.Distance(transform.position, playerTarget.position) <= damageDistance){
                playerHealth.ReceiveDamage(damageAmount);

                //play sound random damage
                AudioClip a = damageHitSounds[Random.Range(0, 2)];
                GetComponent<AudioSource>().clip = a;
                GetComponent<AudioSource>().Play();
            }
        }

    }
}
