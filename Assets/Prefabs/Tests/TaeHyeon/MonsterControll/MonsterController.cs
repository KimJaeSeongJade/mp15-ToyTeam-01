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

public class MonsterController : MonoBehaviour
{
    [Header("Monster Settings")]

    [SerializeField] private Monstertype monsterType;

    [SerializeField] private float moveSpeed = 5f;

    /// <summary>
    /// 방어물 이름 제가 임시로 NexusPoint라고 지었습니다. 나중에 방어물 이름이 정해지면 다시 바꾸겠습니다.
    /// </summary>
    [SerializeField] private UnityEngine.Transform NexusPoint;  

    [SerializeField] private float arriveDistance = 0.5f; 

    private float currentHealth;

    private bool isResolved; 

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
        currentHealth = GetMaxHealth();
        isResolved = false;

    }


    public void Update()
    {
        MoveToNexus();
    }


    /// <summary>
    ///  넥서스 위치 전달 하고 몬스터 초기화 합니다
    /// </summary>
    private void Initialize(UnityEngine.Transform nexusPoint)
    {
        NexusPoint = nexusPoint;
        currentHealth = GetMaxHealth();
        isResolved = false;
    } 

    /// <summary>
    /// 몬스터 데미지 처리 함수입니다 
    /// 0되면 사망처리 시키고 죽은 친구나 넥서스에 들어간 친구의 중복처리 방지합니다.
    /// </summary>
    public void TakeDamage(float damage)
    {
        if (isResolved)
        {
            return;
        }
        
        currentHealth -= damage;

        if(currentHealth <= 0f)
        {
            MonsterDeath();
        }
    }

    /// <summary>
    /// 몬스터 이동 처리 함수입니다
    /// 오브젝트가 생성되어 있는가, 넥서스 포인트가 존재하는가를 체크 한 후에 이동시킵니다.
    /// </summary>
    public void MoveToNexus()
    {
        if(isResolved || NexusPoint == null)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, 
        NexusPoint.position, moveSpeed * Time.deltaTime);

        //거리 측정하고 도착판정 범위를 검사하는 부분입니다
        float distance = Vector3.Distance(transform.position, NexusPoint.position);

        if(distance <= arriveDistance)
        {
           ArriveNexus();
        }
    }


    /// <summary>
    /// inspector에서 몬스터 타입에 따라 최대 체력을 반환하는 함수입니다.
    /// </summary>
    private float GetMaxHealth()
    {
       
        
        switch (monsterType)
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
    /// 몬스터 사망처리 함수입니다. 사망시 이벤트를 발생시키고 오브젝트를 비활성화 시킵니다.
    /// </summary>
    private void MonsterDeath()
    {
        isResolved = true;
        OnKilled?.Invoke(this); 
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 넥서스 도착 처리 함수입니다. 도착시 이벤트를 발생시키고 오브젝트를 비활성화 시킵니다.
    /// </summary>
    private void ArriveNexus()
    {
        isResolved = true;
        OnNexusArrived?.Invoke(this);
        gameObject.SetActive(false);
    }










    




}
