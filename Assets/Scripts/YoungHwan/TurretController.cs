using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("터렛")]
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private Transform _turretPoint;
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;
    [SerializeField] private float _mouseSensitivity;
    
    private float _pitch;
    
    private void Update()
    {
        Rotate();
    }

    private Vector3 ReadRotateInput()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        return new Vector3(-y, x, 0);

    }
    
    public void Rotate()
    {
        Vector3 input = ReadRotateInput() * _mouseSensitivity;
        
        transform.Rotate(0, input.y, 0, Space.Self);
        
        _pitch = Mathf.Clamp(_pitch + input.x, -_maxPitch, _maxPitch);
        
        _cameraPivot.localRotation = Quaternion.Euler(_pitch, 0, 0);
    }
    
}
