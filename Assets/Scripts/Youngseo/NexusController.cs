using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusController : MonoBehaviour, IDamageable
{
    [Header("몬스터 레이어 설정")]
    [SerializeField] private LayerMask _monsterLayer;

    private NexusData _nexusData;

    private void Awake() => CacheComponents();
    private void Start() => SetData(_nexusData.MaxHealth);
    private void Update()
    {
        //TODO: 현재는 테스트용으로 하드코딩 되어있습니다. 추후에 몬스터 쪽에서 Nexus의 TakeDamage를 불러주면 됩니다.
        if(Input.GetKeyDown(KeyCode.Space)) TakeDamage(100);
    }

    public void TakeDamage(float damage)
    {
        if(damage >= _nexusData.CurrentHealth)
        {
            _nexusData.CurrentHealth = 0;
        }
        else
        {
            _nexusData.CurrentHealth -= damage;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if((_monsterLayer & (1 << other.gameObject.layer)) != 0)
        {
            other.gameObject.GetComponent<MonsterController>().ArriveNexus();
            TakeDamage(other.gameObject.GetComponent<MonsterData>().Damage);
        }
    }

    private void Init()
    {
        _nexusData.CurrentHealth = _nexusData.MaxHealth;
    }

    private void CacheComponents()
    {
        _nexusData = GetComponent<NexusData>();
    }

    public void SetData(float maxHealth)
    {
        _nexusData.MaxHealth = maxHealth;
        Init();
    }
}
