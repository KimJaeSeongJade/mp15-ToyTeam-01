using System;
using UnityEngine;
public class MonsterController : MonoBehaviour, IDamageable, IPoolable
{
    [Header("몬스터 데이터")]
    private MonsterData _monsterData;

    [Header("탄알 레이어")]
    private LayerMask _bulletLayer;

    // =============== IPoolable 구현 ===============
    public GameObject GameObject { get; set; }
    public ObjectPool Source { get; set; }

    public event Action<MonsterData> OnKilled;
    public event Action<MonsterData> OnNexusArrived;

    private bool _isReturned = true;

    // =============== 유니티 생명 주기 ===============

    private void Awake() => CacheComponents();
    private void OnEnable() => _isReturned = false;
    private void FixedUpdate() => MoveToNexus();
    private void OnDisable() => _isReturned = true;

    private void OnTriggerEnter(Collider other)
    {
        if((_bulletLayer & (1 << other.gameObject.layer)) != 0){
            TakeDamage(other.GetComponent<BulletData>().Damage);
        }
    }

    // =============== 기본 이동 로직 ===============
    private void MoveToNexus()
    {
        //이동 로직을 단순히 앞으로 이동하는 것으로 수정하였습니다. (추후 발판 등 도입)
        transform.Translate(Vector3.forward * _monsterData.MoveSpeed * Time.deltaTime);
    }

    // =============== Nexus 도착 시 수행할 로직 ===============
    public void ArriveNexus()
    {   
        OnNexusArrived?.Invoke(_monsterData);
        OnNexusArrived = null;
        OnKilled = null;
        gameObject.SetActive(false);
        ReturnToPool();
    }

    // =============== IDamageable 구현 ===============
    public void TakeDamage(float damage)
    {
        if (damage >= _monsterData.Health)
        {
            _monsterData.Health = 0;
            OnKilled?.Invoke(_monsterData);
            OnNexusArrived = null;
            OnKilled = null;
            ReturnToPool();
        }
        else
        {
            _monsterData.Health -= damage;
        }
    }

    // =============== IPoolable 구현 ===============

    public void ReturnToPool()
    {
        if (_isReturned) return;

        Source.Push(this);
    }

    // =============== Awake() 초기화 메서드 ===============

    private void CacheComponents()
    {
        _monsterData = GetComponent<MonsterData>();
    }
}
