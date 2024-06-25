using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    [SerializeField] int hp = 5;
    [SerializeField] GameObject Enemy;
    public float knockbackForce = 0.1f;
    public float knockbackDuration = 0.2f;

    private Rigidbody2D parentRb;
    private bool isKnockback;

    private void Start()
    {
        parentRb = transform.parent.GetComponent<Rigidbody2D>();

        if (isKnockback)
            return;
        if (Enemy == null)
        {
            Debug.LogError("敵オブジェクトが指定されていません");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)//攻撃されたらHp減る
    {
        if (other.CompareTag("Attack"))
        {
            // ノックバック方向を求める
            Vector2 knockbackDirection = new Vector2(transform.position.x - other.transform.position.x, 0).normalized;
            // 親オブジェクトにノックバックを加える
            KnockbackParentObject(knockbackDirection * knockbackForce);


            //ダメージ処理
            hp -= 1;
            Debug.Log("NomalEnemy HP: " + hp);

            if (hp <= 0)
            {
                Enemy.SetActive(false);
            }

            void KnockbackParentObject(Vector2 direction)
            {
                // ノックバックを開始する
                StartCoroutine(DoKnockback(direction));
            }

            IEnumerator DoKnockback(Vector2 direction)
            {
                isKnockback = true;

                // ノックバック方向に力を加える（親オブジェクトのRigidbody2Dを使用）
                parentRb.velocity = direction * knockbackForce;

                // ノックバックが終わるまで待つ
                yield return new WaitForSeconds(knockbackDuration);

                isKnockback = false;

                // ノックバック終了後に速度をゼロにする（必要に応じて）
                parentRb.velocity = Vector2.zero;
            }
        }
    }
}
