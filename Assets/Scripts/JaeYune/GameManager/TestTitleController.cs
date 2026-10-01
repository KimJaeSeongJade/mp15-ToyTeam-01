using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTitleController : MonoBehaviour
{
    [field: SerializeField] public GameObject _titleUi;
    [field: SerializeField] public GameObject _selectStageUi;

    private void Awake() => Init();

    private void Init()
    {
        _titleUi.SetActive(true);
        _selectStageUi.SetActive(false);
    }
}
