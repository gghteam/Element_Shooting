using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapController : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorialMap;

    [Header("Random Map")]
    [SerializeField]
    public Transform startingPos; 

    [SerializeField]
    private GameObject start;
    [SerializeField]
    private GameObject end;
    [SerializeField]
    private List<GameObject> itemMap = new List<GameObject>();
    [SerializeField]
    private List<GameObject> monsterMap = new List<GameObject>();
    [SerializeField]
    private List<GameObject> trapMap = new List<GameObject>();
    [SerializeField]
    private List<GameObject> etcMap = new List<GameObject>();
    [SerializeField]
    private List<GameObject> boundaryMap = new List<GameObject>();
    //[SerializeField]
    //private GameObject[] rooms; // 0 �ʼ����� 1 �������� 2 �������� 3 ��Ÿ����
    [SerializeField]
    private GameObject empty;

    [SerializeField]
    private int itemCount;
    [SerializeField]
    private int monsterCount;
    [SerializeField]
    private int trapCount;
    [SerializeField]
    private int etcCount; // (���?���� - �����?����)

    [SerializeField]
    private int width = 5;
    [SerializeField]
    private int height = 5;

    [SerializeField]
    private float moveAmount;

    public float startTimetrwRoom;
    private float timeBtwRoom;

    private int dx = 0;
    private int dy = 0;

    private Vector2 startPos = Vector2.zero, endPos = Vector2.zero, downPos = Vector2.zero;

    private Vector2 bounderyPos;

    private int mapCount;
    public bool stopGeneration = true;
    public bool stopDownGeneration = true;
    private bool isCompleteF = false;

    private GameObject[,] mapObjects;

    [Header("�� �߶��� ���� ����")]
    [SerializeField]
    private float setDownTime = 5f;
   [SerializeField]
    private float shakeDuration = 1f;
    private float downTime;
    private int downIndex = -1;
    enum ERoom
    {
        Item,
        Monster,
        Trap,
        Etc,
        Count
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
        mapObjects = new GameObject[height,width];
        // PlayerPrefs.SetInt("TURORIAL",1);
        if (PlayerPrefs.GetInt("TURORIAL", 1) == 1)
        {
            GameObject map = Instantiate(tutorialMap, new Vector3(0, 0, 0), Quaternion.identity);
            map.transform.parent = gameObject.transform;
        }
        else
        {
            //Etc_Map Setting
            mapCount = width * height;
            etcCount = mapCount - (itemCount + monsterCount + trapCount);

            SpawnBoundary();

            SpawnStart();
        }
    }

    private void Update()
    {
        if (timeBtwRoom <= 0 && !stopGeneration)
        {
            Move();
            timeBtwRoom = startTimetrwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }

        if (stopDownGeneration)
        {
            downTime += Time.deltaTime;
            if (downTime >= setDownTime)
            {
                DownMap();
                downTime = 0;
            }
        }
    }

    private void SpawnBoundary()
    {
        bounderyPos = new Vector2(startingPos.position.x, startingPos.position.y + moveAmount);

        // HEIGHT
        for (int i = 0; i <= height + 1; i++)
        {
            Vector2 newPos = new Vector2(bounderyPos.x - moveAmount, bounderyPos.y - (moveAmount * i));
            Vector2 newSPos = new Vector2(bounderyPos.x + moveAmount * width, bounderyPos.y - (moveAmount * i));
            GameObject fObj = null, sObj = null;
            if(i == 0)
            {
                fObj = boundaryMap[0];
                sObj = boundaryMap[2];
            }
            else if(i == height + 1)
            {
                fObj = boundaryMap[5];
                sObj = boundaryMap[7];
            }
            else
            {
                fObj = boundaryMap[3];
                sObj = boundaryMap[4];
            }
            Instantiate(fObj, newPos, Quaternion.identity);
            Instantiate(sObj, newSPos, Quaternion.identity);
        }

        
        //WIDTH
        for (int i = 0; i < width; i++)
        {
            Vector2 newPos = new Vector2(bounderyPos.x + moveAmount * i, bounderyPos.y);
            Vector2 newSPos = new Vector2(bounderyPos.x + moveAmount * i, bounderyPos.y - moveAmount * (height + 1));


            Instantiate(boundaryMap[1], newPos, Quaternion.identity);
            Instantiate(boundaryMap[6], newSPos, Quaternion.identity);
        }
        
    }

    private void SpawnStart()
    {
        //������Ʈ ��ġ�� ������ ��ġ�� ����
        transform.position = startingPos.position;

        int random = Random.Range(0, 4);
        int pos;
        switch (random)
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

        downPos = startPos;
        Vector2 newStartPos = new Vector2(transform.position.x + (moveAmount * startPos.x), transform.position.y - (moveAmount * startPos.y));
        Vector2 newEndPos = new Vector2(transform.position.x + (moveAmount * endPos.x), transform.position.y - (moveAmount * endPos.y));

        mapObjects[(int)startPos.y, (int)startPos.x] = Instantiate(start, newStartPos, Quaternion.identity);
        mapObjects[(int)endPos.y, (int)endPos.x] = Instantiate(end, newEndPos, Quaternion.identity);
        GameManager.Instance.SetPos = newStartPos;
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
            while (!isComplete)
            {
                rand = Random.Range(0, (int)ERoom.Count);
                isComplete = Check(rand);
            }

            List<GameObject> useList = ProvideList(rand);

            rand = Random.Range(0, useList.Count);

            mapObjects[dy, dx] = Instantiate(useList[rand], newPos, Quaternion.identity);
        }

        if (++dx >= width)
        {
            dx = 0;
            dy++;
        }

        //�� ���� ��
        if (dy >= height)
        {
            stopGeneration = true;
            //PrintObject();
        }
    }

    private bool Check(int index)
    {
        switch (index)
        {
            case (int)ERoom.Item:
                if (itemCount <= 0) return false;
                else itemCount--;
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

    private List<GameObject> ProvideList(int index)
    {
        switch (index)
        {
            case (int)ERoom.Item:
                return itemMap;
            case (int)ERoom.Monster:
                return monsterMap;
            case (int)ERoom.Trap:
                return trapMap;
            case (int)ERoom.Etc:
                return etcMap;
        }
        return new List<GameObject>();
    }

    private void DownMap()
    {
        if(!isCompleteF)
        {
            StartCoroutine(GameManager.Instance.camera.Shake(0.1f, shakeDuration));
            StartCoroutine(Shake(0.3f, shakeDuration, mapObjects[(int)downPos.y, (int)downPos.x]));
            mapObjects[(int)downPos.y, (int)downPos.x].GetComponent<MapObject>().IsDown = true;
            //mapObjects[(int)downPos.y, (int)downPos.x].transform.Translate(Vector2.down * downSpeed * Time.deltaTime);
            //mapObjects[(int)downPos.y, (int)downPos.x].SetActive(false);
            mapObjects[(int)downPos.y, (int)downPos.x] = empty;
            isCompleteF = true;

            return;
        }
        //UDRL
        Vector2[] dir = new Vector2[4];
        bool[] dirCheck = new bool[4];
        dir[0] = new Vector2(0, -1); //UP
        dir[1] = new Vector2(1, 0); //RIGHT
        dir[2] = new Vector2(0, 1); //DOWN
        dir[3] = new Vector2(-1, 0); //LEFT

        //int rand = Random.Range(0, 4);
        downIndex = (downIndex + 1) % 4;
        Debug.Log($"DOWNINDEX:{downIndex}");
        Vector2 addPos = downPos + dir[downIndex];
        dirCheck[downIndex] = true;

        while(addPos.x < 0 || addPos.x >= width || addPos.y < 0 || addPos.y >= height 
            || mapObjects[(int)addPos.y, (int)addPos.x] == empty || mapObjects[(int)addPos.y, (int)addPos.x].CompareTag("Necessary"))
        {
            if (CheckBDir(dirCheck))
            {
                stopDownGeneration = false;
                return;
            }

            //rand = Random.Range(0, 4);
            downIndex = (downIndex + 1) % 4;
            Debug.Log($"CDOWNINDEX:{downIndex}");
            dirCheck[downIndex] = true;
            addPos = downPos + dir[downIndex];
        }

        Debug.Log($"Down:{addPos}");
        downPos = addPos;
        StartCoroutine(GameManager.Instance.camera.Shake(0.1f, shakeDuration));
        StartCoroutine(Shake(0.3f, shakeDuration, mapObjects[(int)downPos.y, (int)downPos.x]));
        mapObjects[(int)downPos.y, (int)downPos.x].GetComponent<MapObject>().IsDown = true;
        //mapObjects[(int)downPos.y, (int)downPos.x].SetActive(false);
        mapObjects[(int)downPos.y, (int)downPos.x] = empty;

    }

    private bool CheckBDir(bool[] checks)
    {
        foreach(bool check in checks)
        {
            if (!check) return false;
        }

        return true;
    }

    private void PrintObject()
    {
        for(int i  = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                Debug.Log($"({i},{j}):{mapObjects[i, j].name}");
            }
        }
    }

    public IEnumerator Shake(float _amount, float _duration, GameObject shakeObj)
    {
        Vector3 originVec = shakeObj.transform.localPosition;
        float timer = 0;
        while (timer <= _duration)
        {
            shakeObj.transform.localPosition = (Vector3)UnityEngine.Random.insideUnitCircle * _amount + originVec;

            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originVec;

    }
}