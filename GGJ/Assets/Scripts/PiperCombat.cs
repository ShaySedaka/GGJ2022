using UnityEngine;

public class PiperCombat : Hero
{
    [SerializeField]
    float CoolDownLA;

    private float lastLA;

    [SerializeField]
    float CoolDownHA;

    private float lastHA;

    public ObjectPool LApool;
    public ObjectPool HApool;

    [SerializeField]
    Transform FirePoint;

    [SerializeField]
    private float HeavyChargeTime;

    private float HeavyChargeTimer;

   

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            LightAttack();
        }

        if (Input.GetMouseButton(1))
        {
            HeavyChargeTimer += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (HeavyChargeTimer >= HeavyChargeTime)
            {
                HeavyAttack();
                HeavyChargeTimer = 0;
            }
            else
            {
                HeavyChargeTimer = 0;
            }
        }
    }
    public override void HeavyAttack()
    {
        if (Time.time - lastHA < CoolDownHA)
        {
            return;
        }
        lastHA = Time.time;
        GameObject bullet = HApool.GetPooledObjects();
        if (bullet != null)
        {
            bullet.transform.position = FirePoint.position;
            Bullet shot = bullet.GetComponent<Bullet>();
            bullet.SetActive(true);
            shot.direction = FirePoint.right;
        }
    }

    public override void LightAttack()
    {

        if (Time.time - lastLA < CoolDownLA)
        {
            return;
        }
        lastLA = Time.time;

        GameObject bullet = LApool.GetPooledObjects();
        if (bullet != null)
        {
            bullet.transform.position = FirePoint.position;
            Bullet shot = bullet.GetComponent<Bullet>();
            bullet.SetActive(true);
            shot.direction = FirePoint.right;
            
        }


    }

    public override void Utility()
    {
        throw new System.NotImplementedException();
    }


}
