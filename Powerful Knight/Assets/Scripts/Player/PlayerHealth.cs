using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {


    public AudioClip[] damageSounds;
    public AudioClip deathSound;

    public float health = 100f;
    private Animator animator;

    private bool isActiveShield;
    private Image healthImg;

    public bool activeShield{

        get { return isActiveShield; }
        set { isActiveShield = value; }
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        healthImg = GameObject.Find("Health").GetComponent<Image>();
    }

    public void ReceiveDamage(float damage){
        
        if (!isActiveShield){
            
            //play sound random damage
            AudioClip a = damageSounds[Random.Range(0, 3)];
            GetComponent<AudioSource>().clip = a;
            GetComponent<AudioSource>().Play();

            health -= damage;
            healthImg.fillAmount = health / 100f;
        }


        if (health <= 0f){

            animator.SetBool("Death", true);
            //deactivate circle
            GameObject.Find("Magic Circle").SetActive(false);
            //play death Sound
            GetComponent<AudioSource>().clip = deathSound;
            GetComponent<AudioSource>().Play();

        }
    }
  
	
}
