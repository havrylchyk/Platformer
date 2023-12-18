using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float ySpeed;
    [SerializeField] private CharacterController conn;

    public int lives = 3;

    void Start()
    {
        conn = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (lives > 0)
        {
            float horizontalMove = Input.GetAxis("Horizontal");
            float verticalMove = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(horizontalMove, 0, verticalMove);
            moveDirection.Normalize();
            float magnitude = moveDirection.magnitude;
            magnitude = Mathf.Clamp01(magnitude);
            conn.SimpleMove(moveDirection * magnitude * speed);

            ySpeed += Physics.gravity.y * Time.deltaTime;
            if (Input.GetButtonDown("Jump") && conn.isGrounded)
            {
                ySpeed = jumpSpeed;
            }

            Vector3 vel = moveDirection * magnitude;
            vel.y = ySpeed;
            conn.Move(vel * Time.deltaTime);

            if (conn.isGrounded)
            {
                ySpeed = -0.5f;
            }

            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotate = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            Debug.Log("Game Over");
        }
    }

    public void TakeDamage()
    {
        lives--;
        Debug.Log("Lives left: " + lives);

        if (lives <= 0)
        {
            Debug.Log("Game Over");
        }
    }

    // Add this method to get the current number of lives
    public int GetLives()
    {
        return lives;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            TakeDamage();
            Destroy(other.gameObject);
        }
    }
}