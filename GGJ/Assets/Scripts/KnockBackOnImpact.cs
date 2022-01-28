using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackOnImpact : MonoBehaviour
{

    [SerializeField]
    private float KnockBackStrenth;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggered");
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb!=null)
        {
            Vector3 direction = collision.transform.position - transform.position;
            rb.AddForce(direction.normalized * KnockBackStrenth, ForceMode2D.Impulse);
        }
    }
}
