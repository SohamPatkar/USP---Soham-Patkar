using UnityEngine;
using Items;
using System;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public event Action<string> OnItemFound;


        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

                if (hit.collider != null)
                {
                    Debug.Log("Hit: " + hit.collider.name);
                    var itemData = hit.collider.GetComponent<Item>();
                    if (itemData != null)
                    {
                        itemData.ChangeItemState(ItemState.Found);
                        OnItemFound?.Invoke(itemData.itemName);
                        Debug.Log($"Item {itemData.itemName} found!");
                        Destroy(itemData.gameObject);
                    }
                }
            }
        }
    }
}


