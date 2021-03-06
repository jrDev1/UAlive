﻿using Ludiq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lasm.UAlive
{
    public static partial class HUMQuery
    {
        /// <summary>
        /// Performs a for loop with an action.
        /// </summary>
        public static Data.For<T> For<T>(this IList<T> collection, System.Action<IList<T>, int> action)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                action(collection, i);
            }

            return new Data.For<T>(collection);
        }

        /// <summary>
        /// Begins a for operation on a collection.
        /// </summary>
        public static Data.For<T> For<T>(this IList<T> collection)
        {
            return new Data.For<T>(collection);
        }

        public static TKey[] KeysToArray<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            return dictionary.Keys.ToArray();
        }

        public static List<TKey> KeysToList<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            return dictionary.Keys.ToList();
        }

        public static TValue[] ValuesToArray<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            return dictionary.Values.ToArray();
        }

        public static List<TValue> ValuesToList<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            return dictionary.Values.ToList();
        }

        /// <summary>
        /// Defines new keys in a dictionary if it does not exist.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="deserializedDictionary"></param>
        /// <param name="key"></param>
        /// <param name="onCreated"></param>
        /// <param name="onAdded"></param>
        /// <returns></returns>
        public static TValue Define<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Dictionary<TKey, TValue> deserializedDictionary, TKey key, Func<TValue, TValue> onCreated, Action<TValue> exists)
        {
            TValue value = (TValue)typeof(TValue).Default();

            if (!deserializedDictionary.ContainsKey(key))
            {
                value = onCreated(value);
            }
            else
            {
                value = deserializedDictionary[key];
                dictionary.Add(key, value);
                exists?.DynamicInvoke(value);
            }

            return value; 
        } 

        /// <summary>
        /// Removes all unused values from one dictionary, that don't exist in another.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        public static void Undefine<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Dictionary<TKey, TValue> deserializedDictionary, Action<TValue> onRemoved)
        {
            var keys = deserializedDictionary.Keys.ToList();
            var values = deserializedDictionary.Values.ToList();

            for (int i = 0; i < deserializedDictionary.Count; i++)
            {
                if (!dictionary.ContainsKey(keys[i]))
                {
                    var value = deserializedDictionary[keys[i]];
                    deserializedDictionary.Remove(keys[i]);
                    onRemoved(value);
                }
            }
        }

        /// <summary>
        /// Removes all unused values from one dictionary, that don't exist in another.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        public static void Undefine<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Dictionary<TKey, TValue> deserializedDictionary, Action<TValue> onRemoved, ref bool isNew)
        {
            if (!isNew)
            {
                var keys = deserializedDictionary.Keys.ToList();
                var values = deserializedDictionary.Values.ToList();

                for (int i = 0; i < deserializedDictionary.Count; i++)
                {
                    if (!dictionary.ContainsKey(keys[i]))
                    {
                        var value = deserializedDictionary[keys[i]];
                        deserializedDictionary.Remove(keys[i]);
                        onRemoved(value);
                    }
                }
            }

            isNew = true;
        }

        public static void MergeUnique<T>(this List<T> list, List<T> other)
        {
            for (int i = 0; i < other.Count; i++)
            {
                if (!list.Contains(other[i])) list.Add(other[i]);
            }
        }
    }
}