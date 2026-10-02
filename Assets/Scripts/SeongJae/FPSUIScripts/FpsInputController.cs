using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsInputController : MonoBehaviour
{
    private FpsUIController _uiController;
    private bool _isActive = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!_isActive)
            {
                Debug.Log("활성화");
                _isActive = true;
                _uiController.ActiveFpsUI();
            }
            else if (_isActive)
            {
                Debug.Log("비활성화");
                _isActive = false;
                _uiController.UnActiveFpsUI();
            }
        }
    }

    private void Awake()
    {
        _uiController = GetComponentInChildren<FpsUIController>();
    }
}
