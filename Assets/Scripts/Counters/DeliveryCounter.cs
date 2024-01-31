using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There are more then 1 delivery counter!");
        }
        Instance = this;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetKitchenObject<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject))
            {
                //Only accepts plates

                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
