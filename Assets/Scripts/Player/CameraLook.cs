using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    private float XMove;
    private float YMove;
    private float XRotation;
    
    [SerializeField] private Transform PlayerBody;
    public Vector2 LockAxis;
    public float Sensivity;
    
    public float MinRotationX = -60f; // Set your desired min angle
    public float MaxRotationX = 60f;  // Set your desired max angle

    void Update()
    {
        XMove = LockAxis.x * Sensivity * Time.deltaTime;
        YMove = LockAxis.y * Sensivity * Time.deltaTime;
        
        XRotation -= YMove;
        XRotation = Mathf.Clamp(XRotation, MinRotationX, MaxRotationX); // Restrict rotation
        
        transform.localRotation = Quaternion.Euler(XRotation, 0, 0);
        PlayerBody.Rotate(Vector3.up * XMove);
    }
}