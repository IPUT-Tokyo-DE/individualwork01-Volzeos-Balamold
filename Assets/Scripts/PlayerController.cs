using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private bool isActive = true; // 操作可能かどうかを管理するフラグ
    public GameObject GameOverText; // ゲームオーバーテキストの参照

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Update()
    {
        // 操作が無効な場合、Update処理をスキップ
        if (!isActive) return;

        // 追記: 範囲制限
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -6, 6),
            Mathf.Clamp(transform.position.y, -2, 1.5f)
        );

        // 移動スクリプト
        if (Input.GetKeyDown(KeyCode.LeftArrow))   // 左移動
        {
            this.transform.Translate(new Vector2(-3f, 0f));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))  // 右移動
        {
            this.transform.Translate(new Vector2(3f, 0f));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))     // 上移動
        {
            this.transform.Translate(new Vector2(0f, 3.5f));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))   // 下移動
        {
            this.transform.Translate(new Vector2(0f, -3.5f));
        }

        // 距離チェック
        CheckDistanceToObjects();
    }

    void CheckDistanceToObjects()
    {
        // FireタグとHunmmerタグのオブジェクトをすべて取得
        GameObject[] fireObjects = GameObject.FindGameObjectsWithTag("Fire");
        GameObject[] hunmmerObjects = GameObject.FindGameObjectsWithTag("Hunmmer");

        // 距離をチェック
        foreach (GameObject obj in fireObjects)
        {
            if (IsWithinDistance(obj))
            {
                DisableAndDestroy();
                return;
            }
        }

        foreach (GameObject obj in hunmmerObjects)
        {
            if (IsWithinDistance(obj))
            {
                DisableAndDestroy();
                return;
            }
        }
    }

    bool IsWithinDistance(GameObject target)
    {
        // 自分と対象オブジェクトのX座標とY座標の距離を計算
        float distanceX = Mathf.Abs(transform.position.x - target.transform.position.x);
        float distanceY = Mathf.Abs(transform.position.y - target.transform.position.y);

        // どちらの距離も1未満であればtrueを返す
        return distanceX < 1f && distanceY < 1f;
    }

    void DisableAndDestroy()
    {
        isActive = false; // 操作を無効化
        Destroy(gameObject); // 現在のオブジェクトを削除
        GameOverText.SetActive(true); // ゲームオーバーテキストを表示
    }
}