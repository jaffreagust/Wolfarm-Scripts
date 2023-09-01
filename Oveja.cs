using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oveja : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Quaternion angulo;
    public float grado;
    
    public GameObject target;

    void Start()
    {
        target = GameObject.Find("Lobo");
    }

    public void Comportamiento_Oveja()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 3){

            cronometro += 1 * Time.deltaTime;
            if (cronometro >=4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                transform.Translate(Vector3.forward * 0 * Time.deltaTime);
                    break;

                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina ++;
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    break;
            }
        }
        else{
            var lookPos = transform.position - target.transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
            transform.Translate(Vector3.forward * 2 * Time.deltaTime);
        }
    }
    
    void Update()
    {
        Comportamiento_Oveja();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Corral"){
            if(grado<180){
                angulo = Quaternion.Euler(0, (grado + 180), 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                transform.Translate(Vector3.forward * 1 * Time.deltaTime);
            }
            else{
                angulo = Quaternion.Euler(0, (grado - 180), 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                transform.Translate(Vector3.forward * 1 * Time.deltaTime);
            }
        }
    }
}
