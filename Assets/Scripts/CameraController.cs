using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Es el player que añadimos al objeto camara
    public float speed = 0.5f;
    public Vector3 offset; // Encuadre
    
    //Es el lateUpdate porque es el último update cada frame del motor del juego
    private void LateUpdate() 
    {
        Vector3 cameraPosition = transform.position; // Es la posición inicial del objeto transform de la camara
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(cameraPosition, desiredPosition, speed);
        smoothPosition.y = 0f; // Bloquear la posición vertical de la cámara en 0;
        transform.position = smoothPosition;
    }

}

