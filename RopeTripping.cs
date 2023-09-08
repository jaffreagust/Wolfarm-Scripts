using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTripping : MonoBehaviour
{
    //reemplazar "Player" por el nombre del objeto del jugador 
    void OnCollisionEnter (Collision collisionInfo){
        if (collisionInfo.gameObject.tag == "Player"){ 
            collisionInfo.gameObject.transform.Translate(0,0,3);
            StartCoroutine (Tripped());
        }
    }

    private IEnumerator Tripped(){
        //Animacion de tropezar
        
        GameObject.Find("Red").GetComponent<WolfController>().enabled = false;
        //empujar al jugador al tropesarse
        yield return new WaitForSeconds(3);
        GameObject.Find("Red").GetComponent<WolfController>().enabled = true;
    }
}
