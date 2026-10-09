using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDecal : MonoBehaviour, IPoolable
{
    private float _lifeTime = 1f;

    public GameObject GameObject { get; set; }
    public ObjectPool Source { get; set; }

    private void OnEnable() => Invoke(nameof(ReturnToPool), _lifeTime);
    private void OnDisable() => CancelInvoke();

    public void Show(Vector3 pos, float groundY)
    {
        transform.SetPositionAndRotation(
            new Vector3(pos.x, groundY + 0.01f, pos.z),
            Quaternion.Euler(90f, 0f, 0f));
    }

    public void ReturnToPool()
    {
        if (!gameObject.activeSelf) 
            return;
        
        Source.Push(this);
    }
}
