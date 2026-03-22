using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab;    //Public since we will never write to a ScriptableObject
    public Sprite sprite;
    public string objectName;

}
