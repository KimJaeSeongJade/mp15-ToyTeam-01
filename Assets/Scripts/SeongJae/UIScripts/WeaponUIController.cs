using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIController : MonoBehaviour
{
    [Header("슬롯 UI 이미지")]
    [SerializeField] private List<Image> _weaponSlots;

    [Header("슬롯 텍스트")]
    [SerializeField] private List<TextMeshProUGUI> _weaponTexts;

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
        int index = 1;
        foreach(Image slot in _weaponSlots)
        {
            if (slot == _weaponSlots[_currentSlot]) continue;
            slot.gameObject.SetActive(false);
        }

        foreach(TextMeshProUGUI post in _weaponTexts)
        {
            post.text = "POST " + index;
            index++;
        }
    }
}
