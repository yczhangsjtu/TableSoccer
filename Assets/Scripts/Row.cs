using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour {

    public float movingSpeed = 10;
    public float maxOffset = 2;
    public float rotatingSpeed = 180;
    private float targetOffset;
    private float fixedX;
    private float fixedY;
    private float fixedRotateX;
    private float fixedRotateY;

    // Use this for initialization
    void Start () {
        targetOffset = 0;
        fixedX = transform.localPosition.x;
        fixedY = transform.localPosition.y;
        fixedRotateX = transform.localRotation.x;
        fixedRotateY = transform.localRotation.y;
        GetComponent<Rigidbody>().maxAngularVelocity = 100;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPosition = new Vector3(
            fixedX,
            fixedY,
            targetOffset
        );
        GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(
            transform.localPosition,
            targetPosition,
            movingSpeed * Time.deltaTime
        ));
        if(Mathf.Abs(transform.localRotation.x - fixedRotateX) > 0.01 ||
           Mathf.Abs(transform.localRotation.y - fixedRotateY) > 0.01) {
            transform.localRotation = Quaternion.Euler(fixedRotateX, fixedRotateY, 0);
        }
    }

    public void Move(float offset) {
        targetOffset += offset;
        if(targetOffset > maxOffset)
            targetOffset = maxOffset;
        if(targetOffset <- maxOffset)
            targetOffset = -maxOffset;
    }

    public void Rotate(float angle) {
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, angle);
    }
}
