using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer ==7)
        {
            //deal dmg to player 
        }
    }

    private void Start()
    {
        Invoke("destroy", 1f);    
    }


     void destroy()
    {
        Destroy(gameObject);
    }
}
