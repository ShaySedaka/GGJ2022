using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    private InputHandler input;

    [SerializeField]
    private float MoveSpeed;

    private Rigidbody2D rb;

    [SerializeField]
    private float checkRadius = 0.4f;

    [SerializeField]
    LayerMask Ground;

    public Transform GroundCheck;

    [SerializeField]
    private Camera cam;

    void Start()
    {
        input = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool grounded = Physics.CheckSphere(GroundCheck.position, checkRadius, Ground);
        var targetVector = new Vector3(input.InputVector.x, 0, input.InputVector.y);
        if (grounded)
        {
            MoveTowardTarget(targetVector);
        }
    }

    void MoveTowardTarget(Vector3 target)
    {
        target = Quaternion.Euler(0, cam.gameObject.transform.rotation.eulerAngles.y, 0) * target;
        rb.velocity = target * MoveSpeed;
    }

}
