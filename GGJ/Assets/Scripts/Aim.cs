using UnityEngine;

public class Aim : MonoBehaviour
{
    public bool AimToMouse;
    Vector3 difference;
    private void FixedUpdate()
    {
        if (AimToMouse)
        {
            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        }
        else
        {
            difference = GameManager.Instance.Player.transform.position - transform.position;
        }
        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }
}
