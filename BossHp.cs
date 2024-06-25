using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHp : MonoBehaviour
{
    Transform playerTr;//Playerのトランスフォーム
    
   
    [SerializeField] int hp = 5;
    [SerializeField] GameObject BossEnemy;
    [SerializeField] float knockbackForce = 0.1f;
    [SerializeField] float knockbackDuration = 0.2f;

    private Rigidbody2D parentRb;
    private bool isKnockback;
    private BossEnemy bossEnemy;

    private void Start()
    {
        parentRb = transform.parent.GetComponent<Rigidbody2D>();
        bossEnemy = transform.parent.GetComponent<BossEnemy>();
        
    }
    private void Update()
    {
        if (isKnockback)
            return;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))//攻撃されたらHp減る
        {
            // ノックバック方向を求める
            Vector2 knockbackDirection = new Vector2(transform.position.x - other.transform.position.x, 0).normalized;
            // 親オブジェクトにノックバックを加える
            KnockbackParentObject(knockbackDirection * knockbackForce);

            hp -= 1;
            Debug.Log("BossEnemy HP: " + hp);

            if (hp <= 0)
            {
                BossEnemy.SetActive(false);
            }
        }
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
