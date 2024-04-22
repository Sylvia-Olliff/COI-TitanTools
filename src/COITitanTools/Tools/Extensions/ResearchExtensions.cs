using Mafi;
using Mafi.Collections;
using Mafi.Core.Research;

namespace COITitanTools.Tools.Extensions;

public static class ResearchExtensions
{
    public static Lyst<ResearchNodeProto> ToLyst(this ResearchNodeProto[] protos)
    {
        return new Lyst<ResearchNodeProto>(protos);
    }

    public static void SetResearchNodeParents(this ResearchNodeProto proto, params ResearchNodeProto[] parents)
    {
        proto.SetPrivateCOIProperty("m_parents", parents.ToLyst());
    }

    public static void SetResearchNodePosition(this ResearchNodeProto proto, Vector2i position)
    {
        proto.SetPrivateCOIProperty("GridPosition", position);
    }
}
