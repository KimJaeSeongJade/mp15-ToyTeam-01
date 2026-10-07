using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUIHandler : MonoBehaviour
{
    [Header("UI 컨트롤러")]
    [SerializeField] private WeaponUIController _weaponUIController;

    [Header("터렛 컨트롤러")]
    [SerializeField] private TurretController _turretController;

    private void OnEnable() => BindEvent();

    private void BindEvent() => _turretController.OnTurretChanged += _weaponUIController.SwapWeaponSlot;
    private void UnBindEvent() => _turretController.OnTurretChanged -= _weaponUIController.SwapWeaponSlot;
}
