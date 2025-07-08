using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnrealSharp.Attributes;
using UnrealSharp.CoreUObject;
using UnrealSharp.Engine;
using UnrealSharp.UMG;
using UnrealSharp.UMGEditor;

namespace ManagedCropoutSampleProject.Test;

[UClass]
public class UInteractiveComponent : UPlayerSensorComponent, IInteractiveObject
{

	[UProperty(PropertyFlags.EditAnywhere | PropertyFlags.BlueprintReadOnly, Category = "Interactive")]
	public float InteractionRange { get; set; } // 130f

	[UProperty(PropertyFlags.EditAnywhere | PropertyFlags.BlueprintReadWrite, Category = "Interactive")]
	public string InteractionTag { get; set; }

	public bool IsInteracting => false;

	public AActor? PlayerInRange => _player;

	private AActor? _player;
	private UWidgetBlueprint _wbInteractiveHintWidgetBlueprint;
	private UUserWidget _userWidget;
	private UWidgetComponent? _widgetComponent;
	private UImage? _WImageHintKey;
	//internal ICoroutine? _coroutineInteract;

	public override void BeginPlay()
	{
		base.BeginPlay();

	}

	public override void EndPlay(EEndPlayReason EndPlayReason)
	{
		base.EndPlay(EndPlayReason);

		//G.Publish(new InteractionTargetOutOfRangeCommand(_player, this));

		// LogGame.Log( $"[UInteractiveComponent] EndPlay: {EndPlayReason}" );
	}

	protected override void OnPlayerEnter(AActor Player)
	{
		Activate();

		_player = Player;
		// LogGame.Log( $"Player: {Player.ObjectName}" );
		//G.Publish(new InteractionTargetInRangeCommand(Player, this));
	}

	protected override void OnPlayerExit(AActor Player)
	{
		if (_widgetComponent != null)
		{
			_widgetComponent.DetachFromComponent();
			// _widgetComponent.MarkAsGarbage();
			_widgetComponent.DestroyComponent(_widgetComponent);
		}

		_player = null;

		//G.Publish(new InteractionTargetOutOfRangeCommand(Player, this));

		Deactivate();
	}

	public override void Tick(float deltaSeconds)
	{
		base.Tick(deltaSeconds);

		LogCropout.Log($"InteractionRange: {InteractionRange}");
	}

	/// <summary>
	/// 重写此方法以支持本地化字符串
	/// </summary>
	public virtual string GetInteractionTag() => InteractionTag.ToString();

	public FVector GetHintLocation()
	{
		// if ( Owner.Com< UBoxComponent >() is {} boxComponent ) {
		// 	return boxComponent.WorldLocation;
		// }

		return Owner.ActorLocation;
	}

	public AActor? GetInteractionTarget() => _player;

	public void SetHintWidgetBlueprint(UWidgetBlueprint widgetBlueprint)
	{
		_wbInteractiveHintWidgetBlueprint = widgetBlueprint;

		//_widgetComponent = Owner.AddComponentByClass<UWidgetComponent>(true, FTransform.Identity);
		//_widgetComponent.AttachTo(Owner.RootComponent);
		//_widgetComponent.Widget = CreateWidget(widgetBlueprint.GeneratedClass.Cast<UUserWidget>());
		//_widgetComponent.WidgetSpace = EWidgetSpace.Screen;
		//_widgetComponent.DrawAtDesiredSize = true;
		//_widgetComponent.Pivot = new FVector2D(0.5, 1.0);
		//_userWidget = _widgetComponent.Widget;
		//_WImageHintKey = _userWidget.GetWidgetFromName<UImage>("Img_HintKey");

		if (_WImageHintKey != null)
		{
			_WImageHintKey.Visibility = ESlateVisibility.Hidden;
		}

		// _userWidget = CreateWidget( widgetBlueprint.GeneratedClass.Cast< UUserWidget >() );
	}

	public virtual bool AllowInteract(AActor actor) => IsActive() && InInteractiveRange();

	public virtual void Interact(AActor actor) { }

	protected bool InInteractiveRange()
	{
		if (_player != null)
		{
			var distance = FVector.Distance(_player.ActorLocation, Owner.ActorLocation);
			// LogGame.Log( $"dis: {distance}" );
			// return distance < 130;
			return distance < InteractionRange;
			// if ( float.TryParse( InteractRangeStr, out var range ) ) {
			// 	return distance < range;
			// }
		}

		return false;
	}

	protected bool InPlayerView()
	{
		//if (G.PlayerController?.ControlledPawn is { } playerPawn)
		//{
		//	var playerLocation = playerPawn.ActorLocation;
		//	var objectLocation = Owner.ActorLocation;

		//	// Calculate direction from player to object
		//	FVector toObjectDirection = Vector3.Normalize(objectLocation - playerLocation);
		//	var playerForward = playerPawn.ActorForwardVector;

		//	// Calculate dot product
		//	var dotProduct = FVector.Dot(toObjectDirection, playerForward);

		//	// Convert field of view angle to cosine value
		//	var fieldOfViewAngle = 45.0f; // 45度视野角度
		//	var maxDot = MathLibrary.Cos(MathLibrary.DegreesToRadians(fieldOfViewAngle));

		//	// Check if object is within view angle
		//	return dotProduct >= maxDot;
		//}

		return false;
	}

}

