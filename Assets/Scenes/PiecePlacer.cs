using UnityEngine;

public class PiecePlacer : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out Column column))
                {
                    int x = column.x;
                    int z = column.z;
                    int y = GameManager.Instance.board.GetAvailableY(x, z);

                    if (y == -1)
                    {
                        GameManager.Instance.messageText.text = "this line is full.";
                        return;
                    }

                    float spacing = GameManager.Instance.ySpacing;
                    float radius = spacing / 2f;
                    Vector3 spawnPos = new Vector3(column.transform.position.x, y * spacing + radius, column.transform.position.z);

                    GameManager.Instance.PlacePieceAtPosition(spawnPos, x, y, z);

                    // ★別の列がクリックされたらメッセージを消す
                    if (GameManager.Instance.messageText.text == "this line is full.")
                    {
                        GameManager.Instance.messageText.text = "";
                    }
                }
            }
        }
    }
}
