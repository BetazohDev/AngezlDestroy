using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Objeto que queremos seguir
    public float smoothSpeed = 0.125f; // Velocidad a la que la cámara se mueve
    public Vector3 offset; // Distancia entre la cámara y el objeto que seguimos

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // Posición deseada de la cámara
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Posición suavizada de la cámara
        transform.position = smoothedPosition; // Movemos la cámara a la posición suavizada
    }
}