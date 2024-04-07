using UnityEngine;

public class UIManager : BaseManager<UIManager>
{
    [SerializeField] private MenuPanel menuPanel;
    [SerializeField] private GamePanel gamePanel;
    [SerializeField] private SettingPanel settingPanel;
    [SerializeField] private LosePanel losePanel;
    [SerializeField] private WinPanel winPanel;
    [SerializeField] private PausePanel pausePanel;
    [SerializeField] private LevelSelectPanel levelSelectPanel;
 
    public MenuPanel MenuPanel => menuPanel;
    public GamePanel GamePanel => gamePanel;
    public SettingPanel SettingPanel => settingPanel;
    public LosePanel LosePanel => losePanel;
    public WinPanel WinPanel => winPanel;
    public LevelSelectPanel LevelSelectPanel => levelSelectPanel;
    public PausePanel PausePanel => pausePanel;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        ActiveMenuPanel(true);
        ActiveGamePanel(false);
        ActiveSettingPanel(false);
        ActiveLosePanel(false);
        ActiveWinPanel(false);
        ActivePausePanel(false);
        ActiveLevelSelectPanel(false);
    }

    public void ActiveMenuPanel(bool active)
    {
        menuPanel.gameObject.SetActive(active);
    }

    public void ActiveGamePanel(bool active)
    {
        gamePanel.gameObject.SetActive(active);
    }

    public void ActiveSettingPanel(bool active)
    {
        settingPanel.gameObject.SetActive(active);
    }

    public void ActiveLosePanel(bool active)
    {
        losePanel.gameObject.SetActive(active);
    }

    public void ActiveWinPanel(bool active)
    {
        winPanel.gameObject.SetActive(active);
    }

    public void ActivePausePanel(bool active)
    {
        pausePanel.gameObject.SetActive(active);
    }

    public void ActiveLevelSelectPanel(bool active)
    {
        levelSelectPanel.gameObject.SetActive(active);
    }
}

