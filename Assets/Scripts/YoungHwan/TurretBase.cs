using System.Collections;
using UnityEngine;

public abstract class TurretBase : MonoBehaviour
{
    [Header("공통")]
    [SerializeField] private TurretType _type;
    [SerializeField] private GameObject _weaponModel;
    [SerializeField] protected Transform _firePoint;
    [SerializeField] private ObjectPool _bulletPool;
    [SerializeField] private BulletData _data;
    [SerializeField] private float _fireDelay;
    [Header("과열")]
    [SerializeField] private float _maxHeat;
    [SerializeField] private float _heatPerShot;
    [SerializeField] private float _coolPerSecond;
    [SerializeField] private float _coolDelay;
    
    public bool OverHeat => _isOverHeat;
    
    private bool _canFire = true;
    private float _currentHeat;
    private bool _isOverHeat;
    private float _lastFireTime;
    private WeaponRecoil _recoil;
    private FlameEffect _muzzleFlame;
    
    public TurretType Type => _type;
    public GameObject WeaponModel => _weaponModel;
    public float HeatGauge => _currentHeat / _maxHeat;
    public bool IsOverHeat => _isOverHeat;
    
    private void Update()
    {
        if (Time.time - _lastFireTime < _coolDelay) return;
        _currentHeat -= _coolPerSecond * Time.deltaTime;
        _currentHeat = Mathf.Max(0, _currentHeat);

        if (_isOverHeat && _currentHeat == 0) _isOverHeat = false;
    }
    
    public void Fire()
    {
        if (!_canFire || _isOverHeat) return;
        StartCoroutine(FireRoutine());
    }

    private IEnumerator FireRoutine()
    {
        _canFire = false;
        Attack();
        if (_recoil != null) _recoil.Play();
        if (_muzzleFlame != null) _muzzleFlame.Play();
        
        _currentHeat += _heatPerShot;
        _lastFireTime = Time.time;
        if (_currentHeat >= _maxHeat)
        {
            _currentHeat = _maxHeat;
            _isOverHeat = true;
        }
        FireSoundOn();
        
        yield return new WaitForSeconds(_fireDelay);
        _canFire = true;
    }

    public void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        BulletController bullet = _bulletPool.Take() as BulletController;
        if (bullet == null) return;
        
        bullet.SetData(_data, position, rotation);
    }

    public virtual void SetFirePoint(Transform model)
    {
        Transform point = model.Find("FirePoint");
        if (point != null) _firePoint = point;

        _recoil = model.GetComponent<WeaponRecoil>();
        _muzzleFlame = _firePoint.GetComponentInChildren<FlameEffect>(true);
    }

    public abstract void Attack();
    
    // + jay
    [Header("사운드")]
    [SerializeField] protected AudioClip _fireSound;
    protected SoundPlayer _fire;

    private void FireSoundOn()
    {
        _fire = SoundManager.Instance.TakeSoundPlayer();
        
        if (_fire == null)
            return;

        _fire.SetSoundVolume(1f)
             .SetSoundLoop(false)
             .PlaySoundWhenStart(false)
             .ConvertSourceToClip(_fireSound)
             .Play();
        
        //StartCoroutine(SoundOffRoutine(_fire, _fireSound.length));
    }

    private IEnumerator SoundOffRoutine(SoundPlayer player, float time)
    {
        yield return new WaitForSeconds(time);
        player.Stop();
    }
}
