using System.Collections;
using UnityEngine;

public class PiperCombat : Hero
{
    private float _lightAttackDelay = 0;
    private float _heavyAttackDelay = 1;

    public ObjectPool LApool;
    public ObjectPool HApool;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private float _heavyShotChargeTime;

    private float _heavyShotChargeTimer;

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
                StopMoving();
                _heavyShotChargeTimer += Time.deltaTime;
            }
            if (Input.GetMouseButtonUp(1))
            {
                if (_heavyShotChargeTimer >= _heavyShotChargeTime)
                {
                    HeavyAttack();
                    _heavyShotChargeTimer = 0;
                }
                else
                {
                    _heavyShotChargeTimer = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Utility();
            }
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

    public override void HeavyAttack()
    {
        if (CurrentHeroStamina >= HeavyAttackCost && _timeSinceLastHeavyAttack >= HeavyAttackCooldown)
        {
            CurrentHeroStamina -= HeavyAttackCost;
            _timeSinceLastHeavyAttack = 0;
            
            StartCoroutine(HeavyShot());
        }            
    }

    private IEnumerator HeavyShot()
    {
        // Play Animation
        GameManager.Instance.Player.LockInput = true;
        yield return new WaitForSeconds(_heavyAttackDelay);  //Animation Duration = delay      

        GameObject bullet = HApool.GetPooledObjects();

        if (bullet != null)
        {
            bullet.transform.position = _firePoint.position;
            Bullet shot = bullet.GetComponent<Bullet>();
            bullet.SetActive(true);
            shot.direction = _firePoint.right * Mathf.Sign(GameManager.Instance.Player.gameObject.transform.localScale.x);

        }

        Debug.Log("HeavyShot");
        GameManager.Instance.Player.LockInput = false;
    }

    public override void Utility()
    {
        if (CurrentHeroStamina >= UtilityCost && _timeSinceLastUtility >= UtilityCooldown)
        {
            CurrentHeroStamina -= UtilityCost;
            _timeSinceLastUtility = 0;

            Instantiate(_caltoprs, transform.position, new Quaternion());
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
