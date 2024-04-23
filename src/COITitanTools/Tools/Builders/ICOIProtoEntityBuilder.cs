using Mafi.Core.Entities;
using Mafi.Core.Prototypes;

namespace COITitanTools.Tools.Builders;

public interface ICOIProtoEntityBuilder<T> where T : EntityProto
{
    public T Build();
}
