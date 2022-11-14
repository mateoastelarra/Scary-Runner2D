using System.Collections;
using UnityEngine;

public class obstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObstacle());
    }


    private IEnumerator SpawnObstacle()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, obstacles.Length);
            float minTime = 0.9f;
            float maxTime = 1.9f;
            float randomTime = Random.Range(minTime, maxTime);

            Instantiate(obstacles[randomIndex], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(randomTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
      
    }
 
}
