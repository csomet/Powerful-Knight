using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BossState{

    NONE,
    PAUSE,
    IDLE,
    ATTACK,
    DEATH
}

public class BossStateChecker : MonoBehaviour {

    private Transform playerTarget;
    private BossState bossState = BossState.NONE;
    private float distanceToTarget;
    private BossHealth bossHealth;
    private PlayerHealth playerHealth;
    [SerializeField]
    private float maxDistanceAlert = 8f;

	void Awake () {

        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        bossHealth = GetComponent<BossHealth>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
		
	}
	


	void Update () {
        SetState();
	}


    public BossState bossStateInfo
    {
        get { return bossState; }
        set { bossState = value; }
    }



    void SetState(){

        distanceToTarget = Vector3.Distance(transform.position, playerTarget.position);

        if (!bossState.Equals(BossState.DEATH)){

            if (playerHealth.health > 0){
                
                if (distanceToTarget > 3.8 && distanceToTarget <= maxDistanceAlert)
                {
                    bossState = BossState.PAUSE;
                }
                else if (distanceToTarget > maxDistanceAlert)
                {
                    bossState = BossState.IDLE;
                }
                else if (distanceToTarget <= 3.7)
                {
                    bossState = BossState.ATTACK;
                }
                else
                {
                    bossState = BossState.NONE;
                }
            } else {
                bossState = BossState.IDLE;
            }


            //Once we're dead no need for execute this If statement.
            if (bossHealth.health <= 0){
                bossState = BossState.DEATH;
            }

        }
    }

}
