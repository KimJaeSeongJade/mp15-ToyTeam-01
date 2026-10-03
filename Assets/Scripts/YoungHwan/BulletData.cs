using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _returnDelay;
    // +
    [SerializeField] private AudioClip _explodeSound;
    
    public int Damage => _damage;
    public float Speed => _speed;
    public float ReturnDelay => _returnDelay;
    // +
    public AudioClip ExplodeSound => _explodeSound;
}