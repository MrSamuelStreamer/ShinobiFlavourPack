using RimWorld;
using Verse;
using Verse.AI;

namespace ShinobiFP.VFED;

public class JobGiver_NobleFood : ThinkNode_JobGiver
{
    private const float FoodSearchRadius = 12f;
    private const float MinNutritionWanted = 0.1f;

    protected override Job TryGiveJob(Pawn pawn)
    {
        if (pawn.Map == null)
        {
            return null;
        }

        var foodNeed = pawn.needs?.food;
        if (foodNeed == null || foodNeed.NutritionWanted < MinNutritionWanted)
        {
            return null;
        }

        var food = GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, ThingRequest.ForGroup(ThingRequestGroup.FoodSourceNotPlantOrTree), PathEndMode.ClosestTouch, TraverseParms.For(pawn, Danger.None),
            FoodSearchRadius, thing => IsValidFoodFor(pawn, thing));

        if (food == null)
        {
            return null;
        }

        var job = JobMaker.MakeJob(JobDefOf.Ingest, food);
        job.count = FoodUtility.WillIngestStackCountOf(pawn, FoodUtility.GetFinalIngestibleDef(food), FoodUtility.NutritionForEater(pawn, food));
        return job;
    }

    private static bool IsValidFoodFor(Pawn pawn, Thing thing)
    {
        return thing.def.IsNutritionGivingIngestible && thing.IngestibleNow && thing is not Corpse && !thing.IsForbidden(pawn) && !thing.IsNotFresh() && !thing.IsDessicated() && pawn.CanReserve(thing) && pawn.WillEat(thing, pawn, careIfNotAcceptableForTitle: true);
    }
}