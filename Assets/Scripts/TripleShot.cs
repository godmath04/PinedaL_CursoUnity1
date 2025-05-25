using UnityEngine;

public class TripleShot : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.y >= 6f)
        {
            Destroy(gameObject);
        }
    }
}
