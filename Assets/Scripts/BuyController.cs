using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] GameObject playerGO;
    [SerializeField] UIController uIController;
    [SerializeField] Button[] skinButtons;//For more skins to be added.
    [SerializeField] int coins;
#pragma warning restore 0649


    public void BuyUseSkin(int index)
    {
        if (index > 0 && PlayerPrefs.GetInt("Duck", 0) == 0)
        {
            BuySkin(index);
        }
        else
        {
            UseSkin(index);
        }
        PlayerPrefs.Save();
    }
    public void UseSkin(int index)
    {
        playerGO.GetComponent<SwitchModel>().SwitchPlayer(index);
        PlayerPrefs.SetInt("CurrentSkin", index);
    }
    public void BuySkin(int index)
    {
        if (coins >= 15)
        {
            coins -= 15;
            skinButtons[index].GetComponentInChildren<TextMeshProUGUI>().text = "Use";
            UseSkin(index);
            uIController.UpdateCoins(coins);
            PlayerPrefs.SetInt("Duck", 1);//Using int 0/1 for bool F/T. Update saving method in future iterations. 
        }
    }

    private void Awake()
    {
        playerGO = playerGO == null ? FindObjectOfType<Player>().gameObject : playerGO;
        uIController = uIController == null ? FindObjectOfType<UIController>() : uIController;

        playerGO.GetComponent<SwitchModel>().SwitchPlayer(PlayerPrefs.GetInt("CurrentSkin"));
    }
    private void Start()
    {
        coins = PlayerPrefs.GetInt("Coins", -1);
        if (coins == -1)
        {
            PlayerPrefs.SetInt("Coins", 0);
            coins = 0;
        }
        if (PlayerPrefs.GetInt("Duck", 0) != 0)
        {
            skinButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Use";
        }
        uIController.UpdateCoins(coins);
    }
}
