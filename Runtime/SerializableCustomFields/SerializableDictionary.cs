using System;
using System.Collections.Generic;
using UnityEngine;

namespace scg.uitoolkit.runtime
{
    [Serializable]
    public class SerializableDictionary<TKEY, TVALUE> : ISerializationCallbackReceiver
    {
        [SerializeField] private List<DictionaryListPair<TKEY, TVALUE>> serializedList;
        private Dictionary<TKEY, TVALUE> deserializedDictionary;

        public Dictionary<TKEY, TVALUE> DeserializedDictionary { get => deserializedDictionary; set => deserializedDictionary = value; }

        public void OnBeforeSerialize()
        {
            serializedList?.Clear();
            if (DeserializedDictionary == null) return;
            if (serializedList == null) serializedList = new List<DictionaryListPair<TKEY, TVALUE>>();
            foreach (var kvp in DeserializedDictionary)
            {
                serializedList.Add(new DictionaryListPair<TKEY, TVALUE>(kvp.Key, kvp.Value));
            }
        }

        public void OnAfterDeserialize()
        {
            DeserializedDictionary = new Dictionary<TKEY, TVALUE>();
            if (serializedList == null) return;
            for (int i = 0; i < serializedList.Count; i++)
                DeserializedDictionary.Add(serializedList[i].Key, serializedList[i].Value);
        }

        public bool ContainsKey(TKEY key)
        {
            return DeserializedDictionary.ContainsKey(key);
        }

        public void Upsert(TKEY key, TVALUE value)
        {
            if (DeserializedDictionary.ContainsKey(key))
            {
                DeserializedDictionary[key] = value;
            }
            else
            {
                DeserializedDictionary.Add(key, value);
            }
        }


        public TVALUE this[TKEY key]
        {
            get => DeserializedDictionary[key];
            set => DeserializedDictionary.Add(key, value);
        }

    }

    [Serializable]
    public class DictionaryListPair<T, X>
    {
        [SerializeField] T key;
        [SerializeField] X value;

        public DictionaryListPair(T key, X value)
        {
            this.key = key;
            this.value = value;
        }

        public T Key { get => key; set => key = value; }
        public X Value { get => value; set => this.value = value; }
    }
}
