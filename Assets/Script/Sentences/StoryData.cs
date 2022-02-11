using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryData : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, string[]> nameData;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        nameData = new Dictionary<int, string[]>();
        GenetateData();
        GenetateNameData();
    }

    void GenetateData()
    {
        talkData.Add(1000, new string[] { "천사의 불타오르는 심장을 부서라", "불타오르는 심장?", "이게 무슨 말이지?", "일단 주위를 더 살펴보자!" });
        talkData.Add(2000, new string[] { "천사의 차가운심장을 부서라", "차가운 심장이라...", "모르겠네", "주위를 살펴보자!" });
        talkData.Add(3000, new string[] { "불타오르는 심장은 오르지 물로만 식힐 수 있다", "물이라..", "원소를 사용하면 될것 같어!", "(Q를 사용 하여 원소를 변경 할 수 있습니다)" });
        talkData.Add(4000, new string[] { "차가운 심장은 오직 불로만 녹일 수 있다", "불이라..", "원소를 사용하면 될것 같은데?", "(Q를 사용 하여 원소를 변경 할 수 있습니다)" });
        talkData.Add(5000, new string[] { "여기까지가 끝입니다!", "플레이해주셔서 감사합니다!", "와아아아아 클리어!!" });
    }

    void GenetateNameData()
    {
        nameData.Add(1000, new string[] { "비석", "나", "나", "나" });
        nameData.Add(2000, new string[] { "비석", "나", "나", "나" });
        nameData.Add(3000, new string[] { "비석", "나", "나", "나" });
        nameData.Add(4000, new string[] { "비석", "나", "나", "나" });
        nameData.Add(5000, new string[] { "NULL", "NULL", "NULL"});
    }
    public void Read(int key)
    {
        string[] sentences = talkData[key];
        string[] name = nameData[key];
        if (GameManager.Instance.dialogueManager.dialogueGroup.alpha == 0)
        {
            GameManager.Instance.dialogueManager.Ondialogue(sentences, name);
        }
    }
}
