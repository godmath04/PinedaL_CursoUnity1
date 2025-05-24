using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 5.0f;
    void Start()
    {
        transform.position = new Vector3(3, 0, 0); // Set initial position to origin

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); 
        float verticalInput = Input.GetAxis("Vertical"); 
        transform.Translate(Vector3.right * speed * horizontalInput *Time.deltaTime); 
        transform.Translate(Vector3.up * speed * verticalInput* Time.deltaTime); 

    }
}
