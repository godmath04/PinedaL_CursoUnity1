using UnityEngine;

public class Powerup : MonoBehaviour
{
    private float _speed = 3.0f;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Usa tags, es más robusto
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.ActivateTripleShot(5f);  // Activar triple shot
                Destroy(this.gameObject);    // Eliminar powerup del juego
            }
        }
    }
}
