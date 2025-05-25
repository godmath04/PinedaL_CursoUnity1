using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public GameObject player;
    private UIManager uiManager;

    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
        if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                gameOver = false;

                // ? Reiniciar spawn
                GameObject spawnManager = GameObject.Find("SpawnManager");
                if (spawnManager != null)
                {
                    spawnManager.GetComponent<SpawnManager>().StartSpawning();
                }

                uiManager.HideTitleScreen();
            }
        }
    }
}
