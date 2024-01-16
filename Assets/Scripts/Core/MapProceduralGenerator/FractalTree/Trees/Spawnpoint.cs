using System.Collections;
using System.Collections.Generic;
using Core.Factory.Gizmo;
using UnityEngine;

namespace FractalTree
{
	public class Spawnpoint : ICircleGizmoDrawable
	{
		private readonly IGizmoDrawerFactory _gizmoDrawerFactory;
		public Vector2 CircleDrawablePosition { get; private set; }
		public bool HasBeenReached { get; set; }

		public Vector2 Position { get; }
		
		public Spawnpoint(IGizmoDrawerFactory gizmoDrawerFactory, Vector2 position)
		{
			_gizmoDrawerFactory = gizmoDrawerFactory;
			Position = position;
			CircleDrawablePosition = position;
			
			_gizmoDrawerFactory.CreateDrawer(this);
		}

		
	}
}