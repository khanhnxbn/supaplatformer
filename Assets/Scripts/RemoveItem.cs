using UnityEngine;

public class RemoveItem : MonoBehaviour
{
    public Item item; // This will be assigned in ListItem()

    public void RemoveFromInventory()
    {
        InventoryManager.Instance.Remove(item);
        Destroy(this.transform.parent.gameObject);
    }
}
