using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    private Animator animator;
    private CharacterController characterController;
    private CollisionFlags collisionFlags = CollisionFlags.None;
    private PlayerHealth playerHealth;

    private float moveSpeed  = 5f;
    private bool canMove;
    private bool finishedMoving = true;

    private float playerToPointDistance;
    private Vector3 targetPos = Vector3.zero;
    private Vector3 playerMov = Vector3.zero;

    private float gravity = 9.8f;

    private float height;



    void Awake(){
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        playerHealth = GetComponent<PlayerHealth>();

            
    }

	
	// Update is called once per frame
	void Update () {
        CalculateHeight();
        CheckIfWeFinisihedMovement();
        CheckDeath();
	}


    //Checking if we are on the floor
    bool IsGrounded(){
        return collisionFlags == CollisionFlags.CollidedBelow ? true : false;
    }



   public bool IsfinishedMoving {

        get {
            return finishedMoving;
        } 

        set{
            finishedMoving = value;
        }
    }

    public Vector3 TargetPos {

        get {
            return targetPos;
        }
        set {
            targetPos = value;
        }
    }


    void CalculateHeight(){
        if (IsGrounded()){
            
            height = 0f;

        }else {
            height -= gravity * Time.deltaTime;
        }
    }

    void CheckIfWeFinisihedMovement(){

        if(!finishedMoving){

            if (!animator.IsInTransition(0) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Stand")
                && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f){
                finishedMoving = true;
            }
        }else {
            MovePlayer();
            playerMov.y = height * Time.deltaTime;
            collisionFlags = characterController.Move(playerMov);

        }

    }


    void MovePlayer (){

        if (Input.GetMouseButtonDown(0)){
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)){
                if (hit.collider is TerrainCollider){

                    playerToPointDistance = Vector3.Distance(transform.position, hit.point);

                    if (playerToPointDistance >= 1.0f){
                        canMove = true;
                        targetPos = hit.point;
                    }

                }
            }

           
        }


        if (canMove)
        {
            animator.SetFloat("Walk", 1.0f);

            Vector3 targetTemp = new Vector3(targetPos.x, transform.position.y, targetPos.z);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetTemp - transform.position), 15.0f * Time.deltaTime);

            playerMov = moveSpeed * transform.forward * Time.deltaTime;

            if (Vector3.Distance(transform.position, targetPos) <= 0.3f)
            {
                canMove = false;
            }

        }
        else
        {
            playerMov.Set(0f, 0f, 0f);
            animator.SetFloat("Walk", 0f);

        }
    }


    void CheckDeath(){
        if (playerHealth.health <= 0f){
            enabled = false;
            GetComponent<PlayerAttack>().enabled = false;

        }
    }

}
