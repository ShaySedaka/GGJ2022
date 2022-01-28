using UnityEngine;

public class Aim : MonoBehaviour
{
    public bool AimToMouse;
    public Transform Target;
    Vector3 difference;
    private void FixedUpdate()
    {
        if (AimToMouse)
        {
            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        }
        else
        {
            difference = Target.position - transform.position;
        }
        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }
}
