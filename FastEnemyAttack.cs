using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject parent;//�e�I�u�W�F�N�g�w��
    private FastEnemy parentEnemy;

    private void Start()
    {
        if (parent != null)
        {
            parentEnemy = parent.GetComponent<FastEnemy>();
            if (parentEnemy == null)
            {
                Debug.LogError("�e�I�u�W�F�N�g��FastEnemy�R���|�[�l���g��������܂���");
            }

        }
        else
        {
            Debug.LogError("�e�I�u�W�F�N�g���w�肳��Ă��܂���");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && parentEnemy != null) // �v���C���[�^�O�擾�Ɛe�I�u�W�F�N�g�̑��݃`�F�b�N
        {
            //�e�I�u�W�F�N�g�ɍU���w��
            parent.GetComponent<FastEnemy>().Attack(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && parentEnemy != null)
        {
            //�e�I�u�W�F�N�g�ɍU���w��
            parent.GetComponent<FastEnemy>().Attack(other.gameObject);
        }
    }
}
