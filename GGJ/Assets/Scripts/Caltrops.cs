using System.Collections;
using UnityEngine;

public class Caltrops : MonoBehaviour
{
    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        StartCoroutine("fadeOut");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            
        }
    }

    IEnumerator fadeOut()
    {
        yield return new WaitForSeconds(2f);

        for (float i = 1f; i >= -0.05f; i -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = i;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
    }


}
