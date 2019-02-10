using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDictionary<Tkey1, Tkey2, Tvalue>
{
    private Dictionary<Tkey1, Dictionary<Tkey2, Tvalue>> dict =
        new Dictionary<Tkey1, Dictionary<Tkey2, Tvalue>>();

    public Tvalue this[Tkey1 key1, Tkey2 key2]
    {
        get
        {
            return dict[key1][key2];
        }

        set
        {
            if (!dict.ContainsKey(key1))
            {
                dict[key1] = new Dictionary<Tkey2, Tvalue>();
            }
            dict[key1][key2] = value;
        }
    }
}