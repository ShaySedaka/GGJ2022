using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caltrops : MonoBehaviour
{
    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Color c = rend.material.color;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            //poison enemy?
        }
    }

    IEnumerator fadeOut()
    {
        for (float i = 1; i < 0; i -= 0.05f)
        {
            float c = rend.material.color.a;
            c -= i;
            yield return new WaitForSeconds(0.05f);
        }
    }


}
