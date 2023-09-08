using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    //reemplazar "Player" por el nombre del objeto del jugador
    void OnCollisionEnter (Collision collisionInfo){
        if (collisionInfo.gameObject.tag == "Player"){
            StartCoroutine (Trapped());
        }
    }

    private IEnumerator Trapped(){
        //animacion de cerrar trampa y lobo liberandose
        GameObject.Find("Red").GetComponent<WolfController>().enabled = false;
        yield return new WaitForSeconds(3);
        GameObject.Find("Red").GetComponent<WolfController>().enabled = true;
    }
}
