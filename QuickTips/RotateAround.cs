using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using static Unity.Mathematics.math;
using Unity.Mathematics;

public class RotateAround : MonoBehaviour
{
    public Transform target;
    public float distance = 3;
    public float speed = 30;
    public float angSpeed = 20;

    double sinA, cosA;
    float3 pos;
    float t = 0.0f;

    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(angSpeed * Time.deltaTime, Vector3.up);

        t += Time.deltaTime * speed * 0.1f;

        sincos(t, out sinA, out cosA);

        pos = float3(distance * (float)cosA, transform.position.y, distance * (float)sinA);

        transform.position = pos;
        transform.position += target.position;

    }
}
