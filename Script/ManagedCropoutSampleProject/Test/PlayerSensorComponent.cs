using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnrealSharp.Attributes;
using UnrealSharp.Engine;

namespace ManagedCropoutSampleProject.Test;

[UClass]
public class UPlayerSensorComponent : USensorComponent
{

	private UBoxComponent _boxComponent;
	public override void BeginPlay()
	{
		base.BeginPlay();

		_boxComponent = Owner.GetComponentByClass<UBoxComponent>();
	}

	protected override void OnActorBeginOverlap(AActor OverlappedActor, AActor OtherActor)
	{
		if (OtherActor != null)
		{
			OnPlayerEnter(OtherActor);
		}
	}

	protected override void OnActorEndOverlap(AActor OverlappedActor, AActor OtherActor)
	{
		if (OtherActor != null)
		{
			OnPlayerExit(OtherActor);
		}
	}

	[UFunction(FunctionFlags.BlueprintEvent)]
	protected virtual void OnPlayerEnter(AActor Player) { }

	// private void OnPlayerEnter_Implementation( AActor OverlappedActor, AActor OtherActor ) {}

	[UFunction(FunctionFlags.BlueprintEvent)]
	protected virtual void OnPlayerExit(AActor Player) { }

	// private void OnPlayerExit_Implementation( AActor OverlappedActor, AActor OtherActor ) {}

}
