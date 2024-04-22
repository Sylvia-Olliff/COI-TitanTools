using Mafi;
using Mafi.Base;
using Mafi.Core.Factory.Recipes;

namespace COITitanTools.Tools.Extensions;

public static class RecipeExtensions
{
    /***
     *  BEGIN EXHAUST EXTENSIONS
     */

    [MustUseReturnValue]
    public static T AddQuarterExhaust<T>(this IRecipeProtoBuilderState<T> builder, string portSelector = "*")
    {
        return builder.AddOutput(portSelector, Ids.Products.Exhaust, (builder.RecipeDuration.Value.Seconds / 10 * 2).ToIntRounded().Quantity(), outputAtStart: true);
    }

    [MustUseReturnValue]
    public static T AddHalfExhaust<T>(this IRecipeProtoBuilderState<T> builder, string portSelector = "*")
    {
        return builder.AddOutput(portSelector, Ids.Products.Exhaust, (builder.RecipeDuration.Value.Seconds / 10 * 4).ToIntRounded().Quantity(), outputAtStart: true);
    }

    [MustUseReturnValue]
    public static T AddFullExhaust<T>(this IRecipeProtoBuilderState<T> builder, string portSelector = "*")
    {
        return builder.AddOutput(portSelector, Ids.Products.Exhaust, (builder.RecipeDuration.Value.Seconds / 10 * 8).ToIntRounded().Quantity(), outputAtStart: true);
    }

    [MustUseReturnValue]
    public static T AddDoubleExhaust<T>(this IRecipeProtoBuilderState<T> builder, string portSelector = "*")
    {
        return builder.AddOutput(portSelector, Ids.Products.Exhaust, (builder.RecipeDuration.Value.Seconds / 10 * 8 * 2).ToIntRounded().Quantity(), outputAtStart: true);
    }

    [MustUseReturnValue]
    public static T AddAirPolution<T>(this IRecipeProtoBuilderState<T> builder, Quantity quantity)
    {
        return builder.AddOutput("VIRTUAL", Ids.Products.PollutedAir, quantity);
    }

    [MustUseReturnValue]
    public static T AddAirPollutionFromBurning<T>(this IRecipeProtoBuilderState<T> builder, Quantity quantity)
    {
        return builder.AddAirPolution(2 * quantity);
    }

    [MustUseReturnValue]
    public static T AddAirPollutionFromExhaust<T>(this IRecipeProtoBuilderState<T> builder, Quantity quantity)
    {
        return builder.AddAirPolution(quantity);
    }

    [MustUseReturnValue]
    public static T AddAirPollutionFromCO2<T>(this IRecipeProtoBuilderState<T> builder, Quantity quantity)
    {
        return builder.AddAirPolution(quantity / 2);
    }

}
