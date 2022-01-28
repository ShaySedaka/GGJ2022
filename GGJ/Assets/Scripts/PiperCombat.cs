using System.Collections;
using UnityEngine;

public class PiperCombat : Hero
{
    private float _lightAttackDelay = 0;
    private float _heavyAttackDelay = 1.3f;


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

    [SerializeField]
    public bool BackOffUnlocked;

    protected override void Update()
    {
        if (!GameManager.Instance.Player.LockInput)
        {
            base.Update();
            if (Input.GetMouseButtonDown(0))
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
        if (CurrentHeroStamina >= LightAttackCost && _timeSinceLastLightAttack >= LightAttackCooldown)
        {
            CurrentHeroStamina -= LightAttackCost;
            _timeSinceLastLightAttack = 0;
            StopMoving();
            StartCoroutine(LightShot());
        }   
    }

    private IEnumerator LightShot()
    {
        // Play Animation
        GameManager.Instance.Player.LockInput = true;
        yield return new WaitForSeconds(_lightAttackDelay);  //Animation Duration = delay      

        GameObject bullet = LApool.GetPooledObjects();
        if (bullet != null)
        {
            bullet.transform.position = _firePoint.position;
            Bullet shot = bullet.GetComponent<Bullet>();
            bullet.SetActive(true);
            shot.direction = _firePoint.right * Mathf.Sign(GameManager.Instance.Player.gameObject.transform.localScale.x);

        }

        Debug.Log("LightShot");
        GameManager.Instance.Player.LockInput = false;
    }

    public override void Utility()
    {
        if (CurrentHeroStamina >= UtilityCost)
        {
            Instantiate(_caltoprs, transform.position, new Quaternion());
            CurrentHeroStamina -= UtilityCost;
        }
    }

    public void UpgradeArms(int damageIncrease)
    {
        _lightAttackDamage += damageIncrease;
    }

    public void UpgradePersistence(float regenIncrease)
    {
        HeroStaminaRegenerate += regenIncrease;
    }

    public void UpgradeSwiftness(float movementIncrease)
    {
        HeroMovementSpeed += movementIncrease;
    }


}
