using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static bool DeepCompared(this object obj, object another)
    {
        if (ReferenceEquals(obj, another)) return true;
        if ((obj == null) || (another == null)) return false;
        //Compare two object's class, return false if they are difference
        if (obj.GetType() != another.GetType()) return false;

        var result = true;
        //Get all properties of obj
        //And compare each other
        foreach (var property in obj.GetType().GetProperties())
        {
            var objValue = property.GetValue(obj);
            var anotherValue = property.GetValue(another);
            if (!objValue.Equals(anotherValue)) result = false;
        }

        return result;
    }

    public static bool CompareEx(this object obj, object another)
    {
        if (ReferenceEquals(obj, another)) return true;
        if ((obj == null) || (another == null)) return false;
        if (obj.GetType() != another.GetType()) return false;

        //properties: int, double, DateTime, etc, not class
        if (!obj.GetType().IsClass) return obj.Equals(another);

        var result = true;
        foreach (var property in obj.GetType().GetProperties())
        {
            var objValue = property.GetValue(obj);
            var anotherValue = property.GetValue(another);
            //Recursion
            if (!objValue.DeepCompared(anotherValue)) result = false;
        }
        return result;
    }

    public static bool JsonCompare(this object obj, object another)
    {
        if (ReferenceEquals(obj, another)) return true;
        if ((obj == null) || (another == null)) return false;
        if (obj.GetType() != another.GetType()) return false;

        var objJson = JsonConvert.SerializeObject(obj);
        var anotherJson = JsonConvert.SerializeObject(another);

        return objJson == anotherJson;
    }
}
