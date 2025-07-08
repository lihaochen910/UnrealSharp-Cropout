using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnrealSharp.Attributes;
using UnrealSharp.Engine;

namespace ManagedCropoutSampleProject.Test;

[UClass( ClassFlags.Abstract )]
public class USensorComponent : UActorComponent
{

	public override void BeginPlay()
	{
		base.BeginPlay();

		Owner.OnActorBeginOverlap.Add(OnActorBeginOverlap);
		Owner.OnActorEndOverlap.Add(OnActorEndOverlap);
	}

	public override void EndPlay(EEndPlayReason EndPlayReason)
	{
		base.EndPlay(EndPlayReason);

		Owner.OnActorBeginOverlap.Remove(OnActorBeginOverlap);
		Owner.OnActorEndOverlap.Remove(OnActorEndOverlap);
	}

	[UFunction(FunctionFlags.BlueprintEvent)]
	protected virtual void OnActorBeginOverlap(AActor OverlappedActor, AActor OtherActor) { }

	// private void OnActorBeginOverlap_Implementation( AActor OverlappedActor, AActor OtherActor ) {}

	[UFunction(FunctionFlags.BlueprintEvent)]
	protected virtual void OnActorEndOverlap(AActor OverlappedActor, AActor OtherActor) { }

	// private void OnActorEndOverlap_Implementation( AActor OverlappedActor, AActor OtherActor ) {}

}
