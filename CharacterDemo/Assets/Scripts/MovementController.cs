using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    [SerializeField] float speedForwards = 5f;
    [SerializeField] float speedBackwards = 2f;
    [SerializeField] float rotateSpeed = 90f;
    public GameObject followTransform;

    CharacterController characterController;
    private Animator animator;

    //private Rigidbody rb;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RotationLocked())
        {
            ApplyRotationFromMouse();
        } else {
            ApplyRotationFromKeyboard();
        }

        Vector3 direction = CalculateDirection();

        float curSpeed = speedForwards; 
        if (Input.GetAxisRaw("Vertical") < 0) {
            curSpeed = speedBackwards;
        }
        
        characterController.SimpleMove(direction * curSpeed);

        SetAnimationValues();

        
    }

    private void SetAnimationValues()
    {
        if (RotationLocked()) {
            animator.SetBool("side", Input.GetAxisRaw("Horizontal") != 0);
            animator.SetBool("mirror", Input.GetAxisRaw("Horizontal") > 0);
        } else  {
            animator.SetBool("side", false);
            animator.SetBool("mirror", false);
        }

        animator.SetBool("forwards", Input.GetAxisRaw("Vertical") > 0);
        animator.SetBool("backwards", Input.GetAxisRaw("Vertical") < 0);
    }

    private void ApplyRotationFromKeyboard()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);
    }

    private void ApplyRotationFromMouse()
    {
        var xAngle = followTransform.transform.localEulerAngles.x;
        transform.rotation *= Quaternion.AngleAxis(followTransform.transform.localEulerAngles.y, Vector3.up);
        followTransform.transform.localEulerAngles = new Vector3(xAngle, 0, 0);
    }

    Vector3 CalculateDirection()
    {
        Vector3 direction = new Vector3(0, 0, 0);
        
        direction += Vector3.forward * Input.GetAxisRaw("Vertical");
        if (RotationLocked()) {
            direction += Vector3.right * Input.GetAxisRaw("Horizontal");
        }
        
        direction = transform.TransformDirection(direction.normalized);

        return direction.normalized;
    }

    bool RotationLocked() {
        return Input.GetAxis("MB1") > 0;
    }
}
