using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRotation : MonoBehaviour
{
   

    Vector2 mouseDelta = Vector2.zero;

    //la "velocidad" de giro
    public float sensitivity = 10.0f;

    Vector2 amount = Vector2.zero;

    Rigidbody rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        mouseDelta.Set(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        //print("md " + md);


        //amplificar o reducir el movimiento segun la sensibilidad
        amount += mouseDelta * sensitivity;
        //amount.y = Mathf.Clamp(amount.y, -90, 90);  //Restringir esta rotacion (Comenta esta linea si quieres)

        //Mezclar las dos rotaciones:
        //transform.rotation = 
        //Quaternion.AngleAxis(amount.x, Vector3.up)
        //* Quaternion.AngleAxis(amount.y, Vector3.right);

        //Rotar la fisica:
        rb.MoveRotation(Quaternion.AngleAxis(amount.x, Vector3.up)
                        * Quaternion.AngleAxis(amount.y, Vector3.right));


    }
}
