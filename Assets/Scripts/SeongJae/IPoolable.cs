using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    public GameObject GameObject { get; set; }
    public ObjectPool Source { get; set; }

    /// <summary>
    /// 인터페이스에서 공용으로 구현을 요구하고 있는 메서드 입니다.
    /// 반환할 때 할 행동을 지정하시고, Source.Push(this); 를 작성 하시면 오브젝트를 반납 합니다.
    /// </summary>
    public void ReturnToPool();
}
