using Mafi.Unity.UiToolkit.Component;

namespace COITitanTools.Tools.Extensions;

public static class UIExtensions
{
    /// <summary>
    /// Makes this component part of the current layout
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="component"></param>
    /// <returns></returns>
    public static T Show<T>(this T component) where T : UiComponent
    {
        component.SetVisible(true);
        return component;
    }

    /// <summary>
    /// Removes this component from the current layout
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="component"></param>
    /// <returns></returns>
    public static T Hide<T>(this T component) where T : UiComponent
    {
        component.SetVisible(false);
        return component;
    }
}
