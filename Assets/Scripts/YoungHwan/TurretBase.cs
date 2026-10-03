using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretBase : MonoBehaviour
{
    [SerializeField] protected TurretType _type;
    [SerializeField] protected Transform _firePoint;
    [SerializeField] protected ObjectPool _bulletPool;
    [SerializeField] protected BulletData _data;
    [SerializeField] protected float _fireDelay;
    
    private bool _canFire = true;
    public TurretType Type => _type;

    public void Fire()
    {
        if (!_canFire) return;
        StartCoroutine(FireRoutine());
    }

    private IEnumerator FireRoutine()
    {
        _canFire = false;
        Attack();
        FireSoundOn();
        
        yield return new WaitForSeconds(_fireDelay);
        _canFire = true;
        
        // + jay
        FireSoundOff();
    }

    public void SpawnBullet()
    {
        BulletController bullet = _bulletPool.Take() as BulletController;
        if (bullet == null) return;
        
        bullet.SetData(_data, _firePoint.position, _firePoint.rotation);
    }

    public abstract void Attack();
    
    // + jay
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
    }

    private void FireSoundOff()
    {
        if (_fire == null) return;
        _fire.Stop();
        _fire = null;
    }
}
