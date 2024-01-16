using System;
using System.Collections.Generic;
using Core.Factory.Gizmo;
using FractalTree;
using UnityEngine;

namespace Core.Bootstrap.MapProceduralGenerator
{
    public class MapRoad : IDisposable, IGraphGizmoDrawable
    {
        private readonly IGizmoDrawerFactory _gizmoDrawerFactory;
        private readonly int _spawnpointsCount;
        private int _segment;
        private IGraphGizmoDrawable _graphGizmoDrawableImplementation;
        private Ellipse _destination { get; }
        private List<IBranch> _branches;
        
        public (Vector2, Vector2)[] Edges { get; private set; }
        
        public MapRoad(IGizmoDrawerFactory gizmoDrawerFactory, int segment, Ellipse destinationEllipse, int spawnpointsCount)
        {
            _gizmoDrawerFactory = gizmoDrawerFactory;
            _spawnpointsCount = spawnpointsCount;
            _segment = segment;
            _destination = destinationEllipse;
            CreateRoadByFractalTree();
            CreateGraphGizmo();
        }

        private void CreateRoadByFractalTree()
        {
            FractalTreeBuilder fractalTreeBuilder = new FractalTreeBuilder(_gizmoDrawerFactory);
            
            fractalTreeBuilder.Build(
                _destination.Points[_segment],
                0.7f,
                _spawnpointsCount);
            
            _branches = fractalTreeBuilder.Branches;
        }

        private void CreateGraphGizmo()
        {
            Edges = new (Vector2, Vector2)[_branches.Count];
            for (int i = 0; i < _branches.Count; i++)
            {
                Edges[i] = new ValueTuple<Vector2, Vector2>(_branches[i].StartPos, _branches[i].EndPos);
            }
            
            _gizmoDrawerFactory.CreateDrawer(this);
        }

        public void Dispose()
        {
            _gizmoDrawerFactory.RemoveDrawer(this);
        }


        
    }
}