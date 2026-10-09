using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimationEvent : MonoBehaviour
{
  public void ReturnToPool()
    {
        MonsterController monster = GetComponentInParent<MonsterController>();

        if (monster == null)
        {
            return;
        }

        if (monster.Source != null)
        {
            monster.ReturnToPool();
        }
        else
        {
            monster.gameObject.SetActive(false);
        }
    }
}
