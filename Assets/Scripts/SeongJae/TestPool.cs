using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPool : MonoBehaviour
{
    [SerializeField] private ObjectPool _objectPool;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) _objectPool.Take();
    }
}
