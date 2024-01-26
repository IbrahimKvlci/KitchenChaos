using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() 
    {
        return kitchenObjectSO;
    }

    public IKitchenObjectParent GetKitchenObjectParent() 
    { 
        return kitchenObjectParent; 
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) 
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent= kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("KitchenObjectParent already has a KitchenObject!");
        }

        kitchenObjectParent.SetKitchenObject(this);
        transform.parent=kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition=Vector3.zero;
    }

    public bool TryGetKitchenObject<T>(out T kitchenObject) where T : KitchenObject
    {
        if(this is T)
        {
            kitchenObject = this as T;
            return true;
        }
        else
        {
            kitchenObject = null;
            return false;
        }
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        
        return kitchenObject;
    }
}
