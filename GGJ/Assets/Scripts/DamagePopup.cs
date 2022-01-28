using System.Collections;
using TMPro;
using UnityEngine;


public class DamagePopup : MonoBehaviour
{
    public TextMeshPro TextObj;
    private void Start()
    {
        Destroy(gameObject, 0.8f);
    }
    public void PopDmg(float dmg)
    {
        TextObj.text = dmg.ToString();
        transform.localPosition += new Vector3(0, 0.5f, 0);
    }
    


}
