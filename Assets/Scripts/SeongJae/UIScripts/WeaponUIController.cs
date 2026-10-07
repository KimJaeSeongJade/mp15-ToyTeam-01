using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIController : MonoBehaviour
{
    [Header("슬롯 UI 리스트")]
    [SerializeField] private List<Image> _weaponSlots;

    private int _currentSlot = 0;
    private void Start() => Init();
    public void SwapWeaponSlot(int index)
    {
        _weaponSlots[_currentSlot].gameObject.SetActive(false);
        _currentSlot = index;
        _weaponSlots[_currentSlot].gameObject.SetActive(true);
    }

    private void Init()
    {
        foreach(Image slot in _weaponSlots)
        {
            slot.gameObject.SetActive(false);
        }
    }
}
