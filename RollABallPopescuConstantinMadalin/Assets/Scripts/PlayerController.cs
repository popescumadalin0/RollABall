using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float force;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private float movementX;
    private float movementY;

    private Rigidbody rb;
    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        count = 0;

        SetCountText();

        winTextObject.SetActive(false);
    }

    void FixedUpdate()
    {
        var movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    void Update()
    {
        Debug.Log($"cout: {count} \n velocity: {rb.velocity.y}\n");
        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0)
        {
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            count += 1;

            SetCountText();
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();

        movementX = v.x;
        movementY = v.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 25)
        {
            winTextObject.SetActive(true);
        }
    }
}
