using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour, IPoolable
{
    public GameObject GameObject { get; set; }
    public ObjectPool Source { get; set; }
    private BulletData _data;
    private Rigidbody _rb;

    private void Awake() => CacheComponenets();
    
    public void SetData(BulletData data, Vector3 position, Quaternion rotation)
    {
        _data = data;
        transform.position = position;
        transform.rotation = rotation;
        _rb.velocity = transform.forward * _data.Speed;
        _rb.angularVelocity = Vector3.zero;

        StartCoroutine(DisableRoutine());
    }

    private IEnumerator DisableRoutine()
    {
        yield return new WaitForSeconds(_data.ReturnDelay);
        ReturnToPool();
        // +
        ExplodeSoundOn();
        yield return new WaitForSeconds(_data.ReturnDelay);
        ExplodeSoundOff();
    }

    public void ReturnToPool()
    {
        Source.Push(this);
    }
    
    // +
    private SoundPlayer _explode;

    private void ExplodeSoundOn()
    {
        _explode = SoundManager.Instance.TakeSoundPlayer();

        if (_explode == null)
        {
            return;
        }
        
        _explode.SetSoundVolume(1f)
                .SetSoundLoop(false)
                .PlaySoundWhenStart(false)
                .ConvertSourceToClip(_data.ExplodeSound)
                .Play();
    }

    private void ExplodeSoundOff()
    {
        _explode.Stop();
        _explode = null;
    }

    private void CacheComponenets()
    {
        _rb = GetComponent<Rigidbody>();
    }
}