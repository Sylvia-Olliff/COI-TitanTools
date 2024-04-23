using COITitanTools.Logging;
using Mafi;
using Mafi.Collections;
using Mafi.Core.Buildings.Farms;
using Mafi.Core.Entities;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using Mafi.Core.Research;
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

    public static void ChangeCosts(this EntityProto proto, EntityCosts newCosts)
    {
        Type entityType = typeof(EntityProto);

        FieldInfo costsField = entityType.GetField("<Costs>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
        if (costsField is not null)
        {
            costsField.SetValue(proto, newCosts);
            COILog.Info("Costs updated successfully");
        }
        else
            COILog.Info("Costs backing field not found");
    }

    public static void ChangeIcon(this Proto proto, string iconPath)
    {
        var gfxProperty = proto.GetType().GetProperty("Graphics", BindingFlags.Public | BindingFlags.Instance);
        if (gfxProperty is not null)
        {
            var gfxObject = gfxProperty.GetValue(proto);
            if (gfxObject is not null)
            {
                PropertyInfo iconPathProperty = gfxObject.GetType().GetProperty("IconPath", BindingFlags.Instance | BindingFlags.NonPublic);
                FieldInfo iconIsCustomField = gfxObject.GetType().GetField("<IconIsCustom>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);

                if (iconIsCustomField is not null && iconPathProperty is not null)
                {
                    iconPathProperty.SetValue(gfxObject, iconPath);
                    iconIsCustomField.SetValue(gfxObject, true);
                }
                else
                    throw new InvalidOperationException("IconPath property or IconIsCustom field not found.");
            }
            else
                throw new InvalidOperationException("Graphics object is null.");
        }
        else
            throw new InvalidOperationException("Graphics property not found on Proto.");
    }
}
