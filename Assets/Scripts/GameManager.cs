using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public static GameManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    [SerializeField] UIManager uiManagerPrefab;
    [SerializeField] LoadManager loadManagerPrefab;

    private UIManager uiManager;
    private LoadManager loadManager;

    private void Awake()
    {
        Instance = this;
        Initialize();
    }

    private void Initialize()
    {
        uiManager = Instantiate(uiManagerPrefab);
        loadManager = Instantiate(loadManagerPrefab);
    }
}
