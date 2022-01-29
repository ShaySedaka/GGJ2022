using System.Collections;
using UnityEngine;

public class Caltrops : MonoBehaviour
{
    private SpriteRenderer rend;
    private float elapsedTime = 0;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        StartCoroutine("fadeOut");
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
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

    private void OnTriggerStay2D(Collider2D collider)
    {

            if(collider.gameObject.layer == 8)
            {
                Debug.Log("plspls");
                collider.gameObject.GetComponent<Enemy>().GetAttacked(5);
            }
    }


}
