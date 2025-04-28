using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Hunmmer : MonoBehaviour
{
    public float moveDistance = 1.0f; // 移動する距離
    public float moveInterval = 1.0f; // 移動する間隔（秒）

    public float rotateAngle = 45.0f; // 回転する角度
    public float rotateInterval = 2.0f; // 回転する間隔（秒）

    public float duplicateInterval = 5.0f; // 複製する間隔（秒）
    public float duplicateYPosition = 0.0f; // 複製する際のY座標を手動で設定

    public float deleteThresholdY = -10.0f; // 一定のY座標に達したらオブジェクトを削除する閾値

    private float timeSinceLastMove = 0.0f; // 最後に移動した時からの経過時間
    private float timeSinceLastRotate = 0.0f; // 最後に回転した時からの経過時間
    private float timeSinceLastDuplicate = 0.0f; // 最後に複製した時からの経過時間

    private bool hasDuplicated = false; // 複製されたかどうかをチェックするフラグ

    // X座標候補のリスト
    private int[] xPositions = new int[] { -6, -3, 0, 3, 6 };

    void Update()
    {
        // 移動の時間を更新
        timeSinceLastMove += Time.deltaTime;
        // 回転の時間を更新
        timeSinceLastRotate += Time.deltaTime;
        // 複製の時間を更新
        timeSinceLastDuplicate += Time.deltaTime;

        // 移動処理
        if (timeSinceLastMove >= moveInterval)
        {
            // オブジェクトを下に移動
            transform.position += Vector3.down * moveDistance;

            // 最後に移動した時間をリセット
            timeSinceLastMove = 0.0f;
        }

        // 回転処理（Z軸回り）
        if (timeSinceLastRotate >= rotateInterval)
        {
            // Z軸を中心に回転
            transform.Rotate(Vector3.forward, rotateAngle);

            // 最後に回転した時間をリセット
            timeSinceLastRotate = 0.0f;
        }

        // 複製処理
        if (timeSinceLastDuplicate >= duplicateInterval && !hasDuplicated)
        {
            // 複製のX座標をランダムに選ぶ
            int randomX = xPositions[Random.Range(0, xPositions.Length)];

            // 複製を行う
            Instantiate(gameObject, new Vector3(randomX, duplicateYPosition, transform.position.z), Quaternion.identity);

            // 複製されたことを記録（自己増殖を防ぐ）
            hasDuplicated = true;

            // 最後に複製した時間をリセット
            timeSinceLastDuplicate = 0.0f;
        }

        // 一定のY座標に達したらオブジェクトを削除
        if (transform.position.y <= deleteThresholdY)
        {
            Destroy(gameObject);
        }
    }
}
