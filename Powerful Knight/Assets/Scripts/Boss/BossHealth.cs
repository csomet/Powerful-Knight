using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{


    public AudioClip[] damageSounds;
    public AudioClip deathSound;

    public float health = 200f;
    private Animator animator;

    private Image healthImg;


    void Awake()
    {

        animator = GetComponent<Animator>();
        healthImg = GameObject.Find("Energy").GetComponent<Image>();

    }



    public void ReceiveDamage(float damage)
    {

        //play animation for damage received
        animator.SetInteger("GetHit", 2);

        //play sound random damage
        AudioClip a = damageSounds[0];
        GetComponent<AudioSource>().clip = a;
        GetComponent<AudioSource>().Play();

        health -= damage;
        healthImg.fillAmount = health / 200f;

        if (health <= 0f)
        {

          //play death Sound
            GetComponent<AudioSource>().clip = deathSound;
            GetComponent<AudioSource>().Play();

        }

    }

}
