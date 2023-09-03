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
    private int peligro;
    private int bandera;
    public float speed = 1;

    void Start()
    {
        bandera = 1;
        cuerpo = gameObject.GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Lobo");
    }

    public void Comportamiento_Oveja()
    {
       
        if (Vector3.Distance(transform.position, target.transform.position) > 3){
            peligro = 0;
        }
        else {
            peligro = 1;
        }

        if (peligro == 0 && bandera ==1){
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
        else if (peligro == 1 && bandera == 1){
            Debug.Log("Modo peligro");
            var lookPos = transform.position - target.transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
            cuerpo.AddForce(transform.forward * dangerSheep );
        }
    }
    
    void Update()
    {
        Comportamiento_Oveja();
        
        
    }

    public void OnCollisionEnter(Collision collision)
    {
       
        if(collision.gameObject.tag == "Corral"){
            bandera = 0;
            peligro = 0;
            var step = speed * (1+Time.deltaTime);
            if(grado<180){
                grado = grado + 100;
                angulo = Quaternion.Euler(0, grado, 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, step);
                cuerpo.AddForce(transform.forward * velSheep);
                Debug.Log("rotacion1");
            }
            else{
                grado = grado - 100;
                angulo = Quaternion.Euler(0, grado, 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, step);
                cuerpo.AddForce(transform.forward * velSheep);
                Debug.Log("rotacion2");
            }
        }

    }

    public void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Corral"){
            bandera = 1;
            Debug.Log("collision exit");
        }
    }
}