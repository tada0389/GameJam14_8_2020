using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float jumpPower = 1f;

    [SerializeField]
    private float speed = 1f;

    private Rigidbody rb;

    [SerializeField]
    private float maxSpeed = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 add = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow)) add += Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow)) add += Vector3.right;
        if (Input.GetKey(KeyCode.UpArrow)) add += Vector3.forward;
        if (Input.GetKey(KeyCode.DownArrow)) add += Vector3.back;

        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        rb.AddForce(add * speed);
        var v = rb.velocity;
        v.x = Mathf.Clamp(v.x, -maxSpeed, maxSpeed);
        v.z = Mathf.Clamp(v.z, -maxSpeed, maxSpeed);
        rb.velocity = v;
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
}
