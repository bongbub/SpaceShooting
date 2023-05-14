using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;

    public AudioClip sound;
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire(); // ���콺�� ������ �� fire()�Լ� ȣ��
        }
    }

    void Fire()
    {
        Instantiate(bullet, firePos.position, firePos.rotation);
        //firePos ��ġ���� �����̼ǰ����� bullet�� �����ϰڴ�
        AudioSource.PlayClipAtPoint(sound, target.transform.position);
    }
}
