using COITitanTools.Interfaces;
using Mafi;
using Mafi.Collections;
using System;
using System.Linq;

namespace COITitanTools.Logging;

public static class COILog
{
    private static readonly Dict<Type, string> _modPrefixes = new();

    internal static void RegisterMod(ICOIMod mod)
    {
        if (_modPrefixes.ContainsKey(mod.GetType()) || string.IsNullOrEmpty(mod.LoggingPrefix))
            return;
        _modPrefixes.Add(mod.GetType(), mod.LoggingPrefix);
    }

    internal static void Info(string message) => Log.Info($"[COITT] - {message}");

    internal static void Debug(string message) => Log.Debug($"[COITT] - {message}");

    internal static void Warn(string message) => Log.Warning($"[COITT] - {message}");

    internal static void Error(string message) => Log.Error($"[COITT] - {message}");

    internal static void Error(string message, Exception exception) => Log.Exception(exception, $"[COITT] - {message}");

    private static string GetPrefix(Type type)
    {
        var prefix = _modPrefixes.ContainsKey(type) ? _modPrefixes[type] : "UNK-MOD";
        if (prefix.First() != '[')
            prefix = $"[{prefix}]";

        return prefix;
    }

    /// <summary>
    /// Log to Mafi.Log using your mod's registered logging prefix
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    public static void Info<T>(string message) where T : ICOIMod => Log.Info($"{GetPrefix(typeof(T))} - {message}");

    /// <summary>
    /// Log to Mafi.Log using your mod's registered logging prefix
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    public static void Debug<T>(string message) where T : ICOIMod => Log.Debug($"{GetPrefix(typeof(T))} - {message}");

    /// <summary>
    /// Log to Mafi.Log using your mod's registered logging prefix
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    public static void Warn<T>(string message) where T : ICOIMod => Log.Warning($"{GetPrefix(typeof(T))} - {message}");

    /// <summary>
    /// Log to Mafi.Log using your mod's registered logging prefix
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    public static void Error<T>(string message) where T : ICOIMod => Log.Error($"{GetPrefix(typeof(T))} - {message}");

    /// <summary>
    /// Log to Mafi.Log using your mod's registered logging prefix
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    public static void Error<T>(string message, Exception exception) where T : ICOIMod => Log.Exception(exception, $"{GetPrefix(typeof(T))} - {message}");
}
