using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�ݵ�� �ʿ��� ������Ʈ�� ����� �ش� ������Ʈ�� �����Ǵ� ���� �����ϴ� ��Ʈ����Ʈ
[RequireComponent(typeof(AudioSource))]

public class FireCtrl : MonoBehaviour
{
    //�Ѿ� ������
    public GameObject bullet;
    //���� �߻� ��ǥ
    public Transform firePos;
    //�Ѿ˿� ���Ǵ� ����
    public AudioClip fireSfx;
    //AudioSource ������Ʈ�� ������ ����
    private new AudioSource audio;
    //Muzzle Flash �� MeshRenderer ������Ʈ
    private MeshRenderer muzzleFlash;

    #region ���� ���� �������� �õ��ߴ� ����
    //private Transform target;
    #endregion

    void Start()
    {
        #region ���� ���� �������� �ߴ� �ڵ�
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        #endregion

        //������Ʈ�� Audio Source ����
        audio = GetComponent<AudioSource>();

        //FirePos ������ �ִ� MuzzleFlash�� Material ������Ʈ ����
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();
        //ó�� ������ �� ��Ȱ��ȭ
        muzzleFlash.enabled = false;
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
        //Bullet �������� �������� ����
        //firePos ��ġ���� �����̼ǰ����� bullet�� �����ϰڴ�
        Instantiate(bullet, firePos.position, firePos.rotation);

        //�ѼҸ� ����
        audio.PlayOneShot(fireSfx, 1.0f);

        //�ѱ� ȭ�� ȿ�� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(ShowMuzzleFlash());


        #region ���� �Ѿ����� �������� ���� �õ��޴� ���...
        //AudioSource.PlayClipAtPoint(sound, target.transform.position);
        #endregion
    }

    IEnumerator ShowMuzzleFlash()
    {
        //������ ��ǥ���� ���� �Լ��� ���
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        //�ؽ����� ������ �� ����
        muzzleFlash.material.mainTextureOffset = offset;

        //MuzzleFlash�� ȸ�� �ݰ�
        float angle = Random.Range(0, 360);
        muzzleFlash.transform.localRotation = Quaternion.Euler(0, 0, angle);

        //MuzzleFlash�� ũ�� ����
        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;

        //Muzzle Flash Ȱ��ȭ
        muzzleFlash.enabled = true;
        //0.2 �� ���� ���(����) �ϴ� ���� �޼��� ������ ����� �纸
        yield return new WaitForSeconds(0.2f);
        //Muzzle Flash ��Ȱ��ȭ
        muzzleFlash.enabled = false;
    }
}
