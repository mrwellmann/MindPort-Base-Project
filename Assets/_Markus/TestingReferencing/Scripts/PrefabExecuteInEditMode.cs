using System;
using UnityEngine;

[ExecuteInEditMode]
public class PrefabExecuteInEditMode : MonoBehaviour
{
    [SerializeField]
    private string guid = string.Empty;

    public Guid Guid
    {
        get
        {
            Guid realGuid;
            if (!Guid.TryParse(guid, out realGuid))
            {
                realGuid = Guid.NewGuid();
                guid = realGuid.ToString();
            }
            return realGuid;
        }
        set
        {
            guid = value.ToString();
        }
    }

    protected void Awake()
    {
        Debug.Log($"PrefabExecuteInEditMode Start {this.name}, instanceID {gameObject.GetInstanceID()}, newGuid {guid} ");
    }

    protected void Start()
    {
        var newGuid = Guid.NewGuid();
        Debug.Log($"PrefabExecuteInEditMode Awake {this.name}, instanceID {gameObject.GetInstanceID()}, oldGuid {guid}, newGuid {newGuid}");
        Guid = newGuid;
    }

    protected void OnEnable()
    {
        Debug.Log($"PrefabExecuteInEditMode OnEnable {this.name}, instanceID {gameObject.GetInstanceID()}, newGuid {guid} ");
    }

    protected void OnDisable()
    {
        Debug.Log($"PrefabExecuteInEditMode OnDisable {this.name}, instanceID {gameObject.GetInstanceID()}, newGuid {guid} ");
    }

    protected void OnDestroy()
    {
        Debug.Log($"PrefabExecuteInEditMode OnDestroy {this.name}, instanceID {gameObject.GetInstanceID()}, newGuid {guid} ");
    }
}
