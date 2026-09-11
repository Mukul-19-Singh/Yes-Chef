using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody rb;
    private Vector3 movement;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }

    private void Update()
    {
        // Does not allow player movement if the game is not in a playing state
        if (GameManager.Instance != null && !GameManager.Instance.IsPlaying) return;
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        // Captures input (WASD or Arrow Keys)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        movement.y = 0f;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsPlaying) return;
        // Moves the Rigidbody based on input and speed
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
