using TMPro;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI messageText;
    public GameObject ballBlack;
    public GameObject ballWhite;
    public Transform boardParent;

    public float ySpacing = 1f;

    public bool isGameOver = false; // ← ★追加：操作ロック用

    private int currentPlayer = 1;
    public Board board;

    void Awake()
    {
        Instance = this;
        board = new Board();
    }

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlacePieceAtPosition(Vector3 position, int x, int y, int z)
    {
        if (isGameOver) return; // ★すでに勝利済みなら玉を置かせない

        GameObject prefab = (currentPlayer == 1) ? ballBlack : ballWhite;
        Instantiate(prefab, position, Quaternion.identity, boardParent);

        board.SetCell(x, y, z, currentPlayer);

        if (board.CheckWin(x, y, z, currentPlayer))
        {
            isGameOver = true;
            messageText.text = $"player{currentPlayer} win.";
            StartCoroutine(RestartGameAfterDelay(3f)); // ★3秒後にリセット
            return;
        }

        currentPlayer = (currentPlayer == 1) ? 2 : 1;
    }

    private IEnumerator RestartGameAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ResetGame();
    }

    private void ResetGame()
    {
        isGameOver = false;
        board = new Board(); // 新しい盤面に差し替え
        currentPlayer = 1;
        messageText.text = "";

        foreach (Transform child in boardParent)
        {
            Destroy(child.gameObject); // 玉を全部削除
        }
    }
}
