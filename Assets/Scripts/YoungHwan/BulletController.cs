using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour, IPoolable
{
    public GameObject GameObject { get; set; }
    public ObjectPool Source { get; set; }

    private float _moveSpeed = 5.0f;
    private float _lifeTime = 1.0f;

    WaitForSeconds waitForDisable;

    private void OnEnable()
    {
        // 만약 총알이라면, 데이터는 Turret 쪽에서 할당할 것입니다.
        // 만약 몬스터라면, 데이터는 Wave 쪽에서 할당할 것입니다.
        SetData();
        StartCoroutine(DiasbleRoutine());
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
    }

    private IEnumerator DiasbleRoutine()
    {
        yield return waitForDisable;
        ReturnToPool();
    }

    public void SetData()
    {
        transform.position = new Vector3(0, 0, 0);
        waitForDisable = new WaitForSeconds(_lifeTime);
    }

    public void ReturnToPool()
    {
        Source.Push(this);
    }
}