using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab; // プレハブをInspectorで設定
    public Transform spawnPoint;  // 出現位置（省略可能）

    void Update()
    {
        // スペースキーで出現
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBall();
        }
    }

    void SpawnBall()
    {
        Vector3 position = spawnPoint != null ? spawnPoint.position : transform.position;
        Instantiate(ballPrefab, position, Quaternion.identity);
    }
}
