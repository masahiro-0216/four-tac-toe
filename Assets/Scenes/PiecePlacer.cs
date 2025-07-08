using UnityEngine;

public class PiecePlacer : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Column column = hit.collider.GetComponent<Column>();
                if (column != null)
                {
                    int x = column.x;
                    int z = column.z;

                    int y = GameManager.Instance.board.GetAvailableY(x, z);
                    if (y == -1)
                    {
GameManager.Instance.messageText.text = "This line is full."; // ★ここで表示
    return;
                    }

                    float ySpacing = GameManager.Instance.ySpacing;
                    float radius = ySpacing / 2;
                    Vector3 spawnPos = new Vector3(column.transform.position.x, y * GameManager.Instance.ySpacing + radius, column.transform.position.z);

                    GameManager.Instance.PlacePieceAtPosition(spawnPos, x, y, z);
                }
            }
            else
            {
                Debug.Log("Raycast did not hit anything.");
            }
        }
    }
}
