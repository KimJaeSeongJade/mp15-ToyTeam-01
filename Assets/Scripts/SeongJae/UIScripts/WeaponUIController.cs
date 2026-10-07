using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIController : MonoBehaviour
{
    [SerializeField] private List<Image> _weaponSlots;

    private int _currentSlot = 0;
    public void SwapWeaponSlot(int index)
    {
        _weaponSlots[_currentSlot].gameObject.SetActive(false);
        _currentSlot = index;
        _weaponSlots[_currentSlot].gameObject.SetActive(true);
    }
}
