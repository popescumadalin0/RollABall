using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float force;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public Transform camera;

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
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector3(camera.right.x, 0, camera.right.z).normalized * speed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(new Vector3(camera.forward.x, 0, camera.forward.z).normalized * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector3(camera.right.x, 0, camera.right.z).normalized * (-speed));
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(new Vector3(camera.forward.x, 0, camera.forward.z).normalized * (-speed));
        }
    }

    void Update()
    {
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

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 25)
        {
            winTextObject.SetActive(true);
        }
    }
}
