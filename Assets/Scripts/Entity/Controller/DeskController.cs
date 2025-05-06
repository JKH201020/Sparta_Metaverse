using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskController : MonoBehaviour
{
    public GameObject textCanvas; // ���� UI ������Ʈ (Inspector���� �Ҵ�)
    public GameObject scoreBoard;

    bool interact = false; // ��ȣ�ۿ� ��������
    bool scoreboardOpen = false; // ���ھ�带 ���� �ִ°�

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && interact && !scoreboardOpen)
        {
            Interactive();
            scoreboardOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && interact && scoreboardOpen)
        {
            scoreBoard.SetActive(false);
            scoreboardOpen = false;
        }
        else if (!interact)
        {
            scoreBoard.SetActive(false);
        }
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
        interact = true;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && player.CompareTag("Player"))
        {
            scoreBoard.SetActive(true);
        }
    }
}
