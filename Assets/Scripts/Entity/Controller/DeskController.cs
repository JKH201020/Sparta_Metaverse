using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskController : MonoBehaviour
{
    public GameObject textCanvas; // ���� UI ������Ʈ (Inspector���� �Ҵ�)
    bool interact = false; // ��ȣ�ۿ� ��������

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interact = true;
            textCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        interact = false;
        textCanvas.SetActive(false);
    }

    void Interactive()
    {
        //interact = true;

        //GameObject player = GameObject.FindGameObjectWithTag("Player");

        //if (Input.GetKeyDown(KeyCode.F) && interact)
        //{
        //    textCanvas.SetActive(true);
        //}
    }
}
