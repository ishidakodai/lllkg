using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemy : MonoBehaviour
{
    Transform playerTransform;//Playerのトランスフォーム

    [SerializeField] float distance = 1f; //プレイヤーに近づける距離
    [SerializeField] float speed = .0001f; //移動速度

    private Vector3 lastPosition;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").
            transform; //プレイヤーのトランスフォーム取得
        lastPosition = transform.position;
    }

    void Update()
    {
        //プレイヤーとの距離が.1f未満になったらそれ以上実行しない
        if (Vector2.Distance(transform.position, playerTransform.position) < distance)
            return;

        //プレイヤーに向けて進む
        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(playerTransform.position.x, playerTransform.position.y),
            speed * Time.deltaTime);

        #region　反転処理
        // プレイヤーキャラクターの位置
        Vector3 playerPosition = playerTransform.transform.position;
        // 敵キャラクターの位置
        Vector3 enemyPosition = transform.position;

        // プレイヤーの方向を計算
        Vector3 direction = playerPosition - enemyPosition;
        // 向きの判断
        if (direction.x > 0)
        {
            // プレイヤーが右にいる場合、敵キャラクターを右向きにする
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            // プレイヤーが左にいる場合、敵キャラクターを左向きにする
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        #endregion
    }

    public void objTr(GameObject obj)
    {
        playerTransform = obj.transform;
    }
}
