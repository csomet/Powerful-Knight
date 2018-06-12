using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heal : MonoBehaviour {


    public float healthIncrease = 20f;
    private Image healthImg;

	void Start () {

        float initialHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health;
        healthImg = GameObject.Find("Health").GetComponent<Image>();

        //Play sound healing
        GetComponent<AudioSource>().Play();

        if (initialHealth < 100){

            if (initialHealth + healthIncrease >= 100){

                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health = 100f;
                healthImg.fillAmount = 1;

            }else{
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health += healthIncrease;
                healthImg.fillAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health / 100f;
            }
        }


	}


}
