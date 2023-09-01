using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stamina : MonoBehaviour
{
    private float staminaTotal = 100;
    private int cooldown = 200;
    private bool sprintando = true;
    private bool dasheando = true;
    private float cooldownDash = 0;

    // Update is called once per frame
    void Update()
    {
        cooldownDash += Time.deltaTime;

        if(staminaTotal > 0){
            if (sprintando){
                staminaTotal -= 0.03f;
                cooldown = 0;
            }
            if(cooldownDash > 15){
                if(dasheando){
                    staminaTotal -= 100;
                    cooldownDash = 0;
                }
            }
        }
        else{
            if (cooldown < 200){
                cooldown++;
            }
            else{
                staminaTotal += 0.08f;
            }
        }
    }
    
    //Interfaz provisoria para probar como funcionan las variables
    private void OnGUI(){
        GUI.Label(new Rect(25, 70, 140, 40), "staminaTotal: " + ((int) staminaTotal));
        GUI.Label(new Rect(150, 70, 140, 40), "cooldown: " + ((int) cooldown));
    }

}