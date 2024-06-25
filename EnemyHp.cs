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
            Debug.LogError("�G�I�u�W�F�N�g���w�肳��Ă��܂���");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)//�U�����ꂽ��Hp����
    {
        if (other.CompareTag("Attack"))
        {
            // �m�b�N�o�b�N���������߂�
            Vector2 knockbackDirection = new Vector2(transform.position.x - other.transform.position.x, 0).normalized;
            // �e�I�u�W�F�N�g�Ƀm�b�N�o�b�N��������
            KnockbackParentObject(knockbackDirection * knockbackForce);


            //�_���[�W����
            hp -= 1;
            Debug.Log("NomalEnemy HP: " + hp);

            if (hp <= 0)
            {
                Enemy.SetActive(false);
            }

            void KnockbackParentObject(Vector2 direction)
            {
                // �m�b�N�o�b�N���J�n����
                StartCoroutine(DoKnockback(direction));
            }

            IEnumerator DoKnockback(Vector2 direction)
            {
                isKnockback = true;

                // �m�b�N�o�b�N�����ɗ͂�������i�e�I�u�W�F�N�g��Rigidbody2D���g�p�j
                parentRb.velocity = direction * knockbackForce;

                // �m�b�N�o�b�N���I���܂ő҂�
                yield return new WaitForSeconds(knockbackDuration);

                isKnockback = false;

                // �m�b�N�o�b�N�I����ɑ��x���[���ɂ���i�K�v�ɉ����āj
                parentRb.velocity = Vector2.zero;
            }
        }
    }
}
