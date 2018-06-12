using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour {

    public float damageAmount = 20f;
    private Animator animator;
    private Transform playerTarget;
    private bool finishedAttacking = true;
    [SerializeField]
    private float damageDistance = 2.8f;
    [SerializeField]
    private AudioClip hitPlayer;


    private PlayerHealth playerHealth;

    void Awake()
    {

        animator = GetComponent<Animator>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = playerTarget.GetComponent<PlayerHealth>();
    }


    void Update()
    {

        if (finishedAttacking)
        {
            Damage(CheckPlayerAttacking());

        }
        else
        {
            if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                finishedAttacking = true;
            }
        }
    }


    private bool CheckPlayerAttacking()
    {
        bool isAttacking = false;

        if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack(1)") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Attack(2)") || 
            animator.GetCurrentAnimatorStateInfo(0).IsName("Attack(3)") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Attack(4)") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Attack(5)") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Attack(6)"))
       {

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
            {

                isAttacking = true;
                finishedAttacking = false;
            }
        }

        return isAttacking;


    }

    void Damage(bool isAttacking)
    {

        if (isAttacking)
        {
            if (Vector3.Distance(transform.position, playerTarget.position) <= damageDistance)
            {
                GetComponent<AudioSource>().clip = hitPlayer;
                GetComponent<AudioSource>().Play();
                playerHealth.ReceiveDamage(damageAmount);

            }
        }

    }
}
