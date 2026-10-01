using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private List<TurretBase> _turrets;
    
    private int _turretIndex = 0;
    private float _pitch;
    private Camera _camera;
    private event Action _onFire;
    private void Start() => Init();
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeTurret(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeTurret(1);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            FireNotify();
        }
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
        _camera.gameObject.transform.position = _cameraPivot.position;
        _camera.gameObject.transform.rotation = _cameraPivot.rotation;
        _turrets[_turretIndex].transform.position = _cameraPivot.position;
    }

    private void ChangeTurret(int index)
    {
        RemoveListener(_turrets[_turretIndex]);
        _turretIndex = index;
        AddListener(_turrets[_turretIndex]);
        
        Debug.Log($"변경된 터렛 위치 : {_turrets[_turretIndex].transform.position}");
        transform.position = _turrets[_turretIndex].transform.position;
        Debug.Log(_turrets[_turretIndex].transform.rotation);
        _cameraPivot.rotation = _turrets[_turretIndex].transform.rotation;
    }

    private void AddListener(TurretBase turret)
    {
        _onFire += turret.Fire;
    }

    private void RemoveListener(TurretBase turret)
    {
        _onFire -= turret.Fire;
    }

    private void FireNotify()
    {
        _onFire?.Invoke();
    }

    private void Init()
    {
        _camera = Camera.main;
        ChangeTurret(0);
    }
    
}
