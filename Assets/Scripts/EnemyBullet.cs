using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    private Transform _target = default;

    private void Start()
    {
        _target = GameObject.FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        //プレイヤーを矩形、自分(弾)を点として判定
        bool isHitX = Mathf.Abs(transform.position.x - _target.position.x)
            <= _target.localScale.x / 2; //x座標が重なっているか
        bool isHitY = Mathf.Abs(transform.position.y - _target.position.y)
            <= _target.localScale.y / 2; //y座標が重なっているか

        if(isHitX && isHitY) //x座標とy座標どちらも重なってたら自分を破壊
        {
            _target.gameObject.GetComponent<IDamageable>().Damage(1);
            Destroy(gameObject);
        }

        transform.position += _speed * Time.deltaTime * -transform.right;
    }
}
