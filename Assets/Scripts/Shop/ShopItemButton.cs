using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private int itemPrice;
    private Button button;
    [SerializeField] private PlayerController player;
    [SerializeField] private ConfirmBuy confirmBuy;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);

        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }
    }

    private void OnButtonClick()
    {
        if (confirmBuy != null)
        {
            SetButtonInteractable(false);
            confirmBuy.ShowConfirmBuyPanel(item, itemPrice, this); 
        }
    }

    public void BuyItem(Item item, int quantity)
    {
        Debug.Log("check");
        int totalPrice = itemPrice * quantity;
        if (GameManager.Instance.currency.CurrentGold >= totalPrice)
        {
            GameManager.Instance.currency.CurrentGold -= totalPrice;
            for (int i = 0; i < quantity; i++)
            {
                player.inventory.Add("Backpack", item);
            }
            Debug.Log($"Purchased {item.name} for {totalPrice} gold. Remaining gold: {GameManager.Instance.currency.CurrentGold}");
        }
        else
        {
            Debug.Log("Not enough gold");
        }
    }

    public void BuyItem(Item item)
    {
        if (GameManager.Instance.currency.CurrentGold >= itemPrice)
        {
            GameManager.Instance.currency.CurrentGold -= itemPrice;
            player.inventory.Add("Backpack", item);
            Debug.Log($"Purchased {item.name} for {itemPrice} gold. Remaining gold: {GameManager.Instance.currency.CurrentGold}");
        }
        else
        {
            Debug.Log("Not enough gold");
        }
    }

    public void SetButtonInteractable(bool interactable)
    {
        button.interactable = interactable;
    }
}
