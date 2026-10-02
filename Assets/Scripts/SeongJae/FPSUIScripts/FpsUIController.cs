using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FpsUIController : MonoBehaviour
{
    [SerializeField] private float _coolDown;

    private TextMeshProUGUI _fpsText;
    private WaitForSeconds _waitCoolDown;
    private Coroutine _refreshRoutine;
    private float _deltaTime = 0.0f;
    private bool _isActive = false;

    private void Awake() => CacheComponenets();
    private void Start() => Init();
    private void Update() => RefreshFrame();

    public void ActiveFpsUI()
    {
        _fpsText.text = "START";
        _isActive = true;
        StartUIRefresh();
    }

    public void UnActiveFpsUI()
    {
        _fpsText.text = "";
        _isActive = false;
        StopUIRefresh();
    }

    private void RefreshFrame()
    {
        _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
    }

    private void StartUIRefresh()
    {
        if (_refreshRoutine != null) return;

        _refreshRoutine = StartCoroutine(UIUpdateRoutine());
    }

    private void StopUIRefresh()
    {
        if (_refreshRoutine == null) return;

        StopCoroutine(_refreshRoutine);
        _refreshRoutine = null;
    }

    private IEnumerator UIUpdateRoutine()
    {
        while (_isActive)
        {
            yield return _waitCoolDown;

            _fpsText.text = $"FPS : {1.0f / _deltaTime}";
        }
    }

    private void CacheComponenets()
    {
        _fpsText = GetComponent<TextMeshProUGUI>();
        _waitCoolDown = new WaitForSeconds(_coolDown);
    }

    private void Init()
    {
        _fpsText.text = "";
        _isActive = false;
    }
}
