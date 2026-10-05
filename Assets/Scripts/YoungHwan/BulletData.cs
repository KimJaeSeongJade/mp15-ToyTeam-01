using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData : MonoBehaviour
{
    [Header("공통")]
    [SerializeField] private BulletType _type;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _returnDelay;
    [Header("런처 전용")]
    [SerializeField] private float _explosionRadius;
    [SerializeField] private AudioClip _explodeSound;
    [Header("롤링볼 전용")]
    [SerializeField] private float _hitRadius;
    [SerializeField] private float _rollSpeed;
    
    public BulletType Type => _type;
    public int Damage => _damage;
    public float Speed => _speed;
    public float ReturnDelay => _returnDelay;
    public float ExplosionRadius => _explosionRadius;
    public float HitRadius => _hitRadius;
    public float RollSpeed => _rollSpeed;
    public AudioClip ExplodeSound => _explodeSound;
}