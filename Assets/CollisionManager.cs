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
        monster.Die();
    }

    private void HandleMonsterBumpsIntoBoy(Monster monster, Boy boy)
    {
        // so far nop
    }
}
