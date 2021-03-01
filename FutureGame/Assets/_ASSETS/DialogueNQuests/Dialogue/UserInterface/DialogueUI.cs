using UnityEngine;
using TMPro;
using Finark.Dialogue;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Transform choiceRoot;
    [SerializeField] private GameObject choicePrefab;
    [SerializeField] private GameObject AIResponse;
    [SerializeField] private TextMeshProUGUI conversantName;

    private PlayerConversant _playerConversant;

    private void Awake()
    {
        _playerConversant = FindObjectOfType<PlayerConversant>();
        _playerConversant.onConversationUpdated += UpdateUI;
        nextButton.onClick.AddListener(_playerConversant.Next);
        quitButton.onClick.AddListener(_playerConversant.Quit);
    }

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {

        gameObject.SetActive(_playerConversant.IsActive());
        if (!_playerConversant.IsActive())
        {
            return;
        }

        conversantName.text = _playerConversant.GetCurrentConversantName();

        AIResponse.SetActive(!_playerConversant.IsChoosing());
        choiceRoot.gameObject.SetActive(_playerConversant.IsChoosing());
        if (_playerConversant.IsChoosing())
        {
            BuildChoiceList();
        }
        else
        {
            dialogueText.text = _playerConversant.GetText();
            nextButton.gameObject.SetActive(_playerConversant.HasNext());
        }
    }

    private void BuildChoiceList()
    {
        foreach (Transform item in choiceRoot)
        {
            Destroy(item.gameObject);
        }
        foreach (DialogueNode choice in _playerConversant.GetChoices())
        {
            GameObject newChoice = Instantiate(choicePrefab, choiceRoot);
            newChoice.GetComponentInChildren<TextMeshProUGUI>().text = choice.GetText();
            Button button = newChoice.GetComponentInChildren<Button>();
            button.onClick.AddListener(() => 
            {
                _playerConversant.SelectChoice(choice);
            });
        }
    }
}
