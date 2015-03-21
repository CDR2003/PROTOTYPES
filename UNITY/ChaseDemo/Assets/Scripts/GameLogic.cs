using UnityEngine;
using System.Collections.Generic;
using UniRx;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
	public Mover player;

	public Mover mob;

	public Text distanceText;

	public float initialTime = 2.0f;

	private ReadOnlyReactiveProperty<float> _distance;

	void Start()
	{
		// Distance display
		_distance = new ReadOnlyReactiveProperty<float>( Observable.EveryUpdate().Select( _ =>
			Vector3.Distance( player.transform.position, mob.transform.position ) ) );
		_distance.SubscribeToText( this.distanceText );

		// Mob chase player every update
		Observable.EveryUpdate().Subscribe( _ =>
			mob.direction.Value = ( player.transform.position - mob.transform.position ).normalized );

		// Time sequence of 1/1, 1/2, 1/3, 1/4...
		var fracTimeSequence = Observable.Create<float>( observer =>
		{
			var i = 1;
			return Scheduler.DefaultSchedulers.TimeBasedOperations.Schedule( System.TimeSpan.FromSeconds( this.initialTime / i ), self =>
			{
				observer.OnNext( i % 2 == 0 ? 1.0f : -1.0f );
				i++;
				self( System.TimeSpan.FromSeconds( this.initialTime / i ) );
			} );
		} );

		fracTimeSequence.Subscribe( d => this.player.direction.Value = Vector3.Cross( Vector3.back, this.mob.direction.Value ).normalized * d );
	}
}
