using Mafi;
using Mafi.Core.Prototypes;
using System;
using System.Reflection;
using UnityEngine;

namespace COITitanTools.Tools.Extensions;

// TODO: Update these extensions with enums of possible properties?
// TODO: Possibly lift Proto inheritence restriction if necessary
public static class ReflectionExtensions
{
    /// <summary>
    /// Use this extension to change the value of a private property on an existing Prototype. Will throw on failure
    /// </summary>
    /// <typeparam name="T">Value Type</typeparam>
    /// <param name="obj">Inheritor of Proto</param>
    /// <param name="propertyName">Property to set</param>
    /// <param name="value">Value being set</param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void SetPrivateCOIProperty<T>(this object obj, string propertyName, T value)
    {
        var type = obj.GetType();

        if (!type.IsSubclassOf(typeof(Proto)))
            throw new InvalidOperationException($"Attempted to set {propertyName} on {type.Name} which does not inherit from Proto!");

        // Attempt to retrieve as Property
        PropertyInfo propInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (propInfo != null && propInfo.CanWrite)
        {
            propInfo.SetValue(obj, value);
            return;
        }

        // Attempt to retrieve as Field
        FieldInfo fieldInfo = type.GetField(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (fieldInfo == null)
        {
            // If not found as primary Field, check if compiler generated Backing Field
            string backingFieldName = $"<{propertyName}k__BackingField";
            fieldInfo = type.GetField(backingFieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        }

        if (fieldInfo != null)
        {
            fieldInfo.SetValue(obj, value);
            return;
        }

        // If all else fails, use the base class type
        var baseType = typeof(T).BaseType;
        FieldInfo baseFieldInfo = baseType.GetField("<Strings>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (baseFieldInfo != null)
        {
            baseFieldInfo.SetValue(obj, value);
            return;
        }

        throw new InvalidOperationException($"Property, field, or backing field '{propertyName}' not found on {type.Name}");
    }

    /// <summary>
    /// Use this extension to get the value of a private property on an existing Prototype. Will throw on failure
    /// </summary>
    /// <typeparam name="T">Property Type</typeparam>
    /// <param name="obj">Inheritor of Proto</param>
    /// <param name="propertyName">Property to retrieve</param>
    /// <returns>Property Value</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static T GetPrivateCOIProperty<T>(this object obj, string propertyName)
    {
        var type = obj.GetType();

        if (!type.IsSubclassOf(typeof(Proto)))
            throw new InvalidOperationException($"Attempted to access private property {propertyName} on non-Proto type {type.Name}");

        // Attempt to retrieve as Property
        PropertyInfo propertyInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (propertyInfo != null && propertyInfo.CanRead)
        {
            return (T)propertyInfo.GetValue(obj);
        }

        // Attempt to retrieve as Field
        FieldInfo fieldInfo = type.GetField(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (fieldInfo is null)
        {
            // Attempt to retrieve as Backing Field
            string backingFieldName = $"<{propertyName}>k__BackingField";
            fieldInfo = type.GetField(backingFieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        }

        if (fieldInfo is not null)
        {
            return (T)fieldInfo.GetValue(obj);
        }

        // If all else fails, use the base class type
        Type baseType = typeof(T).BaseType;
        FieldInfo baseFieldInfo = baseType.GetField(propertyName, BindingFlags.Instance | BindingFlags.NonPublic);
        if (baseFieldInfo is not null)
        {
            return (T)baseFieldInfo.GetValue(obj);
        }

        throw new InvalidOperationException($"Property, field, or backing field '{propertyName}' not found on {type.Name}.");
    }

    /// <summary>
    /// Use this extension to retrieve a private static property from class that inherits from Proto
    /// </summary>
    /// <typeparam name="T">Value Type</typeparam>
    /// <param name="obj">Inheritor of Proto</param>
    /// <param name="fieldName">Static property name</param>
    /// <returns>Retrieved value</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static T GetPrivateCOIStaticProperty<T>(this object obj, string fieldName)
    {
        var type = obj.GetType();

        if (!type.IsSubclassOf(typeof(Proto)))
            throw new InvalidOperationException($"Attempted to access private static field {fieldName} on non-Proto type {type.Name}");

        // Attempt to retrieve static field
        FieldInfo fieldInfo = type.GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        if (fieldInfo is null)
        {
            // Attempt to retrieve as backing field
            string backingFieldName = $"<{fieldName}>k__BackingField";
            fieldInfo = type.GetField(backingFieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        }

        if (fieldInfo is not null)
        {
            return (T)fieldInfo.GetValue(obj);
        }

        throw new InvalidOperationException($"Static field or backing field '{fieldName}' not found on {type.Name}");
    }

    /// <summary>
    /// Use this extension to set the value of a static field on an object that inherits from Proto
    /// </summary>
    /// <typeparam name="T">Value Type</typeparam>
    /// <param name="obj">Inheritor of Proto</param>
    /// <param name="fieldName">Static field name</param>
    /// <param name="value">Value to set</param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void SetPrivateCOIStaticProperty<T>(this object obj, string fieldName, T value)
    {
        var type = obj.GetType();

        if (!type.IsSubclassOf(typeof(Proto)))
            throw new InvalidOperationException($"Attempted to access private static field {fieldName} on non-Proto type {type.Name}");

        // Attempt to retrieve static field
        FieldInfo fieldInfo = type.GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        if (fieldInfo is null)
        {
            // Attempt to retrieve as backing field
            string backingFieldName = $"<{fieldName}>k__BackingField";
            fieldInfo = type.GetField(backingFieldName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        }

        if (fieldInfo is not null)
        {
            // Allows setting of readonly fields. BE WARNED!
            fieldInfo.SetValue(null, value);
            return;
        }

        throw new InvalidOperationException($"Static field or backing field '{fieldName}' not found on {type.Name}");
    }

    /// <summary>
    /// This extension allows for the calling of private methods on ANY OBJECT. Only use if you know what you are doing!!!
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="methodName"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static object Call(this object obj, string methodName, params object[] args)
    {
        var methodInfo = obj.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (methodInfo is not null)
            return methodInfo.Invoke(obj, args);
        return null;
    }
}
