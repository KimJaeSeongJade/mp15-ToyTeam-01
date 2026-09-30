using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int _size;
    [SerializeField] private IPoolable[] _objectPool;
    [SerializeField] private GameObject _prefab;

    private int _count;

    private void Start() => Init();

    /// <summary>
    /// 오브젝트 풀을 초기화 합니다.
    /// </summary>
    private void Init()
    {
        _objectPool = new IPoolable[_size];
        _count = _size;

        for(int i = 0; i < _objectPool.Length; i++)
        {
            GameObject temp = Instantiate(_prefab);
            temp.SetActive(false);
            _objectPool[i] = temp.GetComponent<IPoolable>();
            _objectPool[i].GameObject = temp;
            _objectPool[i].Source = this;
        }
    }

    /// <summary>
    /// ObjectPool에서, Ipoolable 인터페이스를 반환시킵니다.
    /// ObjectPool이 비어있다면, 작동하지 않습니다.
    /// </summary>
    /// <returns></returns>
    public IPoolable Take()
    {
        if(_count >= 1)
        {
            _count--;
            _objectPool[_count].GameObject.SetActive(true);
            return _objectPool[_count];
        }

        return null;
    }

    /// <summary>
    /// ObjectPool에 사용한 IPoolable 객체를 반납합니다.
    /// ObjectPool이 가득 차 있다면, 반납하지 않습니다.
    /// </summary>
    /// <param name="poolable">오브젝트 풀에 반납할 객체</param>
    public void Push(IPoolable poolable)
    {
        if (_count == _size) return;

        poolable.GameObject.SetActive(false);
        _objectPool[_count] = poolable;
        _count++;

        Debug.Log(_count);
    }
}
