using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            // 인스턴스가 없을 경우 씬에서 찾아보고, 그래도 없으면 에러를 띄우거나 새로 만듦
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    Debug.LogError($"{typeof(T).Name} 싱글톤이 씬에 없습니다!");
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(this.gameObject); // 씬이 넘어가도 파괴되지 않게 유지
        }
        else if (_instance != this as T)
        {
            Destroy(this.gameObject); // 중복 방지
        }
    }
}