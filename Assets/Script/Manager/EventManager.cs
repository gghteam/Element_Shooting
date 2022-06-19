using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public enum EventName
    {
        PLAYER_DAMAGED = 0,
        PLAYER_DEAD = 1,
        PLAYER_RUN = 2,
        //0~10 Player Event

        GET_ITEM = 50,
        //50~100 Item

        PANEL_SHOW = 100,
        PANEL_HIDE = 101,
        PANEL_ElementChanged = 102
        //100~? UI Event
    }

    private static Dictionary<EventName,Action> eventDictionary;

    private void Awake() {
        Init();
    }
    private void Init()
    {
        if(eventDictionary == null)
        {
            eventDictionary = new Dictionary<EventName, Action>();
        }
    }

    public static void StartListening(EventName eventName,Action listener)
    {
        if(eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] += listener;
        }
        else
        {
            eventDictionary.Add(eventName,listener);
        }
    }
    public static void StopListening(EventName eventName,Action listener)
    {
        if(eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] -= listener;
        }
        else
        {
            eventDictionary.Remove(eventName);
        }
    }
    public static bool IsListening(EventName eventName)
    {
        if(eventDictionary == null) return false;
        return eventDictionary.ContainsKey(eventName);
    }
    public static void TriggerEvent(EventName eventName)
    {
        Action thisAction = null;
        if(eventDictionary.TryGetValue(eventName,out thisAction))
        {
            thisAction?.Invoke();
        }
    }
}
