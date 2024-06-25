using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalEnemy : MonoBehaviour
{
    [SerializeField]Transform playerTransform;//Playerのトランスフォーム
    [SerializeField] float distance = 1f; //プレイヤーに近づける距離
    [SerializeField] float attackCooltime = 1.0f;//攻撃のクールダウン
    [SerializeField] float delay = 1f;
    [SerializeField] float speed = 1.5f; //移動速度
    [SerializeField] AudioClip zonbieSE; //ゾンビの攻撃音

    private const string MainCamera = "MainCamera";//メインカメラのタグ
    private bool isCameraIn = false;//カメラに入っているかのフラグ
    private float Stop = 0f;//攻撃中止まる
    private float Go = 1.5f;//攻撃終わったら動く
    private bool isAttack = false;//攻撃中かどうか管理するフラグ
    private float lastAttackTime;//最後に攻撃した時間
    private Vector3 lastPosition;
    private Animator anim;
    private AudioSource audioSource;　//オーディオソース  
  

    
    
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>(); //オーディオソース取得
        lastAttackTime = -attackCooltime; //最初の攻撃をすぐ出せる
        anim = GetComponent<Animator>();
        lastPosition = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").
            transform; //プレイヤーのトランスフォーム取得
    }

    private void Move()//移動処理
    {
        if (isCameraIn == true)//カメラに入っている間処理する
        {
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

        #region 移動処理
        //プレイヤーとの距離が.1f未満になったらそれ以上実行しない
        if (Vector2.Distance(transform.position, playerTransform.position) < distance)
            return;

        //プレイヤーに向けて進む
        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(playerTransform.position.x, playerTransform.position.y),
            speed * Time.deltaTime);

        if (isAttack) return;//攻撃中は移動しない
        #endregion
    }

    public void Attack(GameObject Player)//攻撃処理
    {
        if (Time.time >= lastAttackTime + attackCooltime)
        {            
            #region 攻撃の処理
            audioSource.PlayOneShot(zonbieSE);　//効果音を一回だけ鳴らす
            anim.SetBool("EnemyAttack", true);//攻撃アニメーションを流す
            lastAttackTime = Time.time;
            Invoke("OnAttackEnd", delay);
            speed = Stop;//攻撃するとき止まる
            #endregion
        }
    }
    void OnAttackEnd()//攻撃の終わる処理
    {
        anim.SetBool("EnemyAttack", false);//攻撃アニメーション終わり
        speed = Go;//攻撃終わったら動く
    }

    private void OnWillRenderObject()
    {
        if (Camera.current.tag == MainCamera)//メインカメラに入っている間下記の処理する
        {
            isCameraIn = true;//カメラに入っているかのフラグ
            Move();//移動処理
            // プレイヤーキャラクターの位置
            Vector3 playerPosition = playerTransform.position;
            // 敵キャラクターの位置
            Vector3 enemyPosition = transform.position;
            // プレイヤーの方向を計算
            Vector3 direction = playerPosition - enemyPosition;
        }
    }

    public void objTr(GameObject obj)
    {
        playerTransform = obj.transform;
    }

}
