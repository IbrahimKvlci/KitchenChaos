using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetKitchenObject<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject))
            {
                //Only accepts plates
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
