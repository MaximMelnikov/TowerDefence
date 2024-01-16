using System.Collections;
using System.Collections.Generic;
using Core.Factory.Gizmo;
using UnityEngine;

namespace FractalTree
{
	public class FractalTreeBuilder
	{
		private readonly IGizmoDrawerFactory _gizmoDrawerFactory;
		public float colonizationInitialLength = 0.1f;
		public float colonizationMinDistance = 0.01f;
		public float colonizationMaxDistance = .1f;
		public List<IBranch> Branches { get; private set; }

		public FractalTreeBuilder(IGizmoDrawerFactory gizmoDrawerFactory)
		{
			_gizmoDrawerFactory = gizmoDrawerFactory;
		}

		public void Build (Vector2 spawnpointsCenterpoint, float spawnRadius, int spawnCount)
		{
			List<Spawnpoint> spawnpoints = SpawnpointsGenerator(spawnpointsCenterpoint, spawnRadius, spawnCount);
			
			ITree tree = CreateTree(spawnpoints);
			Branches = CreateBranches(tree);
		}
		
		private List<IBranch> CreateBranches (ITree tree)
		{
			return tree.Generate();;
		}

		private ITree CreateTree(List<Spawnpoint> spawnpoints)
		{
			Vector2 treeRootPosition = Vector2.zero;
			
			ITree tree = new ColonizationTree (
				spawnpoints,
				treeRootPosition,
				colonizationInitialLength, 
				colonizationMinDistance, 
				colonizationMaxDistance);

			return tree;
		}
		
		private List<Spawnpoint> SpawnpointsGenerator(Vector2 spawnpointsCenterpoint, float radius, int count)
		{
			List<Spawnpoint> spawnpoints = new List<Spawnpoint>(count);
			
			for (int i = 0; i < count; i++) {
				spawnpoints.Add(new Spawnpoint(_gizmoDrawerFactory, spawnpointsCenterpoint + (Random.insideUnitCircle * radius)));
			}

			return spawnpoints;
		}
	}

    
}