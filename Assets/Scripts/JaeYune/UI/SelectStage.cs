using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    [SerializeField] private Button _easyStageButton;
    [SerializeField] private Button _hardStageButton;
    [SerializeField] private Button _hellStageButton;
    
    private TestTitleController _testTitleController;
    
    [SerializeField] private string _inGameSceneName;
    
    private void BindButtonEvents()
    {}
    
    private void UnbindButtonEvents()
    {}
    
    
}
