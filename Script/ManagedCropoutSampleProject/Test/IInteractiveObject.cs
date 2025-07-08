using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnrealSharp.Attributes;
using UnrealSharp.CoreUObject;
using UnrealSharp.Engine;
using UnrealSharp.UMGEditor;

namespace ManagedCropoutSampleProject.Test;

[UInterface]
public interface IInteractiveObject : IInterface
{

	bool IsInteracting { get; }

	FVector GetHintLocation();

	void SetHintWidgetBlueprint(UWidgetBlueprint widgetBlueprint);

	// [UFunction( FunctionFlags.BlueprintEvent )]
	bool AllowInteract(AActor actor);

	[UFunction(FunctionFlags.BlueprintEvent)]
	void Interact(AActor actor);

}
