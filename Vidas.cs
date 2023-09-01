using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vidas : MonoBehaviour
{
    private int hp = 2;
    private bool golpeViejo = false;
    private bool golpeVieja = false;

    // Update is called once per frame
    void Update()
    {
        if(hp > 0){
            if (golpeViejo){
                hp -= 1;
            }
            if (golpeVieja){
                hp = 0;
            }
        }
        else Debug.Log("Game over");

    }

    //Interfaz provisoria para probar como funcionan las variables
    private void OnGUI(){
        GUI.Label(new Rect(25, 110, 140, 40), "hp: " + ((int) hp));
    }    
}