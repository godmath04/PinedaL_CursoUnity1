using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyExplotionPrefab;
    
    [SerializeField]
    private float _speed = 4.0f;

    private float _minX = -8.0f;
    private float _maxX = 8.0f;
    private float _resetY = 7.0f;
    private float _minY = -5.5f;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < _minY)
        {
            float randomX = Random.Range(_minX, _maxX);
            transform.position = new Vector3(randomX, _resetY, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            Instantiate(_enemyExplotionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage();
            }

            Instantiate(_enemyExplotionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject); // El enemigo también muere al chocar

        }
    }
}
