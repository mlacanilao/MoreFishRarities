using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace MoreFishRarities;

internal static class ThingPatch
{
    internal static IEnumerable<CodeInstruction> GetNameTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> instructionList = new(collection: instructions);

        MethodInfo? isEquipmentOrRangedOrAmmoGetter = AccessTools.PropertyGetter(
            type: typeof(Card),
            name: nameof(Card.IsEquipmentOrRangedOrAmmo));
        MethodInfo? shouldUseEquipmentStyleRareNaming = AccessTools.Method(
            type: typeof(ThingPatch),
            name: nameof(ShouldUseEquipmentStyleRareNaming),
            parameters: new[] { typeof(Thing) });

        if (isEquipmentOrRangedOrAmmoGetter == null || shouldUseEquipmentStyleRareNaming == null)
        {
            MoreFishRarities.LogError(message: "Thing.GetName transpiler: required naming methods were not found");
            return instructionList;
        }

        int getterMatchCount = CountMethodCalls(
            instructions: instructionList,
            method: isEquipmentOrRangedOrAmmoGetter);
        MoreFishRarities.LogDebug(message: $"Thing.GetName transpiler: found {getterMatchCount} IsEquipmentOrRangedOrAmmo getter call(s)");

        if (getterMatchCount != 1)
        {
            MoreFishRarities.LogError(message: "Thing.GetName transpiler: expected exactly one IsEquipmentOrRangedOrAmmo getter call");
            return instructionList;
        }

        CodeMatcher codeMatcher = new(instructions: instructionList);
        codeMatcher.MatchStartForward(matches: new[]
        {
            new CodeMatch(predicate: instruction => CallsMethod(instruction: instruction, method: isEquipmentOrRangedOrAmmoGetter))
        });

        if (codeMatcher.IsValid == false)
        {
            MoreFishRarities.LogError(message: "Thing.GetName transpiler: IsEquipmentOrRangedOrAmmo getter call was not matched");
            return instructionList;
        }

        MoreFishRarities.LogDebug(message: "Thing.GetName transpiler: widening equipment-style rare naming for fish");
        codeMatcher.SetInstruction(
            instruction: new CodeInstruction(opcode: OpCodes.Call, operand: shouldUseEquipmentStyleRareNaming)
                .WithLabels(labels: codeMatcher.Instruction.labels)
                .WithBlocks(blocks: codeMatcher.Instruction.blocks));

        return codeMatcher.Instructions();
    }

    internal static bool ShouldUseEquipmentStyleRareNaming(Thing thing)
    {
        if (thing.IsEquipmentOrRangedOrAmmo)
        {
            return true;
        }

        return thing.IsIdentified &&
               thing.rarity >= Rarity.Legendary &&
               thing.source?._origin == "fish";
    }

    private static int CountMethodCalls(List<CodeInstruction> instructions, MethodInfo method)
    {
        int count = 0;

        foreach (CodeInstruction instruction in instructions)
        {
            if (CallsMethod(instruction: instruction, method: method))
            {
                count++;
            }
        }

        return count;
    }

    private static bool CallsMethod(CodeInstruction instruction, MethodInfo method)
    {
        return (instruction.opcode == OpCodes.Call || instruction.opcode == OpCodes.Callvirt) &&
               Equals(objA: instruction.operand, objB: method);
    }
}
