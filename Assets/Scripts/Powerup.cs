using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private int powerUPID; // 0 = triple shot, 1 = speed, 2 = otro

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                if (powerUPID == 0)
                {
                    player.ActivateTripleShot(5f);
                }
                else if (powerUPID == 1)
                {
                    player.ActivateSpeedBoost(5f, 9f); // Velocidad aumentada por 5 segundos
                }
                else if (powerUPID == 2)
                {
                    player.ActivateShield(5f); // Escudo activo por 5 segundos
                }

                Destroy(this.gameObject);
            }
        }
    }
}
