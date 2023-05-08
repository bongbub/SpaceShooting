using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmo : MonoBehaviour
{
    public Color _color = Color.red;
    public float _radius = 0.1f;

    void OnDrawGizmos()
    {
        Gizmos.color = _color;      //기즈모 색상 설정
        Gizmos.DrawSphere(transform.position, _radius);     //구체 모양의 기즈모 생성
        //인자는 (생성위치, 반지름)
    }
}
