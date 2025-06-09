using UnityEngine;

public class KnifeThrow : MonoBehaviour
{
    [SerializeField] float throwPower = 10f;
    [SerializeField] float despawnDistance = 12f;

    public GameObject aimLineObject;
    
    private Rigidbody2D _rb;
    private bool isActive = true;

    private bool rotateLeft = false;
    private bool rotateRight = false;
    [SerializeField] float rotationSpeed = 90f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(AutoDespawn()); // optional fallback
    }

    void Update()
    {
        if (!isActive) return;

        // Rotate while holding buttons
        if (rotateLeft)
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        else if (rotateRight)
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);

        // Offscreen position check
        if (Mathf.Abs(transform.position.x) > despawnDistance || Mathf.Abs(transform.position.y) > despawnDistance)
        {
            Missed();
        }
    }

    public void SetRotateLeft(bool state) => rotateLeft = state;
    public void SetRotateRight(bool state) => rotateRight = state;

    public void ThrowKnife()
    {
        if (!isActive) return;
  
        Vector2 dir = transform.up.normalized; // assuming knife faces UP in the sprite
        _rb.velocity = Vector2.zero;
        _rb.constraints = RigidbodyConstraints2D.None;
        _rb.AddForce(dir * throwPower, ForceMode2D.Impulse);
        
        if (aimLineObject != null)
            aimLineObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive) return;

        if (collision.gameObject.CompareTag("Log"))
        {
            isActive = false;
            _rb.velocity = Vector2.zero;
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;

            transform.rotation = Quaternion.identity; // <- Snap to upright
            transform.SetParent(collision.transform);

            LogHealth.instance.UpdateHealth();
            KnifeSpawner.instance.SpawnKnife();
        }

        else if (collision.gameObject.CompareTag("Knife"))
        {
            isActive = false;
            GameManager.instance.GameOver();
            LogHealth.instance.GameOverAudio();
        }else if (collision.gameObject.CompareTag("Reflective"))
        {
            // Get normal of the first contact point
            Vector2 normal = collision.contacts[0].normal;

            // Reflect current velocity across the normal
            _rb.velocity = Vector2.Reflect(_rb.velocity, normal);

            // Optional: add small boost to keep speed consistent after bounce
            _rb.velocity = _rb.velocity.normalized * throwPower;
        }
    }

    private void Missed()
    {
        if (!isActive) return;
        isActive = false;
        KnifeSpawner.instance.SpawnKnife();
        Destroy(gameObject);
    }

    private System.Collections.IEnumerator AutoDespawn()
    {
        yield return new WaitForSeconds(5f);
        Missed(); // backup in case knife flies forever
    }
}
