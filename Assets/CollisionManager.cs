using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public static CollisionManager Instance()
    {
        return FindObjectOfType<CollisionManager>();
    }

    public void HandleBoyJumpsOn(Boy boy, GameObject obj)
    {
        Monster monster = obj.GetComponent<Monster>();
        if (monster)
        {
            HandleBoyJumpsOnMonster(boy, monster);
        }
    }

    public void HandleMonsterBumpsInto(Monster monster, GameObject obj)
    {
        Boy boy = obj.GetComponent<Boy>();
        if (boy)
        {
            HandleMonsterBumpsIntoBoy(monster, boy);
        }
    }

    private void HandleBoyJumpsOnMonster(Boy boy, Monster monster)
    {
        boy.Jump(0.25f);
        monster.Die();
    }

    private void HandleMonsterBumpsIntoBoy(Monster monster, Boy boy)
    {
        boy.Die();
    }

    public void HandleBoomerang(Boomerang boomerang, GameObject obj)
    {
        var monster = obj.GetComponent<Monster>();
        if (monster)
        {
            HandleBoomerangHitsMonster(boomerang, monster);
        }

        var boy = obj.GetComponent<Boy>();
        if (boy)
        {
            HandleBoomerangHitsBoy(boomerang, boy);
        }
    }

    private void HandleBoomerangHitsMonster(Boomerang boomerang, Monster monster)
    {
        if (boomerang.IsFatalVelocity())
        {
            monster.Die();
        }
    }

    private void HandleBoomerangHitsBoy(Boomerang boomerang, Boy boy)
    {
        boy.CollectBoomerang();
        Destroy(boomerang.gameObject);
    }

    public void HandleCheckPointReached(CheckPoint checkPoint, GameObject obj)
    {
        var boy = obj.GetComponent<Boy>();
        if (boy)
        {
            HandleCheckPointReachedByBoy(checkPoint, boy);
        }
    }

    private void HandleCheckPointReachedByBoy(CheckPoint checkPoint, Boy boy)
    {
        checkPoint.Win();
    }
}
