using UnityEngine;

public class PlayerLooks : MonoBehaviour
{
    public float mouseSensitivity = 100f;      // マウス感度
    public Transform playerBody;               // プレイヤー本体（左右回転）

    private float xRotation = 0f;              // カメラ上下角度

    void Start()
    {
        // マウスカーソルを非表示＆ゲーム画面内にロック
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // マウスの移動量を取得（毎フレーム）
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 上下の回転角を更新（マウスYは逆方向に回転するのでマイナス）
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 真上・真下を超えないよう制限

        // カメラの上下回転（local）
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // プレイヤー本体を左右に回転
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
