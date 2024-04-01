using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Camera myCamera;
    private Rigidbody myRigidbody;
    private float speed = 8;
    private float rotateSpeed = 2;
    private Vector3 offset;
    private float cameraRotate;

    void Start()
    {
        myRigidbody = this.GetComponent<Rigidbody>();
        offset = this.transform.position;
    }

    private void Update()
    {

    }

void FixedUpdate()
{
    Vector3 vel = myRigidbody.velocity;

    float v = Input.GetAxis("Vertical");
    float h = Input.GetAxis("Horizontal");
    if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f)
    {
        float sr = Mathf.Sin(cameraRotate);
        float cr = Mathf.Cos(cameraRotate);
        myRigidbody.velocity = new Vector3((v * sr + h * cr) * speed, vel.y, (v * cr - h * sr) * speed);
        transform.rotation = Quaternion.LookRotation(new Vector3((v * sr + h * cr), 0, (v * cr - h * sr)));
    }
}

void LateUpdate()
{
    myCamera.transform.position += this.transform.position - offset;
    offset = this.transform.position;

    float mouseX = Input.GetAxis("Mouse X") * rotateSpeed;

    myCamera.transform.RotateAround(this.transform.position, Vector3.up, mouseX);

    cameraRotate = myCamera.transform.eulerAngles.y / 180 * Mathf.PI;
}
}

