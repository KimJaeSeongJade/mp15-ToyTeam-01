using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusUIHandler : MonoBehaviour
{
    [SerializeField] private NexusData _nexusData;
    private NexusUIController _nexusUIController;

    private void Awake() => CacheComponents();
    private void OnEnable() => AddListener();
    private void OnDisable() => RemoveListener();

    private void CacheComponents()
    {
        _nexusUIController = GetComponent<NexusUIController>();
    }

    private void AddListener()
    {
        _nexusData.OnMaxHealthChanged += _nexusUIController.RefreshMaxHealthText;
        _nexusData.OnHealthChanged += _nexusUIController.RefreshCurrentHealthText;
        _nexusData.OnHealthChanged += _nexusUIController.RefreshHealthBar;
    }

    private void RemoveListener()
    {
        _nexusData.OnMaxHealthChanged -= _nexusUIController.RefreshMaxHealthText;
        _nexusData.OnHealthChanged -= _nexusUIController.RefreshCurrentHealthText;
        _nexusData.OnHealthChanged -= _nexusUIController.RefreshHealthBar;
    }
}
