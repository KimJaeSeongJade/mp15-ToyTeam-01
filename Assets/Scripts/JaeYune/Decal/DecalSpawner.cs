using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalSpawner : MonoBehaviour
{
    public static DecalSpawner Instance { get; private set; }
    [SerializeField] private ObjectPool _smallPool;
    [SerializeField] private ObjectPool _middlePool;
    [SerializeField] private ObjectPool _bigPool;
    [SerializeField] private float _groundY = 0f;

    private void Awake() => Instance = this;

    public void Spawn(MonsterType type, Vector3 pos)
    {
        ObjectPool pool = type switch
        {
            MonsterType.SMALL => _smallPool,
            MonsterType.MIDDLE => _middlePool,
            MonsterType.BIG => _bigPool
        };
        
        if (pool == null) 
            return;

        DeathDecal decal = pool.Take() as DeathDecal;
        
        if (decal == null) 
            return;  
        
        decal.Show(pos, _groundY);
    }
}
