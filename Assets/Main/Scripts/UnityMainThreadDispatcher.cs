using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 线程安全的Unity主线程调度器
/// 作者: Pim de Witte, 项目地址: https://github.com/PimDeWitte/UnityMainThreadDispatcher
/// </summary>
public class UnityMainThreadDispatcher : MonoBehaviour
{
    private static readonly Queue<System.Action> _executionQueue = new Queue<System.Action>();
    private static UnityMainThreadDispatcher _instance = null;

    public static UnityMainThreadDispatcher Instance()
    {
        if (!_instance)
        {
            // 在场景中查找现有的调度器对象
            _instance = FindObjectOfType<UnityMainThreadDispatcher>();
            
            if (!_instance)
            {
                // 如果不存在，创建一个新的GameObject并挂载脚本
                var obj = new GameObject("UnityMainThreadDispatcher");
                _instance = obj.AddComponent<UnityMainThreadDispatcher>();
                DontDestroyOnLoad(obj); // 场景切换时不销毁
            }
        }
        return _instance;
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    /// <summary>
    /// 将Action添加到主线程执行队列
    /// </summary>
    public void Enqueue(System.Action action)
    {
        lock (_executionQueue)
        {
            _executionQueue.Enqueue(action);
        }
    }

    /// <summary>
    /// 将IEnumerator协程添加到主线程执行队列
    /// </summary>
    public void Enqueue(IEnumerator action)
    {
        lock (_executionQueue)
        {
            _executionQueue.Enqueue(() => StartCoroutine(action));
        }
    }

    void Update()
    {
        // 在主线程的Update中执行队列中的所有任务
        lock (_executionQueue)
        {
            while (_executionQueue.Count > 0)
            {
                _executionQueue.Dequeue()?.Invoke();
            }
        }
    }
}