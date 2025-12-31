using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 7f;
    public float sprintMultiplier = 2.8f;
    public float rotateSpeed = 100f;

    private Rigidbody rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float rotateInput = Input.GetAxis("Horizontal");

        bool sprinting = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
        float currentSpeed = moveSpeed * (sprinting ? sprintMultiplier : 1f);

        float rotateAmount = rotateInput * rotateSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, rotateAmount, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

        float moveAmount = moveInput * currentSpeed * Time.fixedDeltaTime;
        Vector3 moveVector = transform.forward * moveAmount;
        rb.MovePosition(rb.position + moveVector);

        float speedPercent = Mathf.Clamp01(Mathf.Abs(moveInput));
        anim.SetFloat("Speed", speedPercent);
        anim.SetBool("isSprinting", sprinting);
    }

    void Update()
    {
        // ✅ Attack Input — Trigger Punch Animation
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("Attack");
        }
    }
}
