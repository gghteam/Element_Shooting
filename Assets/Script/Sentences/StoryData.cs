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
        talkData.Add(1000, new string[] { "õ���� ��Ÿ������ ������ �μ���", "��Ÿ������ ����?", "�̰� ���� ������?", "�ϴ� ������ �� ���캸��!" });
        talkData.Add(2000, new string[] { "õ���� ����������� �μ���", "������ �����̶�...", "�𸣰ڳ�", "������ ���캸��!" });
        talkData.Add(3000, new string[] { "��Ÿ������ ������ ������ ���θ� ���� �� �ִ�", "���̶�..", "���Ҹ� ����ϸ� �ɰ� ����!", "(Q�� ��� �Ͽ� ���Ҹ� ���� �� �� �ֽ��ϴ�)" });
        talkData.Add(4000, new string[] { "������ ������ ���� �ҷθ� ���� �� �ִ�", "���̶�..", "���Ҹ� ����ϸ� �ɰ� ������?", "(Q�� ��� �Ͽ� ���Ҹ� ���� �� �� �ֽ��ϴ�)" });
        talkData.Add(5000, new string[] { "��������� ���Դϴ�!", "�÷������ּż� �����մϴ�!", "�;ƾƾƾ� Ŭ����!!" });
    }

    void GenetateNameData()
    {
        nameData.Add(1000, new string[] { "��", "��", "��", "��" });
        nameData.Add(2000, new string[] { "��", "��", "��", "��" });
        nameData.Add(3000, new string[] { "��", "��", "��", "��" });
        nameData.Add(4000, new string[] { "��", "��", "��", "��" });
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
