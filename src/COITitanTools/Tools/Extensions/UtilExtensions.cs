using COITitanTools.Interfaces;
using COITitanTools.Logging;
using Mafi;
using Mafi.Core.Products;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace COITitanTools.Tools.Extensions;

public static class UtilExtensions
{
    /// <summary>
    /// Logs all properties of given instance
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="M"></typeparam>
    /// <param name="instance"></param>
    /// <param name="includePrivate"></param>
    public static void PropsToLog<T, M>(this T instance, bool includePrivate = false) where T : class where M : ICOIMod
    {
        COILog.Info<M>($"Printing out all public properties for {instance.ToStringSafe()}");
        if (instance is null)
        {
            COILog.Info<M>("Instance is null");
            return;
        }

        Type type = instance.GetType();
        PropertyInfo[] properties = 
            includePrivate ? 
            type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) : 
            type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        COILog.Info<M>($"Properties of {type.Name}:");
        StringBuilder stringBuilder = new();

        foreach (var prop in properties)
        {
            try
            {
                object value = prop.GetValue(instance);
                stringBuilder.AppendLine($"{prop.Name} = {value}");
            }
            catch (Exception ex)
            {
                stringBuilder.AppendLine($"{prop.Name} could not be read! REASON: {ex.Message}");
            }
        }

        COILog.Info<M>(stringBuilder.ToString());
    }

    /// <summary>
    /// Set the new Icon Path for a given graphics object. Will also flag that object as having a custom Icon. 
    /// </summary>
    /// <param name="gfxObject"></param>
    /// <param name="path"></param>
    public static void SetIconPath(this ProductProto.Gfx gfxObject, string path)
    {
        gfxObject.SetPrivateCOIProperty("IconPath", path);
        gfxObject.SetPrivateCOIProperty("IconIsCustom", true);
    }
}
