using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oveja : MonoBehaviour
{
    private Rigidbody cuerpo;
    private GameObject target;
    private bool peligro;
    private bool colision;

    public float velSheep = 400.0f;
    public float dangerSheep = 450.0f;

    private enum SheepState { Safe, Danger, Collision };
    private SheepState currentState = SheepState.Safe;

    private Quaternion targetRotation;
    private float rotationSpeed = 2.0f;

    public int rutina;
    public float cronometro;
    public Quaternion angulo;
    public float grado;
    public float maxSpeed = 500.0f; 
    public float rotationForce= 50.0f;
    

    void Start()
    {
        colision = false;
        peligro = false;
        cuerpo = gameObject.GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Comportamiento_Oveja();
        CheckSpeed();
    }

    void Comportamiento_Oveja()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 15)
        {
            peligro = false;
        }
        else
        {
            peligro = true;
        }
        
        switch (currentState)
        {
            case SheepState.Safe:
                SafeBehavior();
                break;
            case SheepState.Danger:
                DangerBehavior();
                break;
            case SheepState.Collision:
                CollisionBehavior();
                break;
        }

        Debug.Log("El estado es " + currentState);
    }

    void SafeBehavior()
    {
        /*if (Vector3.Distance(transform.position, target.transform.position) > 15)
        {
            peligro = false;
        }
        else
        {
            peligro = true;
        }*/

        // Handle safe behavior here, e.g., random movement
        Debug.Log("Modo seguro");
            cronometro += 1 * Time.deltaTime;
            if (cronometro >=2)
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
        currentState = peligro ? SheepState.Danger : SheepState.Safe;
    }

    void DangerBehavior()
    {
        // Handle danger behavior here, e.g., flee from the player
        var lookPos = transform.position - target.transform.position;
        lookPos.y = 0;
        targetRotation = Quaternion.LookRotation(lookPos) * Quaternion.Euler(0,30,0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        cuerpo.AddForce(transform.forward * dangerSheep);
        currentState = peligro ? SheepState.Danger : SheepState.Safe;
    }

    void CollisionBehavior()
    {
        // Handle collision behavior here, e.g., rotate and add force
        // You can use your existing code for collision behavior here
        if(grado<180)
        {
            var grado2 = grado + 100;
            var angulo2 = Quaternion.Euler(0, grado2, 0);
            
            transform.Rotate(Vector3.up * 120f * rotationForce * Time.deltaTime);
            cuerpo.AddForce(transform.forward * velSheep * 30);
            Debug.Log(grado2);
        }
        else
        {
            var grado2 = grado - 100;
            var angulo2 = Quaternion.Euler(0, grado2, 0);
            transform.Rotate(Vector3.up * -120f * rotationForce * Time.deltaTime);
            cuerpo.AddForce(transform.forward * velSheep * 30);
            Debug.Log(grado2);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Corral")
        {
            currentState = SheepState.Collision;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        currentState = peligro ? SheepState.Danger : SheepState.Safe;
    }
    void CheckSpeed (){
        if(cuerpo.velocity.magnitude > maxSpeed)
        {
               cuerpo.velocity = cuerpo.velocity.normalized * maxSpeed;
        }
    }
}
