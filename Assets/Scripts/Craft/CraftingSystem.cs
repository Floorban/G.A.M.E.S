using System.Collections.Generic;
using UnityEngine;


public class CraftingSystem : MonoBehaviour
{
    public List<CraftingRecipeSO> craftingRecipeSOList;
    public BoxCollider placeItemAreaBoxCollider;
    private CraftingRecipeSO craftingRecipeSO;
    public Transform itemSpawnPoint;

    private void Awake()
    {
        NextRecipe();
    }
    private void OnTriggerStay(Collider other)
    {
       
       
    }
    public void NextRecipe()
    {
        if(craftingRecipeSO == null)
        {
            craftingRecipeSO = craftingRecipeSOList[0];

        }else
        {
            int index = craftingRecipeSOList.IndexOf(craftingRecipeSO);
            index = (index + 1) % craftingRecipeSOList.Count;
            craftingRecipeSO = craftingRecipeSOList[index];
        }

    }
    public void Craft()
    {
        Debug.Log("Craft");
        Collider[] colliderArray = Physics.OverlapBox(
                transform.position + placeItemAreaBoxCollider.center,
                placeItemAreaBoxCollider.size,
                placeItemAreaBoxCollider.transform.rotation);


        List<ItemSO> inputItemList = new List<ItemSO>(craftingRecipeSO.inputItemSOList);
        List<GameObject> consumeItemGameObjectList = new List<GameObject>();
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out ItemSOHolder itemSOHolder))
            {
                if (inputItemList.Contains(itemSOHolder.itemSO))
                {
                    Debug.Log("Found item: " + itemSOHolder.itemSO.itemName);
                    inputItemList.Remove(itemSOHolder.itemSO);
                    consumeItemGameObjectList.Add(collider.gameObject);
                }
            }
        }

        if (inputItemList.Count == 0)
        {
            Debug.Log("Yes");
            Instantiate(craftingRecipeSO.outputItemSO, itemSpawnPoint.position, itemSpawnPoint.rotation);

            foreach (GameObject consumeItemGameObject in consumeItemGameObjectList)
            {
                Destroy (consumeItemGameObject);
            }
        }
    }
}
