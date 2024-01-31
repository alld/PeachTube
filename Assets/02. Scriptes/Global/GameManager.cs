using GameEngine.Event;
using UnityEngine;

/// <summary>
/// [매니저] 게임에 전반적인 흐름을 담당하는 매니저입니다. 
/// </summary>
[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 게임 매니저의 기본 기능을 사용할때 쓰입니다. 
    /// </summary>
    public static GameManager Instance;

    /// <summary>
    /// 모든 씬에서 제한없이 사용가능한 이벤트들을 관리하는 매니저입니다.
    /// </summary>
    public Manager_Event Event = new Manager_Event();


    private void Awake()
    {
        #region 싱글턴
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
            Destroy(this.gameObject);
        #endregion
    }
}
