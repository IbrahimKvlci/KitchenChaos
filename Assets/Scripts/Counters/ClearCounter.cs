using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter,IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is no kitchen object
            if (player.HasKitchenObject())
            {
                //Player has a kitchen object
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //Player has not a kitchen object
            }
        }
        else
        {
            //There is a kitchen object
            if (player.HasKitchenObject())
            {
                //Player has a kitchen object
                if(player.GetKitchenObject().TryGetKitchenObject<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject))
                {
                    //Player is holding Plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    //Player is not holding plate but sth else
                    if(GetKitchenObject().TryGetKitchenObject<PlateKitchenObject>(out plateKitchenObject))
                    {
                        //Counter is holding a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                //Player has not a kitchen object
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
