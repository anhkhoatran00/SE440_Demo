using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    private void Start()
    {
       /* MoveGameObject(() =>
        {
            Debug.Log("CallBack");
        });*/
      /*  Debug.Log("StartCallCountDown");
        StartCoroutine(counDown());
        Debug.Log("EndCallCountDown");*/

        MultiThread02();
    }
    private void MoveGameObject(Action callback)
    {
        Debug.Log("Move GO");
        callback?.Invoke();
    }
    private async void MultiThread02()
    {
        Debug.Log("Start Multi Task");
        List<UniTask> tasks = new List<UniTask>();
        tasks.Add(Taskk("Move", 2000));
        tasks.Add(Taskk("Mlem", 3000));
        tasks.Add(Taskk("Mlame", 4000));
        await UniTask.WhenAll(tasks);
        Debug.Log("Complete");
    }
    private async UniTask Taskk(string log, int delay)
    {
        Debug.Log($"Task Start{log}");
        await UniTask.Delay(delay);
        Debug.Log($"Task Done{log}");
    }
    private IEnumerator counDown()
    {
        Debug.Log("Start_CountDown");
        int countTime = 3;
        for (int i = 0; i < countTime; i++)
        {
            yield return new WaitForSeconds(1);
        }
        Debug.Log("End Count Down");
    }
}
