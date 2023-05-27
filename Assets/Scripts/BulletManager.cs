using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>「敵の弾」と「プレイヤーの弾」の処理をまとめる</summary>
public class BulletManager : MonoBehaviour
{
    [SerializeField] private Transform _player = default;
    [SerializeField] private List<Transform> _enemies = new List<Transform>(5);
    [SerializeField] private List<GameObject> _playerBullets = new List<GameObject>();
    [SerializeField] private float _playerBulletSpeed = 4f;
    [SerializeField] private List<GameObject> _enemyBullets = new List<GameObject>();
    [SerializeField] private float _enemyBulletSpeed = 4f;
      
    void Update()
    {
        
        //プレイヤーの弾
        foreach(var pb in _playerBullets)
        {

            //もういなかったらリストから削除
            if (!pb)
            {
                _playerBullets.Remove(pb);
                return;
            }

            //右に移動
            pb.transform.position += _playerBulletSpeed * Time.deltaTime * pb.transform.right;

            //敵との当たり判定
            foreach(var e in _enemies)
            {
                if (!e)
                {
                    _enemies.Remove(e);
                    return;
                }

                //敵を矩形、自分(弾)を矩形として判定
                bool isHitX = Mathf.Abs(pb.transform.position.x - e.position.x) <= pb.transform.localScale.x / 2 + e.localScale.x / 2; //x座標が重なっているか
                bool isHitY = Mathf.Abs(pb.transform.position.y - e.position.y) <= pb.transform.localScale.y / 2 + e.localScale.y / 2; //y座標が重なっているか

                if (isHitX && isHitY) //x座標とy座標が重なってたらダメージを与えて自信を破壊
                {
                    e.GetComponent<IDamageable>().Damage(1);
                    Destroy(pb);
                }

            }

        }

        //敵の弾
        foreach (var eb in _enemyBullets)
        {

            //もういなかったらリストから削除
            if (!eb)
            {
                _enemyBullets.Remove(eb);
                return;
            }

            //左に移動
            eb.transform.position += _enemyBulletSpeed * Time.deltaTime * -eb.transform.right;

            //プレイヤーを矩形、自分(弾)を点として判定
            bool isHitX = Mathf.Abs(eb.transform.position.x - _player.position.x) <= _player.localScale.x / 2; //x座標が重なっているか
            bool isHitY = Mathf.Abs(eb.transform.position.y - _player.position.y) <= _player.localScale.y / 2; //y座標が重なっているか

            if (isHitX && isHitY) //x座標とy座標どちらも重なってたらダメージを与えて自信を破壊
            {
                _player.gameObject.GetComponent<IDamageable>().Damage(1);
                Destroy(eb);
            }

        }

    }

    public void AddPlayerBullet(GameObject bullet)
    {
        _playerBullets.Add(bullet);
    }

    public void AddEnemyBullet(GameObject bullet)
    {
        _enemyBullets.Add(bullet);
    }

    public void AddEnemy(Transform enemy)
    {
        _enemies.Add(enemy);
    }

    public void SetPlayer(Transform player)
    {
        _player = player;
    }

}
