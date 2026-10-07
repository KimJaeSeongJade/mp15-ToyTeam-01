using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [Header("반동")]
    [SerializeField] private float _kickBack;
    [SerializeField] private float _kickUp;
    [SerializeField] private float _returnSpeed;

    private Vector3 _startPosition;
    private Vector3 _startAngle;
    private float _recoil;

    private void Awake()
    {
        _startPosition = transform.localPosition;
        _startAngle = transform.localEulerAngles;
    }
    
    private void Update()
    {
        _recoil = Mathf.Lerp(_recoil, 0, _returnSpeed * Time.deltaTime);

        transform.localPosition = _startPosition + Vector3.back * _kickBack * _recoil;
        transform.localEulerAngles = _startAngle + new Vector3(-_kickUp * _recoil, 0, 0);
    }
    
    public void Play()
    {
        _recoil = 1;
    }
}