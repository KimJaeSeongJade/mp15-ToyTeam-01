using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 몬스터 타입을 열거형 정의합니다.
public enum Monstertype
{
    small,
    middle,
    big
}
public class MonsterController : MonoBehaviour, IDamageable
{
    [Header("Monster Settings")]
    [SerializeField] private Monstertype _monsterType;
    [SerializeField] private float _moveSpeed = 5f;
    /// <summary>
    /// 방어물 이름 제가 임시로 NexusPoint라고 지었습니다. 나중에 방어물 이름이 정해지면 다시 바꾸겠습니다.
    /// </summary>
    [SerializeField] private Transform _nexusPoint;  
    [SerializeField] private float _arriveDistance = 0.5f; 
    private float _currentHealth;
    /// <summary>
    /// 몬스터가 사망했는지 혹은 넥서스에 도착했는지를 체크하는 변수로 
    /// 두 상태가 중복으로 발생하지 않도록 방지합니다
    /// </summary>
    private bool _isResolved; 
    /// <summary>
    /// 나중에 몬스터 사망시 점수 획득을 위한 이벤트 부분입니다.
    /// </summary>
    public event Action<MonsterController> OnKilled;
    /// <summary>
    /// 나중에 몬스터가 방어물에 도착했을 때 이벤트를 발생시키기 위한 부분입니다.
    /// </summary>
    public event Action<MonsterController> OnNexusArrived;
    // 몬스터 활성화 시 상태 초기화
    public void OnEnable()
    {
        ResetState();
    }
    public void Update()
    {
        MoveToNexus();
    }
    /// <summary>
    ///  넥서스 위치 전달 하고 몬스터 초기화 합니다
    /// </summary>
    public void Initialize(Transform nexusPoint)
    {
        _nexusPoint = nexusPoint;
        ResetState();
    } 
    /// <summary>
    /// 기존에 있던 초기화 함수 따로 빼줬습니다.
    /// </summary>
    public void ResetState()
    {
        _currentHealth = GetMaxHealth();
        _isResolved = false;
    }
    /// <summary>
    /// 몬스터 데미지 처리 함수입니다 
    /// 0되면 사망처리 시키고 죽은 친구나 넥서스에 들어간 친구의 중복처리 방지합니다.
    /// </summary>
    public void TakeDamage(float damage)
    {
        if (_isResolved)
        {
            return;
        }
        //데미지 받을때 체력이 0 밑으로 내려가서 표기되는걸 방지합니다. 
        _currentHealth = Mathf.Max(_currentHealth - damage, 0f);
        if(_currentHealth <= 0f)
        {
            MonsterDeath();
        }
        //Debug.Log($"Monster took {damage} damage. Current health: {_currentHealth}");
        //몬스터 데미지 처리후 현재 체력 로그입니다 필요하실때 켜서 사용하시면 됩니다.
    }
    /// <summary>
    /// 몬스터 이동 처리 함수입니다
    /// 오브젝트가 생성되어 있는가, 넥서스 포인트가 존재하는가를 체크 한 후에 이동시킵니다.
    /// </summary>
    public void MoveToNexus()
    {
        if(_isResolved || _nexusPoint == null)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, 
        _nexusPoint.position, _moveSpeed * Time.deltaTime);
        //거리 측정하고 도착판정 범위를 검사하는 부분입니다
        float distance = Vector3.Distance(transform.position, _nexusPoint.position);
        if(distance <= _arriveDistance)
        {
           ArriveNexus();
        }
    }
    /// <summary>
    /// inspector에서 몬스터 타입에 따라 최대 체력을 반환하는 함수입니다.
    /// </summary>
    public float GetMaxHealth()
    {
        switch (_monsterType)
        {
            case Monstertype.small:
                return 50f;

            case Monstertype.middle:
                return 100f;

            case Monstertype.big:
                return 200f;

            default:
                return 0f;
        }
    }
    /// <summary>
    /// inspector에서 몬스터 타입에 따라 점수 획득량을 반환하는 함수입니다.
    /// </summary>
    public int GetScoreAmount()
    {
        switch (_monsterType)
        {
            case Monstertype.small:
                return 10;

            case Monstertype.middle:
                return 20;

            case Monstertype.big:
                return 50;

            default:
                return 0;
        }
    }
    /// <summary>
    /// inspector에서 몬스터 타입에 따라 넥서스에 가하는 데미지를 반환하는 함수입니다.
    /// </summary>
    public float GetNexusDamage()
    {
        switch (_monsterType)
        {
            case Monstertype.small:
                return 5f;

            case Monstertype.middle:
                return 10f;

            case Monstertype.big:
                return 20f;

            default:
                return 0f;
        }
    }
    /// <summary>
    /// 몬스터 사망처리 함수입니다. 사망시 이벤트를 발생시키고 오브젝트를 비활성화 시킵니다.
    /// </summary>
    private void MonsterDeath()
    {   
        if (_isResolved)
        {
            return;
        }
        _isResolved = true;
        // 사망시 점수 획득을 위한 이벤트 발생 
        // if(scoreManager != null)
        // {
        //     scoreManager.AddScore(GetScoreAmount());
        // }
        OnKilled?.Invoke(this);
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 넥서스 도착 처리 함수입니다. 도착시 이벤트를 발생시키고 오브젝트를 비활성화 시킵니다.
    /// </summary>
    private void ArriveNexus()
    {   
        if (_isResolved)
        {
            return;
        }
        _isResolved = true;
         // 도착시 체력감소를 위한 이벤트 발생 
        // if(nexusHealth != null)
        // {
        //     nexusHealth.TakeDamage(GetNexusDamage());
        // }
        OnNexusArrived?.Invoke(this);
        gameObject.SetActive(false);
    }
    









    




}
