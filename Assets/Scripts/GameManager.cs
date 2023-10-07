using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Fields
    public static GameManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] protected bool dontDestroyOnLoad = true;
    [Header("Random")]
    [SerializeField] private int seed;
    [Header("References")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button retryButtonWin;
    [SerializeField] private Button retryButtonLost;
	[SerializeField] private GameObject winPanel;
	[SerializeField] private GameObject losePanel;
    [SerializeField] private EnemyManager enemyManager;

    private int score;
	private int maxScore;
    #endregion

    #region Properties
    #endregion

    #region Unity Messages

    private void Awake()
	{
        // inicializa a funçao que faz esse manager persistir em qualquer cena e ser accessado sem precisar de referencia
        InitializeSingleton();
		Random.InitState(seed);

		// colocando o Retry no clique do botão por codigo
		retryButtonWin.onClick.AddListener(Retry);
		retryButtonLost.onClick.AddListener(Retry);

		maxScore = enemyManager.Enemies.Count;

		// reseta o time scale
		Time.timeScale = 1f;
	}

	private void OnDestroy()
	{
		// removendo a funçào do clique quando o objeto for destruido
		// porque pode acontecer de ficar registrado a funçao e gerar erros
		retryButtonWin.onClick.RemoveListener(Retry);
		retryButtonLost.onClick.RemoveListener(Retry);
	}

	private void Update()
    {
        if (score >= maxScore)
            Win();
    }
    #endregion

    #region Public Methods
    public static void IncreaseScore()
    {
        Instance.score++;
        Instance.UpdateScore();
    }

    public void Retry()
    {
        SceneManager.LoadScene("Level 1");
		Time.timeScale = 1f;
	}

	public void Win()
	{
		winPanel.SetActive(true);
		losePanel.SetActive(false);
		Time.timeScale = 0f;
	}
	public void Defeat()
	{
		winPanel.SetActive(false);
		losePanel.SetActive(true);
		Time.timeScale = 0f;
	}
	#endregion

	#region Private Methods
	private void InitializeSingleton()
    {
		if (Instance == null)
		{
			Instance = this;

			if (dontDestroyOnLoad)
				DontDestroyOnLoad(gameObject);
		}
		else if (Instance != this)
		{
			Destroy(Instance.gameObject);
			Instance = this;

			if (dontDestroyOnLoad)
				DontDestroyOnLoad(gameObject);
		}
	}

    private void UpdateScore()
    {
		scoreText.text = $"Defeated {score} out of {maxScore}";
    }
	#endregion
}
