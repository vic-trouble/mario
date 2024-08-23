using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public GameObject target_;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target_)
        {
            var monster = gameObject.GetComponent<Unit>();
            if (target_.transform.position.x > gameObject.transform.position.x)
            {
                monster.Move(1, Time.deltaTime);
            }
            else
            {
                monster.Move(-1, Time.deltaTime);
            }
        }
    }
}
