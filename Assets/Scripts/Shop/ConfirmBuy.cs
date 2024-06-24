using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmBuy : MonoBehaviour
{
    [SerializeField] private GameObject confirmBuyField;
    [SerializeField] private Button addButton;
    [SerializeField] private Button removeButton;
    [SerializeField] private Button confirmButton;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Color normalColor = Color.white; 
    [SerializeField] private Color unbuyableColor = new Color(0.39f, 0.39f, 0.39f); 

    private int buyQuantity = 1;
    private Item currentItem;
    private int itemPrice;
    private ShopItemButton currentShopItemButton;

    private void Start()
    {
        addButton.onClick.AddListener(OnAddButtonClick);
        removeButton.onClick.AddListener(OnRemoveButtonClick);
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
    }

    public void ShowConfirmBuyPanel(Item item, int price, ShopItemButton shopItemButton)
    {
        currentItem = item;
        itemPrice = price;
        buyQuantity = 1;
        currentShopItemButton = shopItemButton;
        UpdateUI();

        if (confirmBuyField != null)
        {
            confirmBuyField.SetActive(true);
        }
    }

    private void UpdateUI()
    {
        if (itemNameText != null)
        {
            itemNameText.text = currentItem.name;
        }

        if (quantityText != null)
        {
            quantityText.text = buyQuantity.ToString();
        }

        UpdateButtonColor();
    }

    private void UpdateButtonColor()
    {
        int totalPrice = itemPrice * buyQuantity;
        if (GameManager.Instance.currency.CurrentGold >= totalPrice)
        {
            confirmButton.image.color = normalColor;
        }
        else
        {
            confirmButton.image.color = unbuyableColor;
        }
    }

    private void OnAddButtonClick()
    {
        buyQuantity++;
        UpdateUI();
    }

    private void OnRemoveButtonClick()
    {
        if (buyQuantity > 1)
        {
            buyQuantity--;
        }
        UpdateUI();
    }

    private void OnConfirmButtonClick()
    {
        if (currentShopItemButton != null)
        {
            currentShopItemButton.BuyItem(currentItem, buyQuantity);
            UpdateUI();
        }
    }

    public void CloseField()
    {
        if (confirmBuyField != null)
        {
            confirmBuyField.SetActive(false);
        }

        if (currentShopItemButton != null)
        {
            currentShopItemButton.SetButtonInteractable(true);
        }
    }
}
