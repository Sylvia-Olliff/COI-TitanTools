using Mafi.Core.Mods;

namespace COITitanTools.Interfaces;

public interface ICOIMod : IMod
{
    string LoggingPrefix { get; }
}
