using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] public float MovementSmoothing = 0.125f;

    public Vector3 offset;
    
    private void FixedUpdate() 
    {
        Vector3 targetPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, MovementSmoothing);

        transform.position = new Vector3(smoothedPosition.x, transform.position.y, transform.position.z);
    }
}