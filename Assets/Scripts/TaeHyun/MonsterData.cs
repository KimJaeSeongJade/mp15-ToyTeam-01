using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : MonoBehaviour
{
    [Header("몬스터의 종류 (SMALL, MIDDE, BIG)")]
    public MonsterType Type;

    [Header("몬스터 이동 속도")]
    public float MoveSpeed;

    [Header("몬스터 체력")]
    public float Health;

    [Header("몬스터 넥서스 피해량")]
    public float Damage;

    [Header("몬스터 처치 시 점수")]
    public int Score;
}
