using System;
using UnityEngine;
public class MonsterController : MonoBehaviour, IDamageable, IPoolable
{
    [Header("몬스터 데이터")]
    private MonsterData _monsterData;

    [Header("현재 넥서스 참조")]
    [SerializeField] private NexusController _nexus;

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
    // =============================================
    // =============== 넥서스 참조 메서드 ===============

    public void SetNexus(NexusController nexus)
    {
        _nexus = nexus;
    }

    // =============== 기본 이동 로직 ===============
    private void MoveToNexus()
    {
        transform.position = Vector3.MoveTowards(transform.position, _nexus.transform.position, _monsterData.MoveSpeed * Time.deltaTime);
        float distance = Vector3.Distance(transform.position, _nexus.transform.position);
        if (distance <= 0.5f)
        {
            ArriveNexus();
        }
    }

    // =============== Nexus 도착 시 수행할 로직 ===============
    private void ArriveNexus()
    {   
        OnNexusArrived?.Invoke(_monsterData);
        OnNexusArrived = null;
        _nexus.TakeDamage(_monsterData.Damage);
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
