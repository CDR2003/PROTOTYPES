using UnityEngine;
using System.Collections;
using UniRx;

public class Mover : MonoBehaviour
{
	public float speed = 2.0f;

	public ReactiveProperty<Vector3> direction { get; private set; }

	public ReadOnlyReactiveProperty<Vector3> velocity { get; private set; }

	void Awake()
	{
		this.direction = new ReactiveProperty<Vector3>( Vector3.up );
		this.velocity = new ReadOnlyReactiveProperty<Vector3>( this.direction.Select( dir => dir * this.speed ) );

		Observable.EveryUpdate().Subscribe( _ => this.transform.Translate( this.velocity.Value * Time.deltaTime ) );
	}
}
