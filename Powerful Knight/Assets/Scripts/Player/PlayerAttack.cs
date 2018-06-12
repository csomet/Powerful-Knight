using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {

    //Values for the image filling parameter in the image fill type of the powers.
    //When it is 1 is full. 0 = void. Recall the 360 clockwise filling effect.
    public Image fillWaitImage1;
    public Image fillWaitImage2;
    public Image fillWaitImage3;
    public Image fillWaitImage4;
    public Image fillWaitImage5;
    public Image fillWaitImage6;

    private int[] ImagesToFade = new int[] { 0, 0, 0, 0, 0, 0 };

    private PlayerMovement playerMovement;
    private Animator animator;
    private bool canAttack;

	void Awake () {

        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
	}
	
	
	void Update () {

        if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Stand")){
            canAttack = true;
        } else {
            canAttack = false;
        }

        CheckInput();
        CheckToFade();
	}


    void CheckInput() {
        
        if(animator.GetInteger("Atk") == 0){
            playerMovement.IsfinishedMoving = false;

            if(!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Stand")){

                playerMovement.IsfinishedMoving = true;
            }

        }


        if (Input.GetKeyDown(KeyCode.Alpha1)){

            if(playerMovement.IsfinishedMoving && ImagesToFade[0] != 1 && canAttack){
                ImagesToFade[0] = 1;
                animator.SetInteger("Atk", 1);
                playerMovement.TargetPos = transform.position;
                RemoveCursor();
            }


        }else if (Input.GetKeyDown(KeyCode.Alpha2)){
            

            if (playerMovement.IsfinishedMoving && ImagesToFade[1] != 1 && canAttack)
            {
                ImagesToFade[1] = 1;
                animator.SetInteger("Atk", 2);
                playerMovement.TargetPos = transform.position;
                RemoveCursor();
            }

        } else if (Input.GetKeyDown(KeyCode.Alpha3)){
            

            if (playerMovement.IsfinishedMoving && ImagesToFade[2] != 1 && canAttack)
            {
                ImagesToFade[2] = 1;
                animator.SetInteger("Atk", 3);
                playerMovement.TargetPos = transform.position;
                RemoveCursor();
            }

        } else if (Input.GetKeyDown(KeyCode.Alpha4)){
            

            if (playerMovement.IsfinishedMoving && ImagesToFade[3] != 1 && canAttack)
            {
                ImagesToFade[3] = 1;
                animator.SetInteger("Atk", 4);
                playerMovement.TargetPos = transform.position;
                RemoveCursor();
            }

        } else if (Input.GetKeyDown(KeyCode.Alpha5)){

            if (playerMovement.IsfinishedMoving && ImagesToFade[4] != 1 && canAttack)
            {
                ImagesToFade[4] = 1;
                animator.SetInteger("Atk", 5);
                playerMovement.TargetPos = transform.position;
                RemoveCursor();
            }

        } else if (Input.GetKeyDown(KeyCode.Alpha6)) {
            
            if (playerMovement.IsfinishedMoving && ImagesToFade[5] != 1 && canAttack)
            {
                ImagesToFade[5] = 1;
                animator.SetInteger("Atk", 6);
                playerMovement.TargetPos = transform.position;
                RemoveCursor();
            }

        } else {
            animator.SetInteger("Atk", 0);
        }



        //Rotate the player
        if(Input.GetMouseButton(1)){

            Vector3 pos = Vector3.zero;

            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)){
                pos = new Vector3(hit.point.x, transform.position.y, hit.point.z);

            }

            transform.rotation = Quaternion.Slerp(transform.rotation, 
                                 Quaternion.LookRotation(pos - transform.position), 15.0f * Time.deltaTime);
        }



    }

   

    bool FadeAndWait(Image imageToFade, float timeFaded){

        bool faded = false;

        if (imageToFade == null){
            return faded;
        }

        if(!imageToFade.gameObject.activeInHierarchy){
            imageToFade.gameObject.SetActive(true);
            imageToFade.fillAmount = 1f;

        }

        imageToFade.fillAmount -= timeFaded * Time.deltaTime;

        if (imageToFade.fillAmount <= 0.0f){
            imageToFade.gameObject.SetActive(false);
            faded = true;
        }

        return faded;
    }


    void CheckToFade()
    {

        if (ImagesToFade[0] == 1){

            if(FadeAndWait(fillWaitImage1, Confg.ACTION1_DELAY_TIME)){
                ImagesToFade[0] = 0;
            }
        }

        if (ImagesToFade[1] == 1)
        {

            if (FadeAndWait(fillWaitImage2, Confg.ACTION2_DELAY_TIME))
            {
                ImagesToFade[1] = 0;
            }
        }

        if (ImagesToFade[2] == 1)
        {

            if (FadeAndWait(fillWaitImage3, Confg.ACTION3_DELAY_TIME))
            {
                ImagesToFade[2] = 0;
            }
        }

        if (ImagesToFade[3] == 1)
        {
            
            if (FadeAndWait(fillWaitImage4, Confg.ACTION4_DELAY_TIME))
            {
                ImagesToFade[3] = 0;
            }
        }

        if (ImagesToFade[4] == 1)
        {
            
            if (FadeAndWait(fillWaitImage5, Confg.ACTION5_DELAY_TIME))
            {
                ImagesToFade[4] = 0;
            }
        }

        if (ImagesToFade[5] == 1)
        {

            if (FadeAndWait(fillWaitImage6, Confg.ACTION6_DELAY_TIME))
            {
                ImagesToFade[5] = 0;
            }
        }
    }


    void RemoveCursor(){
        
        GameObject cursor = GameObject.FindGameObjectWithTag("Cursor");

        if (cursor){
            Destroy(cursor);
        }

    }



}
