using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Spt.Config;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Routers;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Services.Mod;
using SPTarkov.Server.Core.Utils;
using System.Reflection;
using Path = System.IO.Path;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;
using SPTarkov.Server.Core.Models.Spt.Server;
using SPTarkov.Server.Core.Services;
using System.Data.Common;


namespace Legs;

// This record holds the various properties for your mod
public record ModMetadata : AbstractModMetadata
{
    public override string ModGuid { get; init; } = "com.sp-tarkov.examples.addtraderjsonassorts";
    public override string Name { get; init; } = "Legs";
    public override string Author { get; init; } = "House16";
    public override List<string>? Contributors { get; init; } = ["Clodan", "CWX"];
    public override SemanticVersioning.Version Version { get; init; } = new("1.0.0");
    public override SemanticVersioning.Range SptVersion { get; init; } = new("~4.0.0");
    public override List<string>? Incompatibilities { get; init; } = ["ReadJsonConfigExample"];
    public override Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; }
    public override string? Url { get; init; } = "https://github.com/sp-tarkov/server-mod-examples";
    public override bool? IsBundleMod { get; init; } = true;
    public override string? License { get; init; } = "MIT";
}

/// <summary>
/// Feel free to use this as a base for your mod
/// </summary>
[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 2)]
public class AddTraderWithAssortJson(
    ModHelper modHelper,
    ImageRouter imageRouter,
    ConfigServer configServer,
    TimeUtil timeUtil,
    AddCustomTraderHelper addCustomTraderHelper, // This is a custom class we add for this mod, we made it injectable so it can be accessed like other classes here
    CustomItemService customItemService,
    WTTServerCommonLib.WTTServerCommonLib wttCommon
)
    : IOnLoad
{
    private readonly TraderConfig _traderConfig = configServer.GetConfig<TraderConfig>();
    private readonly RagfairConfig _ragfairConfig = configServer.GetConfig<RagfairConfig>();



    public async Task OnLoad()
    {
        // A path to the mods files we use below
        var pathToMod = modHelper.GetAbsolutePathToModFolder(Assembly.GetExecutingAssembly());

        // A relative path to the trader icon to show
        var traderImagePath = Path.Combine(pathToMod, "data/Legs.jpg");

        // The base json containing trader settings we will add to the server
        var traderBase = modHelper.GetJsonDataFromFile<TraderBase>(pathToMod, "data/base.json");
        

       
        //New Items
        var bearHat = new NewItemFromCloneDetails
        {
            ItemTplToClone = "5b40e61f5acfc4001a599bec",
            ParentId = "5a341c4086f77401f2541505",
            NewId = "6765d79148caecc924f38301",
            FleaPriceRoubles = 100,
            HandbookPriceRoubles = 100,
            HandbookParentId = "5b47574386f77428ca22b330",
            Locales = new Dictionary<string, LocaleDetails>{
                {
                    "en", new LocaleDetails
                    {
                        Name = "BEAR 5-Panel",
                        ShortName = "BEAR Hat",
                        Description = "5 Panel hat designed for BEAR PMCs"
                    }
                }
            },
            OverrideProperties = new TemplateItemProperties
            {
                Prefab = new Prefab
                {
                    Path = "bear_hat.bundle",
                    Rcid = ""
                }
            },
        };

        customItemService.CreateItemFromClone(bearHat);

        var bearBeanie = new NewItemFromCloneDetails
        {
            ItemTplToClone = "5b40e61f5acfc4001a599bec",
            ParentId = "5a341c4086f77401f2541505",
            NewId = "67e343112c5ed0143e3c74eb",
            FleaPriceRoubles = 100,
            HandbookPriceRoubles = 100,
            HandbookParentId = "5b47574386f77428ca22b330",
            Locales = new Dictionary<string, LocaleDetails>{
                {
                    "en", new LocaleDetails
                    {
                        Name = "BEAR Beanie",
                        ShortName = "BEAR Beanie",
                        Description = "Beanie designed for BEAR PMCs"
                    }
                }
            },
            OverrideProperties = new TemplateItemProperties
            {
                Prefab = new Prefab
                {
                    Path = "bear_beanie.bundle",
                    Rcid = ""
                }
            },
        };

        customItemService.CreateItemFromClone(bearBeanie);

        var leatherBackpack = new NewItemFromCloneDetails
        {
            ItemTplToClone = "66b5f247af44ca0014063c02",
            ParentId = "5448e53e4bdc2d60728b4567",
            NewId = "679d31db3d176182a2adfd86",
            FleaPriceRoubles = 20000,
            HandbookPriceRoubles = 20000,
            HandbookParentId = "5b5f6f6c86f774093f2ecf0b",
            Locales = new Dictionary<string, LocaleDetails>{
                {
                    "en", new LocaleDetails
                    {
                        Name = "Legs' Trusty Backpack",
                        ShortName = "Leather Backpack",
                        Description = "A old leather backpack. Was used by legs often before the contract wars."
                    }
                }
            },
            OverrideProperties = new TemplateItemProperties
            {
                Prefab = new Prefab
                {
                    Path = "leather_backpack.bundle",
                    Rcid = ""
                }
            },
        };

        customItemService.CreateItemFromClone(leatherBackpack);

        var usecHat = new NewItemFromCloneDetails
        {
            ItemTplToClone = "5b40e61f5acfc4001a599bec",
            ParentId = "5a341c4086f77401f2541505",
            NewId = "679de71c3b47fe7f7537f7ff",
            FleaPriceRoubles = 100,
            HandbookPriceRoubles = 100,
            HandbookParentId = "5b47574386f77428ca22b330",
            Locales = new Dictionary<string, LocaleDetails>{
                {
                    "en", new LocaleDetails
                    {
                        Name = "USEC 5-Panel",
                        ShortName = "USEC Hat",
                        Description = "5-Panel designed for USEC PMCs"
                    }
                }
            },
            OverrideProperties = new TemplateItemProperties
            {
                Prefab = new Prefab
                {
                    Path = "usec_hat.bundle",
                    Rcid = ""
                }
            },
        };

        customItemService.CreateItemFromClone(usecHat);

        var usecBeanie = new NewItemFromCloneDetails
        {
            ItemTplToClone = "5b40e61f5acfc4001a599bec",
            ParentId = "5a341c4086f77401f2541505",
            NewId = "679d31ae779c471efb063dba",
            FleaPriceRoubles = 100,
            HandbookPriceRoubles = 100,
            HandbookParentId = "5b47574386f77428ca22b330",
            Locales = new Dictionary<string, LocaleDetails>{
                {
                    "en", new LocaleDetails
                    {
                        Name = "USEC Beanie",
                        ShortName = "USEC Beanie",
                        Description = "Beanie designed for USEC PMCs"
                    }
                }
            },
            OverrideProperties = new TemplateItemProperties
            {
                Prefab = new Prefab
                {
                    Path = "usec_beanie.bundle",
                    Rcid = ""
                }
            },
        };

        customItemService.CreateItemFromClone(usecBeanie);

        // Create a helper class and use it to register our traders image/icon + set its stock refresh time
        imageRouter.AddRoute(traderBase.Avatar.Replace(".jpg", ""), traderImagePath);
        addCustomTraderHelper.SetTraderUpdateTime(_traderConfig, traderBase, timeUtil.GetHoursAsSeconds(1), timeUtil.GetHoursAsSeconds(2));

        // Add our trader to the config file, this lets it be seen by the flea market
        _ragfairConfig.Traders.TryAdd(traderBase.Id, true);

        // Add our trader (with no items yet) to the server database
        // An 'assort' is the term used to describe the offers a trader sells, it has 3 parts to an assort
        // 1: The item
        // 2: The barter scheme, cost of the item (money or barter)
        // 3: The Loyalty level, what rep level is required to buy the item from trader
        addCustomTraderHelper.AddTraderWithEmptyAssortToDb(traderBase);

        // Add localisation text for our trader to the database so it shows to people playing in different languages
        addCustomTraderHelper.AddTraderToLocales(traderBase, "Legs", "Welcome to Leg's");

        // Get the assort data from JSON
        var assort = modHelper.GetJsonDataFromFile<TraderAssort>(pathToMod, "data/assort.json");

        //Quest import using WTT COMMON LIB
        var assembly = Assembly.GetExecutingAssembly();

        await wttCommon.CustomQuestService.CreateCustomQuests(assembly);

        // Save the data we loaded above into the trader we've made
        addCustomTraderHelper.OverwriteTraderAssort(traderBase.Id, assort);

        // Send back a success to the server to say our trader is good to go
        await Task.CompletedTask;
    }
}