// Copyright (C) IHS Markit. All Rights Reserved.
// Notice: The information, data, processing technology, software (including source code), technical and intellectual concepts and processes and all other materials provided (collectively the "Property") are Copyright © IHS Markit and/or its affiliates (together "IHS Markit") and constitute the proprietary and confidential information of IHS Markit. IHS Markit reserves all rights in and to the Property. Any copying, reproduction, distribution, transmission or disclosure of the Property, in any form, is strictly prohibited without the prior written consent of IHS Markit. Unless otherwise agreed in writing, the Property is provided on an "as is" basis and IHS Markit makes no warranty, express or implied, as to its accuracy, completeness, timeliness, or to any results to be obtained by recipient nor shall IHS Markit in any way be liable to any recipient for any inaccuracies, errors or omissions in the Property. Without limiting the generality of the foregoing, IHS Markit shall have no liability whatsoever to any recipient of the Property, whether in contract, in tort (including negligence), under warranty, under statute or otherwise, in respect of any loss or damage suffered by any recipient as a result of or in connection with such Property, or any course of action determined, by it or any third party, whether or not based on the Property. The IHS Markit logo is a registered trademark of IHS Markit, and the trademarks of IHS Markit used herein are protected by international laws. Any other names may be trademarks of their respective owners.

using System.Collections.Generic;

namespace Common
{
    public static class DictionaryExtensions
    {
        public static void AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, KeyValuePair<TKey, TValue> item)
        {
            if (dictionary.ContainsKey(item.Key))
            {
                dictionary[item.Key] = item.Value;
            }
            else
            {
                dictionary.Add(item.Key, item.Value);
            }
        }

        public static void AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey item, TValue value)
        {
            if (dictionary.ContainsKey(item))
            {
                dictionary[item] = value;
            }
            else
            {
                dictionary.Add(item, value);
            }
        }
    }
}