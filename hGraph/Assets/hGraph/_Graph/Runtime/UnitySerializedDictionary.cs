using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitySerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<KeyValueData> keyValueData = new List<KeyValueData>();
    public void OnAfterDeserialize()
    {
        this.Clear();
        foreach (var item in this.keyValueData)
        {
            this[item.Key] = item.Value;
        }
    }
    public void OnBeforeSerialize()
    {
        this.keyValueData.Clear();
        foreach (var kvp in this)
        {
            this.keyValueData.Add(new KeyValueData() {Key = kvp.Key, Value = kvp.Value} );
        }
    }
    [System.Serializable]
    public class KeyValueData
    {
        public TKey Key;
        public TValue Value;
    }
}