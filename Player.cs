using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �ړ����x�̎w��
    [SerializeField]
    private int moveSpeed;

    // �I�u�W�F�N�g��Rigidbody2D���A�^�b�`����
    public Rigidbody2D rb;
    public int _hp = 5;

    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 240;
    }

    // Update is called once per frame
    void Update()
    {
        // �΂߈ړ����ł��Ȃ��悤�ɁAHorizontal�̓��͂�D�悷��
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), 0).normalized * moveSpeed;
        }
        else
        {
            rb.velocity = new Vector2(0, Input.GetAxisRaw("Vertical")).normalized * moveSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttack"))
        {
            _hp = _hp - 1;
            Debug.Log(_hp);
        }
    }
}