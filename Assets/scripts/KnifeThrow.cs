using System.Collections.Generic;
using UnityEngine;

public class KnifeThrow : MonoBehaviour
{
    [SerializeField] float throwPower = 10f;
    [SerializeField] float despawnDistance = 12f;

    private Rigidbody2D _rb;
    private bool _isActive = true;

    private bool _rotateLeft = false;
    private bool _rotateRight = false;
    [SerializeField] float rotationSpeed = 90f;

    private List<Vector2> _pathPoints;
    private int _pathIndex = 0;
    [SerializeField] float travelSpeed = 10f;

    [SerializeField] AimLineController aimLinePrefab;
    private AimLineController _aimLine;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(AutoDespawn()); // optional fallback

        _aimLine = Instantiate(aimLinePrefab); // Create a unique aimline
        _aimLine.SetKnife(this.transform); // Link to this knife
    }

    void Update()
    {
        if (!_isActive) return;

        // Rotate while holding buttons
        if (_rotateLeft)
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        else if (_rotateRight)
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);

        // Offscreen position check
        if (Mathf.Abs(transform.position.x) > despawnDistance || Mathf.Abs(transform.position.y) > despawnDistance)
        {
            Missed();
        }

        // Handle path movement
        if (!_isActive || _pathPoints == null || _pathIndex >= _pathPoints.Count)
            return;

        Vector2 target = _pathPoints[_pathIndex];
        Vector2 moveDir = (target - (Vector2)transform.position).normalized;
        float step = travelSpeed * Time.deltaTime;

        if (Vector2.Distance(transform.position, target) < step)
        {
            transform.position = target;
            _pathIndex++;
        }
        else
        {
            transform.position += (Vector3)(moveDir * step);
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public void SetRotateLeft(bool state) => _rotateLeft = state;
    public void SetRotateRight(bool state) => _rotateRight = state;


    public void ThrowKnife()
    {
        if (!_isActive || _aimLine == null) return;

        _pathPoints = new List<Vector2>(_aimLine.GetReflectionPath());
        _pathIndex = 1;
        _isActive = true;

        _rb.bodyType = RigidbodyType2D.Kinematic;
        _rb.velocity = Vector2.zero;

        _aimLine.Hide(); // hide after throw
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isActive) return;

        if (collision.gameObject.CompareTag("Log"))
        {
            _isActive = false;

            _rb.velocity = Vector2.zero;
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
            _rb.bodyType = RigidbodyType2D.Dynamic;
            transform.rotation = Quaternion.identity;
            transform.SetParent(collision.transform);

            LogHealth.instance.UpdateHealth();
            KnifeSpawner.instance.SpawnKnife();
        }
        
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            _isActive = false;
            GameManager.instance.GameOver();
            LogHealth.instance.GameOverAudio();
        }

        else if (collision.gameObject.CompareTag("Knife"))
        {
            _isActive = false;
            GameManager.instance.GameOver();
            LogHealth.instance.GameOverAudio();
        }
        
        else if (collision.gameObject.CompareTag("Reflective"))
        {
            // Get normal from contact point
            Vector2 normal = collision.contacts[0].normal;

            // Reflect velocity direction
            Vector2 incomingDir = _rb.velocity.normalized;
            Vector2 reflectDir = Vector2.Reflect(incomingDir, normal);

            // Set new velocity in reflected direction
            _rb.velocity = reflectDir * throwPower;

            // Freeze angular rotation if it's spinning
            _rb.freezeRotation = true;
            _rb.angularVelocity = 0;

            // Rotate the knife to match the new direction
            float angle = Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void Missed()
    {
        if (!_isActive) return;
        _isActive = false;
        KnifeSpawner.instance.SpawnKnife();
        Destroy(gameObject);
    }

    private System.Collections.IEnumerator AutoDespawn()
    {
        yield return new WaitForSeconds(5f);
        Missed(); // backup in case knife flies forever
    }
}