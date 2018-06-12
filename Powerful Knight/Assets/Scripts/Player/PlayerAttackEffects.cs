using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEffects : MonoBehaviour {
    

    public GameObject groundImpactSpawn;
    public GameObject kickSpawn;
    public GameObject fireTornadoSpawn;
    public GameObject fireShieldSpawn;


    public GameObject groundImpactPrefab;
    public GameObject kickPrefab;
    public GameObject fireTornadoPrefab;
    public GameObject fireShieldPrefab;
    public GameObject thunderAttackPrefab;
    public GameObject healPrefab;


	


    void GroundImpact(){

        Instantiate(groundImpactPrefab, groundImpactSpawn.transform.position, Quaternion.identity);
    }


    void Kick(){

        Instantiate(kickPrefab, kickSpawn.transform.position, Quaternion.identity);

    }

    void ThunderAttack(){

        for (int i = 0; i < 8; i++)
        {

            Vector3 pos = Vector3.zero;

            if (i == 0)
            {
                pos = new Vector3(transform.position.x - 4f, transform.position.y + 0.8f, transform.position.z);

            } else if (i == 1){
                
                pos = new Vector3(transform.position.x + 4f, transform.position.y + 0.8f, transform.position.z);

            } else if (i == 2)
            {

                pos = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z - 3f);

            } else if (i == 3)
            {

                pos = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z - 3f);

            } else if (i == 4)
            {

                pos = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z + 3f);

            } else if (i == 5)
            {

                pos = new Vector3(transform.position.x+1f, transform.position.y + 0.8f, transform.position.z - 2.5f);

            } else if (i == 6)
            {
                
                pos = new Vector3(transform.position.x + 1f, transform.position.y + 0.88f, transform.position.z + 1f);

            } else if (i == 7)
            {

                pos = new Vector3(transform.position.x - 2.5f, transform.position.y + 0.81f, transform.position.z - 1f);
            }


            Instantiate(thunderAttackPrefab, pos, Quaternion.identity);
        }

    }

    void FireTornado(){

        Instantiate(fireTornadoPrefab, fireShieldSpawn.transform.position, Quaternion.identity);
    }


    void FireShield() {

        GameObject fshield = Instantiate(fireShieldPrefab, fireShieldSpawn.transform.position, Quaternion.identity) 
            as GameObject;
        // Set same position for gameobjet than parent.
        fshield.transform.SetParent(transform);
    }


    void Heal(){

        Vector3 temp = transform.position;
        temp.y += 2f;
        GameObject healObj = Instantiate(healPrefab, temp, Quaternion.identity) as GameObject;
        // Set same position for gameobjet than parent.
        healObj.transform.SetParent(transform);

       
    }


}
