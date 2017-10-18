using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{    
    [SerializeField]
    private GameObject Root;
    [SerializeField]
    private GameObject Prefab;
    [SerializeField]
    private int StartCount;
    public List<GameObject> Items = new List<GameObject>();

    public void Awake()
    {
        if (StartCount > Items.Count) Generate();
    }

    private void Generate()
    {
        var count = StartCount - Items.Count;
        for (int i = 0; i < count; i++)
        {
            var obj = AddChild(Root, Prefab);
            obj.SetActive(false);
            Items.Add(obj);
        }
    }

    public GameObject Get()
    {
        for( int i = 0; i < Items.Count; i++ ) {
			if (!Items[i].activeSelf) {
				Items[ i ].SetActive(true);
				return Items[ i ];
			}
        }

        Items.Add(AddChild(Root, Prefab));
		Items[ Items.Count - 1 ].SetActive(true);
		return Items[ Items.Count - 1 ];
    }

    public void HideAll()
    {
        for( int i = 0; i < Items.Count; i++ ) {
            Items[i].SetActive( false );
        }
    }

    GameObject AddChild(GameObject parent, GameObject prefab)
    {
        GameObject go = GameObject.Instantiate(prefab) as GameObject;

        if (go != null && parent != null)
        {
            Transform t = go.transform;
            t.SetParent(parent.transform);
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            if (t is RectTransform)
            {
                RectTransform rt = t as RectTransform;
                RectTransform prt = prefab.transform as RectTransform;
                rt.anchoredPosition = prt.anchoredPosition;
                rt.sizeDelta = prt.sizeDelta;
            }
            go.layer = parent.layer;
        }
        return go;
    }
}