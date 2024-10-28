using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(3, 3, 1);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-3, 3, 1);

        if (Input.GetKey(KeyCode.Space) && grounded) 
            Jump();

        // Set animator parameters
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("Jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
            grounded = true;
    }
}
