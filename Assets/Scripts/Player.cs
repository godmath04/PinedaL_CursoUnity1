using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public bool canTripleShot = false;
    public bool shieldsActive = false;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private GameObject _shieldGameObject;

    [SerializeField]
    private float _fireRate = 0.25f;

    private float _canFire = 0.0f;

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private int _lives = 3;

    private UIManager _uiManager;

    void Start()
    {
        transform.position = new Vector3(3, 0, 0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager != null)
        {
            _uiManager.UpdateLives(_lives);
        }
    }

    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            if (canTripleShot)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.00f, 0), Quaternion.identity);
            }

            _canFire = Time.time + _fireRate;
        }
    }

    public void ActivateTripleShot(float duration)
    {
        StartCoroutine(TripleShotCooldown(duration));
    }

    private IEnumerator TripleShotCooldown(float duration)
    {
        canTripleShot = true;
        yield return new WaitForSeconds(duration);
        canTripleShot = false;
    }

    public void ActivateSpeedBoost(float duration, float newSpeed)
    {
        StartCoroutine(SpeedBoostCooldown(duration, newSpeed));
    }

    private IEnumerator SpeedBoostCooldown(float duration, float newSpeed)
    {
        float originalSpeed = _speed;
        _speed = newSpeed;
        yield return new WaitForSeconds(duration);
        _speed = originalSpeed;
    }

    public void ActivateShield(float duration)
    {
        StartCoroutine(ShieldCooldown(duration));
    }

    private IEnumerator ShieldCooldown(float duration)
    {
        shieldsActive = true;
        _shieldGameObject.SetActive(true);
        Debug.Log("�Escudo activado!");
        yield return new WaitForSeconds(duration);
        shieldsActive = false;
        _shieldGameObject.SetActive(false);
        Debug.Log("Escudo desactivado");
    }

    public void TakeDamage()
    {
        if (shieldsActive)
        {
            Debug.Log("�Da�o bloqueado por el escudo!");
            return;
        }

        _lives--;
        _uiManager.UpdateLives(_lives);

        Debug.Log("Vida restante: " + _lives);

        if (_lives <= 0)
        {
            Debug.Log("Juego terminado");
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            // ? Detener el spawn
            GameObject spawnManager = GameObject.Find("SpawnManager");
            if (spawnManager != null)
            {
                spawnManager.GetComponent<SpawnManager>().StopSpawning();
            }

            Destroy(this.gameObject);
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime;
        transform.Translate(movement);

        if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }
        else if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0f, 0);
        }

        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }
}
