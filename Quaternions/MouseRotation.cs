using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour
{
    Quaternion qLeft;
    Quaternion qRight;

    Quaternion qUp;
    Quaternion qDown;

    Vector2 mouseDelta = Vector2.zero;

    //la "velocidad" de giro
    public float sensitivity = 0.05f;

    Vector2 amount = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        //creamos 4 quaterniones apuntando en diferentes direcciones

        qLeft = Quaternion.AngleAxis(-90, Vector3.up);
        qRight = Quaternion.AngleAxis(90, Vector3.up);

        qUp = Quaternion.AngleAxis(-90, Vector3.right);
        qDown = Quaternion.AngleAxis(90, Vector3.right);


        amount.Set(0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

        mouseDelta.Set(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        //print("md " + md);


        //amplificar o reducir el movimiento segun la sensibilidad
        amount += mouseDelta * sensitivity;

   
        //print("amount " + amount);

        //interpolar y mezclar las rotaciones segun el movimiento del raton

        transform.rotation =
            Quaternion.Lerp(qLeft, qRight, amount.x) *
            Quaternion.Lerp(qUp, qDown, amount.y);

    }
}
