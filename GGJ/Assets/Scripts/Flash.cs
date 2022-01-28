using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    Color original;
    SpriteRenderer rend;
    public float flashDuration;
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        original = rend.material.color;
    }

    public void GetHit()
    {
        StartCoroutine("Hit");
    }

    IEnumerator Hit()
    {
        rend.material.color = new Color(1,0,0);
        yield return new WaitForSeconds(flashDuration);
        rend.material.color = original;
    }


}
