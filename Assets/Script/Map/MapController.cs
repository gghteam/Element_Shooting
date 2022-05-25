using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorialMap;

    [Header("Random Map")]
    [SerializeField]
    public Transform startingPos; //������ ��ġ

    // ������ ������Ʈ
    [SerializeField]
    private GameObject start;
    [SerializeField]
    private GameObject end;
    [SerializeField]
    private GameObject[] rooms; // 0 �ʼ����� 1 �������� 2 �������� 3 ��Ÿ����


    //������ ����
    [SerializeField]
    private int necessaryCount;
    [SerializeField]
    private int monsterCount;
    [SerializeField]
    private int trapCount;
    [SerializeField]
    private int etcCount; // (��� ���� - ����� ����)

    //�� ũ��
    [SerializeField]
    private int width = 5;
    [SerializeField]
    private int height = 5;

    //�̵��� ��
    [SerializeField]
    private float moveAmount;

    //���� �ð�(Test��)
    public float startTimetrwRoom;
    private float timeBtwRoom;

    //�̵��� ��ġ 
    private int dx = 0;
    private int dy = 0;

    private Vector2 startPos = Vector2.zero, endPos = Vector2.zero;

    private int mapCount;
    public bool stopGeneration = true;
    enum ERoom
    {
        Neccssary,
        Monster,
        Trap,
        Etc
    }

    enum ChangePos
    {
        StartX,
        StartY,
        EndX,
        EndY
    }

    private void Awake()
    {
      // PlayerPrefs.SetInt("TURORIAL",1);
       if(PlayerPrefs.GetInt("TURORIAL",1) == 1)
        {
            GameObject map = Instantiate(tutorialMap, new Vector3(0, 0, 0), Quaternion.identity);
            map.transform.parent = gameObject.transform;
        }
       else
        {
            //Etc_Map Setting
            mapCount = width * height;
            etcCount = mapCount - (necessaryCount + monsterCount + trapCount);

            SpawnStart();
        }
    }

    private void Update()
    {
        //�ð��� ������
        if(timeBtwRoom <= 0 && !stopGeneration)
        {
            //������ ����
            Move();
            timeBtwRoom = startTimetrwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    private void SpawnStart()
    {
        //������Ʈ ��ġ�� ������ ��ġ�� ����
        transform.position = startingPos.position;

        int random = Random.Range(0, 4);
        int pos;
        switch(random)
        {
            case (int)ChangePos.StartX:
                pos = Random.Range(0, width);
                startPos = new Vector2(pos, 0);
                endPos = new Vector2((width - 1) - pos, height - 1);
                break;
            case (int)ChangePos.StartY:
                pos = Random.Range(0, height);
                startPos = new Vector2(0, pos);
                endPos = new Vector2(width - 1, (height - 1) - pos);
                break;
            case (int)ChangePos.EndX:
                pos = Random.Range(0, width);
                startPos = new Vector2(pos, height - 1);
                endPos = new Vector2((width - 1) - pos, 0);
                break;
            case (int)ChangePos.EndY:
                pos = Random.Range(0, height);
                startPos = new Vector2(width - 1, pos);
                endPos = new Vector2(0, (height - 1) - pos);
                break;
        }

        Vector2 newStartPos = new Vector2(transform.position.x + (moveAmount * startPos.x), transform.position.y - (moveAmount * startPos.y));
        Vector2 newEndPos = new Vector2(transform.position.x + (moveAmount * endPos.x), transform.position.y - (moveAmount * endPos.y));

        Instantiate(start, newStartPos, Quaternion.identity);
        Instantiate(end, newEndPos, Quaternion.identity);
        // Start,End�� �ʼ�ī��Ʈ�� ����
        GameManager.Instance.SetPos = newStartPos;
        necessaryCount -= 2;
        stopGeneration = false;
    }
    private void Move()
    {
        Vector2 newPos = new Vector2(transform.position.x + (moveAmount * dx),
            transform.position.y - (moveAmount * dy));
        bool isComplete = false;
        int rand = 0;

        if ((dx != startPos.x || dy != startPos.y) && (dx != endPos.x || dy != endPos.y))
        {
            while(!isComplete)
            {
                rand = Random.Range(0, rooms.Length);
                isComplete = Check(rand);
            }
            Instantiate(rooms[rand], newPos, Quaternion.identity);
        }

        if(++dx >= width)
        {
            dx = 0;
            dy++;
        }

        //�� ���� ��
        if (dy >= height) stopGeneration = true;
    }

    private bool Check(int index)
    {
        switch(index)
        {
            case (int)ERoom.Neccssary:
                if (necessaryCount <= 0) return false;
                else necessaryCount--;
                break;
            case (int)ERoom.Monster:
                if (monsterCount <= 0) return false;
                else monsterCount--;
                break;
            case (int)ERoom.Trap:
                if (trapCount <= 0) return false;
                else trapCount--;
                break;
            case (int)ERoom.Etc:
                if (etcCount <= 0) return false;
                else etcCount--;
                break;
        }
        return true;
    }
}
