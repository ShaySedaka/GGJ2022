using System.Collections;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    SpriteRenderer rendy;
    private void Start()
    {
        rendy = GetComponent<SpriteRenderer>();
        Color c = rendy.material.color;
        c.a = 0f;
        rendy.material.color = c;
        StartCoroutine("Fade");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Debug.Log("player went boom");
        }
    }


    IEnumerator Fade()
    {
        for (float i = 0.05f; i <= 1; i += 0.05f)
        {
            Color c = rendy.material.color;
            c.a = i;
            rendy.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
}
