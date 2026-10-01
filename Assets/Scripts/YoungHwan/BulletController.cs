using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour, IPoolable
{
    private int _damage;
    private float _speed;
    private float _returnDelay;
    
    public GameObject GameObject { get; set; }
    public ObjectPool Source { get; set; }
    public Transform tr { get => transform; }

    public void ReturnToPool()
    {
        Source.Push(this);
    }

    public void SetData(int damage, float speed, float returnDelay)
    {
        _damage = damage;
        _speed = speed;
        _returnDelay = returnDelay;
    }
}
