using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oveja : MonoBehaviour
{
    private Rigidbody cuerpo;
    public int rutina;
    public float cronometro;
    public Quaternion angulo;
    public float grado;
    private float velSheep = 5.0f;
    private float dangerSheep = 5.0f;
    public GameObject target;
    private bool peligro;
    private bool colision;

    void Start()
    {
        colision = false;
        cuerpo = gameObject.GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Lobo");
    }

    public void Comportamiento_Oveja()
    {
       
        if (Vector3.Distance(transform.position, target.transform.position) > 3){
            peligro = false;
        }
        else {
            peligro = true;
        }

        if (peligro == false && colision == false){
            Debug.Log("Modo seguro");
            cronometro += 1 * Time.deltaTime;
            if (cronometro >=1)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                cuerpo.AddForce(transform.forward * 0 );
                    break;

                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina ++;
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    cuerpo.AddForce(transform.forward * velSheep);
                    break;
            }
        }
        else if (peligro == true && colision == false){
            Debug.Log("Modo peligro");
            var lookPos = transform.position - target.transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
            cuerpo.AddForce(transform.forward * dangerSheep );
        }
         if (colision == true){
            if(grado<180){
                var grado2 = grado + 100;
                var angulo2 = Quaternion.Euler(0, grado2, 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo2, 2);
                cuerpo.AddForce(transform.forward * velSheep);
                Debug.Log(grado2);
            }
            else{
                var grado2 = grado - 100;
                var angulo2 = Quaternion.Euler(0, grado2, 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo2, 2);
                cuerpo.AddForce(transform.forward * velSheep);
                Debug.Log(grado2);
            }
        }
    }
    
    void Update()
    {
        Comportamiento_Oveja();
        
        
    }

    public void OnCollisionEnter(Collision collision)
    {
       
        if(collision.gameObject.tag == "Corral"){
            colision = true;
            peligro = false; 
        }

    }

    public void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Corral"){
            colision = false;
            Debug.Log("collision exit");
        }
    }
}