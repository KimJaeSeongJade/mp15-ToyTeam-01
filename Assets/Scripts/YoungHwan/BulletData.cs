using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData : MonoBehaviour
{
    [SerializeField] private BulletType _type;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _returnDelay;
    [SerializeField] private float _explosionRadius;
    // +
    [SerializeField] private AudioClip _explodeSound;
    
    public BulletType Type => _type;
    public int Damage => _damage;
    public float Speed => _speed;
    public float ReturnDelay => _returnDelay;
    public float ExplosionRadius => _explosionRadius;

    // +
    public AudioClip ExplodeSound => _explodeSound;
}