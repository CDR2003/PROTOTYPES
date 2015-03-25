using UnityEngine;
using System.Collections;

namespace FdmGames
{
	public static class Geometry2D
	{
		public static Vector2? RayIntersectsLineSegment( Vector2 origin, Vector2 direction, Vector2 segmentStart, Vector2 segmentEnd )
		{
			// Algorithm copied from http://stackoverflow.com/questions/14307158/how-do-you-check-for-intersection-between-a-line-segment-and-a-line-ray-emanatin
			var p = origin;
			var r = direction;
			var q1 = segmentStart;
			var q2 = segmentEnd;
			var s = q2 - q1;
			var q = q1;
			var t = ( p.x * s.y - p.y * s.x - q.x * s.y + q.y * s.x ) / ( r.y * s.x - r.x * s.y );
			var u = ( p.x * r.y - p.y * r.x - q.x * r.y + q.y * r.x ) / ( r.y * s.x - r.x * s.y );

			if( t > 0.0f && u >= 0.0f && u <= 1.0f )
			{
				var sx = ( 1.0f - u ) * q1.x + u * q2.x;
				var sy = ( 1.0f - u ) * q1.y + u * q2.y;
				return new Vector2( sx, sy );
			}

			return null;
		}
	}
}