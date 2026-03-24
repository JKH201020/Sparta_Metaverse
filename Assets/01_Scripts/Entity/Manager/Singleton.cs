using UnityEngine;

// where T : MonoBehaviour는 "T는 반드시 유니티 컴포넌트여야 해!"라는 안전장치
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            // 인스턴스가 없을 경우 씬에서 찾아보고, 그래도 없으면 에러를 띄우거나 새로 만들음
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    Debug.LogError($"{typeof(T).Name} 싱글톤이 씬에 없음");
                }
            }

            return instance;
        }
    }

    // 자식 클래스에서도 Awake를 쓸 수 있게 함
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this); // 씬이 넘어가도 파괴되지 않게 유지
        }
        else if (instance != this as T)
        {
            Destroy(this); // 중복 방지
        }
    }
}