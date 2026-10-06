using System;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private Transform _weaponHolder;
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private List<TurretBase> _turrets;
    
    private int _turretIndex = 0;
    private float _pitch;
    private Camera _camera;
    private Vector2 _currentRotation;
    private List<GameObject> _weaponModels = new();
    private bool _isChanged;
    public TurretBase CurrentTurret => _turrets[_turretIndex];
    public int CurrentIndex => _turretIndex;

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
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeTurret(2);
        }
        else
        {
            Rotate();
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            FireNotify();
        }
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
        if (input == Vector3.zero) return;

        _pitch = Mathf.Clamp(_pitch + input.x, -_maxPitch, _maxPitch);

        transform.Rotate(0, input.y, 0, Space.World);
        _cameraPivot.localRotation = Quaternion.Euler(_pitch, 0, 0);

        _camera.gameObject.transform.position = _cameraPivot.position;
        _camera.gameObject.transform.rotation = _cameraPivot.rotation;
        _turrets[_turretIndex].transform.rotation = _cameraPivot.rotation;
    }

    private void ChangeTurret(int index)
    {
        if (index >= _turrets.Count) return;
        
        RemoveListener(_turrets[_turretIndex]);
        _turretIndex = index;
        AddListener(_turrets[_turretIndex]);

        _currentRotation.x = _turrets[_turretIndex].transform.eulerAngles.x;
        _currentRotation.y = _turrets[_turretIndex].transform.eulerAngles.y;

        transform.position = _turrets[_turretIndex].transform.position;
        transform.eulerAngles = new Vector3(_currentRotation.x, _currentRotation.y, 0);

        _camera.gameObject.transform.position = _cameraPivot.position;
        _camera.gameObject.transform.rotation = _cameraPivot.rotation;
        
        ShowWeaponModel(index);
    }

    private void CreateWeaponModels()
    {
        foreach (TurretBase turret in _turrets)
        {
            GameObject model = Instantiate(turret.WeaponModel, _weaponHolder);
            turret.SetFirePoint(model.transform);
            model.SetActive(false);
            _weaponModels.Add(model);
        }
    }
    
    private void ShowWeaponModel(int index)
    {
        for (int i = 0; i < _weaponModels.Count; i++)
        {
            _weaponModels[i].SetActive(i == index);
        }
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
        CreateWeaponModels();
        ChangeTurret(0);
    }
    
}
