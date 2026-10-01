using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField] private GameObject _titleUi;
    [SerializeField] private GameObject _selectStageUi;

    private void Awake() => ViewMainMenu();
    
    public void ViewSelectStage()
    {
        _titleUi.SetActive(false);
        _selectStageUi.SetActive(true);
    }

    public void ViewMainMenu()
    {
        _titleUi.SetActive(true);
        _selectStageUi.SetActive(false);
    }
}
