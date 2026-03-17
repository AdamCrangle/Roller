using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    [Header("Movement")]
    public float rollForce = 10f;

    [Header("Input")]
    public InputActionReference moveAction;

    private Rigidbody rb;
    private Vector2 moveInput;
    private int partsCollected;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        partsCollected = 0;
    }

    private void OnEnable()
    {
        if (moveAction != null)
            moveAction.action.Enable();
    }

    private void OnDisable()
    {
        if (moveAction != null)
            moveAction.action.Disable();
    }

    private void Update()
    {
        if (moveAction != null)
            moveInput = moveAction.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        rb.AddForce(moveDirection * rollForce, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Parts"))
        {
            partsCollected++;
            Debug.Log("Parts Collected: " + partsCollected);
            Destroy(other.gameObject);
        }
        else { }
    }
}