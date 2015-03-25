using UnityEngine;
using System.Collections;

namespace FdmGames
{
	public class OutScreenIndicator : MonoBehaviour
	{
		public Transform target;

		internal PolygonCollider2D wall;

		internal void Update()
		{
			var relativePos = Camera.main.transform.InverseTransformPoint( this.target.position );
			relativePos.z = 0.0f;
			this.transform.localRotation = Quaternion.FromToRotation( Vector3.right, relativePos );

			if( this.wall != null )
			{
				this.UpdatePosition( relativePos.normalized );
			}
		}

		private void UpdatePosition( Vector2 direction )
		{
			var intersection = this.CalculateWallIntersection( direction );
			if( intersection.HasValue )
			{
				this.transform.localPosition = intersection.Value;
			}
		}

		private Vector2? CalculateWallIntersection( Vector2 direction )
		{
			for( int i = 0; i < this.wall.pathCount; i++ )
			{
				var path = this.wall.GetPath( i );
				for( int j = 0; j < path.Length; j++ )
				{
					var startPoint = path[j];
					var endPoint = path[( j + 1 ) % path.Length];
					var ray = new Ray2D( Vector2.zero, direction );
					var intersection = Geometry2D.RayIntersectsLineSegment( ray, startPoint, endPoint );
					if( intersection.HasValue )
					{
						return intersection.Value;
					}
				}
			}

			return null;
		}
	}
}