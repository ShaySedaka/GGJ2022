using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeAttack : MonoBehaviour
{
    int dmg;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer ==7)
        {
            Debug.Log("dealt dmg");
            GameManager.Instance.Player.PlayerGlobalStatsRef.CurrentHero.TakeDamage(dmg);
        }
    }

    public void SetDmg(int amount)
    {
        dmg = amount;
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
