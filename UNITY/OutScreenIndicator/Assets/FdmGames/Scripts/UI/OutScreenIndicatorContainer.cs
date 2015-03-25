using UnityEngine;
using System.Collections.Generic;

namespace FdmGames
{
	[RequireComponent( typeof( PolygonCollider2D ) )]
	public class OutScreenIndicatorContainer : MonoBehaviour
	{
		private List<OutScreenIndicator> _indicators = new List<OutScreenIndicator>();
		
		public void AddIndicator( OutScreenIndicator indicator )
		{
			_indicators.Add( indicator );
			indicator.transform.SetParent( this.transform, false );
			indicator.wall = this.GetComponent<PolygonCollider2D>();
		}

		public bool RemoveIndicator( OutScreenIndicator indicator, bool destroy )
		{
			if( destroy )
			{
				Object.Destroy( indicator.gameObject );
			}
			return _indicators.Remove( indicator );
		}

		public bool RemoveIndicator( Transform target, bool destroy )
		{
			var indicator = _indicators.Find( i => i.target == target );
			if( indicator == null )
			{
				return false;
			}

			return this.RemoveIndicator( indicator, destroy );
		}

		void Update()
		{
			foreach( var indicator in _indicators )
			{
				var frustum = GeometryUtility.CalculateFrustumPlanes( Camera.main );
				var visible = GeometryUtility.TestPlanesAABB( frustum, indicator.target.collider.bounds );
				var active = !visible;
				if( indicator.gameObject.activeSelf == false && active )
				{
					indicator.Update();
				}

				indicator.gameObject.SetActive( !visible );
			}
		}
	}
}