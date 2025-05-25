using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public bool canTripleShot = false;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.25f;

    private float _canFire = 0.0f;

    [SerializeField]
    private float _speed = 5.0f;

    void Start()
    {
        transform.position = new Vector3(3, 0, 0);
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

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Debug para revisar si se está moviendo solo
        Debug.Log("Horizontal: " + horizontalInput + " | Vertical: " + verticalInput);

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
