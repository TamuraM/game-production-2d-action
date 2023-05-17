using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private Transform _player = default;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
        pos.z = 0;

        transform.position = pos;

        float angle = Mathf.Atan2(pos.y - _player.position.y, pos.x - _player.position.x);
        _player.rotation = Quaternion.AngleAxis(angle * 180 / Mathf.PI, new Vector3(0, 0, 1));
    }

}
