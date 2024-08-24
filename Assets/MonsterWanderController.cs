using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWanderController : MonoBehaviour
{
    public int direction_ = -1;
    public float HIT_CHECK_COOLDOWN = 0.25f;
    private float hitCheckCooldown_;

    void Start()
    {
        hitCheckCooldown_ = HIT_CHECK_COOLDOWN;
    }

    void Update()
    {
        var monster = gameObject.GetComponent<Monster>();
        if (monster.GetFaceHit())
        {
            hitCheckCooldown_ -= Time.deltaTime;
            if (hitCheckCooldown_ < 0)
            {
                hitCheckCooldown_ = HIT_CHECK_COOLDOWN;
                direction_ = direction_ == 1 ? -1 : 1;
            }
        }

        monster.Move(direction_, Time.deltaTime);
    }
}
