using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed = 1;

    void Update()
    {
        //当たり判定をしたい敵たちを取ってくるんだけど、重そう
        var targets = GameObject.FindGameObjectsWithTag("Enemy");

        for(int i = 0; i < targets.Length; i++)
        {
            //敵を矩形、自分(弾)を矩形として判定
            bool isHitX = Mathf.Abs(transform.position.x - targets[i].transform.position.x)
            <= transform.localScale.x / 2 + targets[i].transform.localScale.x / 2; //x座標が重なっているか
            bool isHitY = Mathf.Abs(transform.position.y - targets[i].transform.position.y)
                <= transform.localScale.y / 2 + targets[i].transform.localScale.y / 2; //y座標が重なっているか

            if(isHitX && isHitY)
            {
                targets[i].GetComponent<IDamageable>().Damage(1);
                Destroy(gameObject);
            }

        }

        this.transform.position += _speed * Time.deltaTime * transform.right;
    }

}
