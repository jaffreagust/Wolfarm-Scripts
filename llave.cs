using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class llave : MonoBehaviour
{
    public KeyCode openKey = KeyCode.E;
    public bool llave_found;
    public float openRadius = 1f;
    public LayerMask openLayer;
    // Start is called before the first frame update
    void Start()
    {
        llave_found = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(llave_found == true && Input.GetKeyDown(openKey)){
            OpenDoor();
        }
    }

    private void OpenDoor(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, openRadius, openLayer);

        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.tag == "Puerta"){
                collider.gameObject.layer = LayerMask.NameToLayer("Ignore");;
            }
        }
    }
}
