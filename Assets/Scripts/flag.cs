using UnityEngine;
using System.Collections;

public class DuplicateSelfWithRandomArrayPosition : MonoBehaviour
{
    public string targetTag = "Player"; // 対象となるタグ
    public float thresholdX = 1.0f; // X軸距離の閾値
    public float thresholdY = 1.0f; // Y軸距離の閾値

    // X軸とY軸の複製位置候補
    public float[] duplicationXPositions = { -6, -3, 0, 3, 6 };
    public float[] duplicationYPositions = { -2, 1.5f };

    private bool hasDuplicated = false; // 複製が行われたかのフラグ
    private Vector3 lastPosition = Vector3.zero; // 最後に生成した座標を記録
    private const float positionTolerance = 0.01f; // 位置比較の許容誤差

    void Update()
    {
        // すでに複製が行われていれば処理をスキップ
        if (hasDuplicated) return;

        // ターゲットの位置を毎フレーム取得し、更新
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject target in targets)
        {
            // ターゲットと現在のオブジェクトとの距離を計算
            Vector3 distance = target.transform.position - transform.position;

            // X軸とY軸の距離が閾値未満であれば複製と削除の処理を開始
            if (Mathf.Abs(distance.x) < thresholdX && Mathf.Abs(distance.y) < thresholdY)
            {
                hasDuplicated = true; // フラグを立てる
                StartCoroutine(DuplicateAndDestroy());
                return; // 処理を終了
            }
        }
    }

    IEnumerator DuplicateAndDestroy()
    {
        Vector3 newPosition;

        // ランダムなXとYの位置を選択（直前の座標と異なるものを選ぶ）
        do
        {
            float randomX = duplicationXPositions[Random.Range(0, duplicationXPositions.Length)];
            float randomY = duplicationYPositions[Random.Range(0, duplicationYPositions.Length)];
            newPosition = new Vector3(randomX, randomY, transform.position.z);
        }
        while (IsApproximatelyEqual(newPosition, lastPosition)); // 前回の位置と近似している場合は再選択

        lastPosition = newPosition; // 新しい座標を記録

        // 自身を複製
        Instantiate(gameObject, newPosition, Quaternion.identity);

        // 0.5秒の猶予を与える
        yield return new WaitForSeconds(0.5f);

        // 現在のオブジェクトを削除
        Destroy(gameObject);
    }

    // 位置の比較を行うためのヘルパーメソッド
    private bool IsApproximatelyEqual(Vector3 a, Vector3 b)
    {
        return Mathf.Abs(a.x - b.x) < positionTolerance &&
               Mathf.Abs(a.y - b.y) < positionTolerance &&
               Mathf.Abs(a.z - b.z) < positionTolerance;
    }
}