using COITitanTools.Logging;
using Mafi;
using Mafi.Core.Buildings.Farms;
using Mafi.Core.Entities;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using System;
using System.Reflection;

namespace COITitanTools.Tools.Extensions;

public static class ProtoExtensions
{
    public static void ChangeName(this Proto proto, Proto.Str newName)
    {
        var baseType = typeof(ProductProto).BaseType;
        FieldInfo fieldInfo = baseType.GetField("<Strings>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance);
        
        fieldInfo?.SetValue(proto, newName);
    }

    public static void ChangeCosts(this FarmProto farmProto, EntityCosts newCosts)
    {
        // Costs property is on EntityProto
        Type entityType = typeof(EntityProto);

        FieldInfo costsField = entityType.GetField("<Costs>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
        if (costsField is not null)
        {
            costsField.SetValue(farmProto, newCosts);
            COILog.Info("Costs updated successfully");
        }
        else
            COILog.Info("Costs backing field not found");
    }
}
