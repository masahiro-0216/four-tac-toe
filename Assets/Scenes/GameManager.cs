using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI messageText;

    public GameObject ballBlack;
    public GameObject ballWhite;

    public Transform boardParent;

    [Header("高さの間隔（1段ごとのY座標）")]
    public float ySpacing = 1.0f;  // ← ここで高さ調整できる

    private int currentPlayer = 1; // 1: Black, 2: White
    public Board board;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Awake()
    {
        Instance = this;
        board = new Board();
    }

    public void PlacePieceAtPosition(Vector3 basePosition, int x, int y, int z)
    {
        GameObject prefab = (currentPlayer == 1) ? ballBlack : ballWhite;

        // 見た目の積み高さを ySpacing に応じて調整
        Vector3 spawnPos = new Vector3(basePosition.x, y * ySpacing + 1, basePosition.z);

        Instantiate(prefab, spawnPos, Quaternion.identity, boardParent);

        board.SetCell(x, y, z, currentPlayer);

        if (board.CheckWin(x, y, z, currentPlayer))
        {
            Debug.Log($"Player {currentPlayer} wins!");
            
            messageText.text = $"{currentPlayer} win !"; // ★ここで表示
            return;
        }

        currentPlayer = (currentPlayer == 1) ? 2 : 1;
    }

    

    
}
