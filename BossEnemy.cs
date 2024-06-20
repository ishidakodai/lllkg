using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    Transform playerTr;

    [SerializeField] float distance = 1f; //プレイヤーに近づける距離
    [SerializeField] float speed = 3f; //移動速度
    [SerializeField] float attackCooltime = 1.0f;//攻撃のクールダウン
    [SerializeField] float delay = 1f;

    private const string MainCamera = "MainCamera";//メインカメラのタグ
    private bool CameraIn = false;//カメラに入っているかのフラグ
    private float Stop = 0f;//攻撃中止まる
    private float Go = 3f;//攻撃終わったら動く
    private bool isAttack = false;//攻撃中かどうか管理するフラグ
    private float lastAttackTime;//最後に攻撃した時間
    private Vector3 lastPos;
    private Animator anim;

    void Start()
    {
        lastAttackTime = -attackCooltime; //最初の攻撃をすぐ出せる
        anim = GetComponent<Animator>();
        lastPos = transform.position;
        playerTr = GameObject.FindGameObjectWithTag("Player").
            transform; //プレイヤーのトランスフォーム取得
    }


    private void move()//移動処理
    {

        if (CameraIn == true)
        {
            #region　反転処理
            Vector3 currentPosition = transform.position;
            Vector3 _direction = currentPosition - lastPos;

            if (_direction.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (_direction.x < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            lastPos = currentPosition;

            #endregion
        }


        #region 移動処理
        //プレイヤーとの距離が.1f未満になったらそれ以上実行しない
        if (Vector2.Distance(transform.position, playerTr.position) < distance)
            return;

        //プレイヤーに向けて進む
        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(playerTr.position.x, playerTr.position.y),
            speed * Time.deltaTime);

        if (isAttack) return;//攻撃中は移動しない
        #endregion
    }

    public void Attack(GameObject Player)//攻撃処理
    {
        if (Time.time >= lastAttackTime + attackCooltime)
        {
            #region 攻撃の処理
            anim.SetBool("Attack", true);//攻撃アニメーションを流す
            lastAttackTime = Time.time;
            Invoke("DelayMethod", delay);
            speed = Stop;//攻撃するとき止まる
            #endregion
        }
    }
    void DelayMethod()//攻撃の終わる処理
    {
        anim.SetBool("Attack", false);//攻撃アニメーション終わり
        speed = Go;//攻撃終わったら動く
    }

    private void OnWillRenderObject()
    {
        if (Camera.current.tag == MainCamera)//メインカメラに入っている間下記の処理する
        {
            CameraIn = true;//カメラに入っているかのフラグ
            move();//移動処理
        }
    }
}
