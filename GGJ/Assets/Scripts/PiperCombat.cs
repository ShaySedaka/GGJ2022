using UnityEngine;

public class PiperCombat : Hero
{



    [SerializeField]
    private float _coolDownLA;

    private float _lastLA;

    [SerializeField]
    private float _coolDownHA;

    private float _lastHA;

    public ObjectPool LApool;
    public ObjectPool HApool;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private float _heavyChargeTime;

    private float _heavyChargeTimer;

    [SerializeField]
    private GameObject _caltoprs;

    private void Update()
    {
        base.Update();
        if (!GameManager.Instance.Player.LockInput)
        {
            if (Input.GetMouseButton(0))
            {
                LightAttack();
            }

            if (Input.GetMouseButton(1))
            {
                _heavyChargeTimer += Time.deltaTime;
            }
            if (Input.GetMouseButtonUp(1))
            {
                if (_heavyChargeTimer >= _heavyChargeTime)
                {
                    HeavyAttack();
                    _heavyChargeTimer = 0;
                }
                else
                {
                    _heavyChargeTimer = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Utility();
            }
        }
            

    }
    public override void HeavyAttack()
    {
        if(CurrentHeroStamina >= HeavyAttackCost)
        {
            
            
            if (Time.time - _lastHA < _coolDownHA)
            {
                return;
            }
            _lastHA = Time.time;
            GameObject bullet = HApool.GetPooledObjects();
            if (bullet != null)
            {
                bullet.transform.position = _firePoint.position;
                Bullet shot = bullet.GetComponent<Bullet>();
                bullet.SetActive(true);
                shot.direction = _firePoint.right;
            }

            CurrentHeroStamina -= HeavyAttackCost;
        }        
    }

    public override void LightAttack()
    {
        if (CurrentHeroStamina >= LightAttackCost)
        {
            if (Time.time - _lastLA < _coolDownLA)
            {
                return;
            }
            _lastLA = Time.time;

            GameObject bullet = LApool.GetPooledObjects();
            if (bullet != null)
            {
                bullet.transform.position = _firePoint.position;
                Bullet shot = bullet.GetComponent<Bullet>();
                bullet.SetActive(true);
                shot.direction = _firePoint.right;

            }
            CurrentHeroStamina -= LightAttackCost;
        }

    }

    public override void Utility()
    {
        if (CurrentHeroStamina >= UtilityCost)
        {
            Instantiate(_caltoprs, transform.position, new Quaternion());
            CurrentHeroStamina -= UtilityCost;
        }
    }


}
