using COITitanTools.Interfaces;
using Mafi;
using Mafi.Base;
using Mafi.Collections;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core;
using Mafi.Core.Game;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using System;

namespace COITitanTools;

public sealed class COITitanTools : IRegistrationMod<ICOIMod>
{

    // Name of this mod. It will be eventually shown to the player.
    public static Version ModVersion = new(0, 0, 1);

    public int Version => ModVersion.Major;

    public string Name => "COI Titan Tools";

    public bool IsUiOnly => false;

    public Option<IConfig> ModConfig => throw new NotImplementedException();

    public COITitanTools(CoreMod coreMod, BaseMod baseMod) {
		// You can use Log class for logging. These will be written to the log file
		// and can be also displayed in the in-game console with command `also_log_to_console`.
		Log.Info("[COITT]: constructed");
	}


	public void RegisterPrototypes(ProtoRegistrator registrator) {
		Log.Info("[COITT]: registering prototypes");
		// Register all prototypes here.

		// Registers all products from this assembly. See ExampleModIds.Products.cs for examples.
		registrator.RegisterAllProducts();
	

		// Registers all research from this assembly. See ExampleResearchData.cs for examples.
		registrator.RegisterDataWithInterface<IResearchNodesData>();
	}

    public void Register(ImmutableArray<ICOIMod> mods, RegistrationContext context)
    {
        
    }

    public void RegisterDependencies(DependencyResolverBuilder depBuilder, ProtosDb protosDb, bool gameWasLoaded)
    {
        
    }

    public void EarlyInit(DependencyResolver resolver)
    {
        
    }

    public void Initialize(DependencyResolver resolver, bool gameWasLoaded)
    {
        
    }
}