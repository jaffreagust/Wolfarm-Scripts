using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiempo : MonoBehaviour
{

    private float sTranscurridos = 0;
    public int tiempoMax = 420;

    // Update is called once per frame
    void Update()
    {
        if (sTranscurridos < tiempoMax){
            sTranscurridos += Time.deltaTime;
        }
        else{
            Debug.Log("Game over");
        }
    }

    //Interfaz provisoria para probar como funcionan las variables
    private void OnGUI(){
        GUI.Label(new Rect(25, 30, 140, 40), "sTranscurridos: " + ((int) sTranscurridos));
    }
}