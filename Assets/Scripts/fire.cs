using UnityEngine;

public class Hammer : MonoBehaviour
{
    public float moveDistance = 1.0f; // 移動する距離
    public float moveInterval = 1.0f; // 移動する間隔（秒）

    public float duplicateInterval = 5.0f; // 複製する間隔（秒）
    public float duplicateYPosition = 0.0f; // 複製する際のY座標を手動で設定

    public float deleteThresholdY = -10.0f; // 一定のY座標に達したらオブジェクトを削除する閾値
    public float waitTimeBeforeDestroy = 2.0f; // 一定座標に達してから待機する時間（秒）

    private float timeSinceLastMove = 0.0f; // 最後に移動した時からの経過時間
    private float timeSinceLastDuplicate = 0.0f; // 最後に複製した時からの経過時間
    private float timeWaiting = 0.0f; // 待機時間の経過時間

    private bool hasDuplicated = false; // 複製されたかどうかをチェックするフラグ
    private bool isWaiting = false; // 待機中かどうかのフラグ

    // X座標候補のリスト
    private int[] xPositions = new int[] { -6, -3, 0, 3, 6 };

    void Update()
    {
        // 移動の時間を更新
        timeSinceLastMove += Time.deltaTime;
        // 複製の時間を更新
        timeSinceLastDuplicate += Time.deltaTime;

        // 待機時間を更新
        if (isWaiting)
        {
            timeWaiting += Time.deltaTime;

            // 待機時間が経過したらオブジェクトを削除
            if (timeWaiting >= waitTimeBeforeDestroy)
            {
                Destroy(gameObject);
            }

            return; // 待機中は他の処理をスキップ
        }

        // 移動処理
        if (timeSinceLastMove >= moveInterval)
        {
            // オブジェクトを下に移動
            transform.position += Vector3.down * moveDistance;

            // 最後に移動した時間をリセット
            timeSinceLastMove = 0.0f;
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

        // 一定のY座標に達したらオブジェクトを待機状態にする
        if (transform.position.y <= deleteThresholdY && !isWaiting)
        {
            isWaiting = true; // 待機開始
        }
    }
}
