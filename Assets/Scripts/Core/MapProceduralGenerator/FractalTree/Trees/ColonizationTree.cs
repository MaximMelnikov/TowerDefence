using System.Collections.Generic;
using UnityEngine;

namespace FractalTree
{
	/// <summary>
	/// Creates a fractal tree using space colonization algorithm: http://algorithmicbotany.org/papers/colonization.egwnp2007.pdf
	/// </summary>
	public class ColonizationTree : ITree
	{
		private const int TRUNK_MAX_ATTEMPTS = 10;
		private const int GROWTH_MAX_ATTEMPTS = 10;

		private readonly List<Spawnpoint> _leaves;
		private readonly Vector2 _rootPosition;
		private readonly float _initialLength;
		private readonly float _minDistance;
		private readonly float _maxDistance;

		public ColonizationTree (
			List<Spawnpoint> leaves,
			Vector2 rootPosition,
			float initialLength,
			float minDistance,
			float maxDistance)
		{
			_leaves = leaves;
			_rootPosition = rootPosition;
			_initialLength = initialLength;
			_minDistance = minDistance;
			_maxDistance = maxDistance;
		}

		public List<IBranch> Generate ()
		{
			var branches = new List<IBranch> ();

			IBranch root = CreateRoot();
			branches.Add (root);

			branches.AddRange (CreateTrunk (root));
		
			int growthAttempts = 0;

			while (growthAttempts++ <= GROWTH_MAX_ATTEMPTS) {
				bool stillGrowing = Grow (ref branches);

				if (!stillGrowing) {
					break;
				}
			}

			return branches;
		}

		private IBranch CreateRoot()
		{
			Vector2 end = _rootPosition + (Vector2.up * _initialLength);

			return CreateBranch (_rootPosition, end);
		}

		private IBranch CreateBranch (IBranch previous, Vector2 end)
		{
			var branch = new Branch();
			branch.Setup (previous.EndPos, end);
			return branch;
		}

		private IBranch CreateBranch (Vector2 start, Vector2 end)
		{
			var branch = new Branch();
			branch.Setup (start, end);
			return branch;
		}

		private List<IBranch> CreateTrunk (IBranch root)
		{
			var branches = new List<IBranch> ();

			IBranch current = root;

			bool found = false;

			int attempts = 0;

			while (!found && (attempts++ < TRUNK_MAX_ATTEMPTS)) {
				foreach (var leaf in _leaves) {
					var dist = Vector2.Distance (current.EndPos, leaf.Position);

					if (dist >= _minDistance && dist <= _maxDistance) {
						found = true;
					}
				}

				if (!found) {
					var dir = (current.EndPos - current.StartPos).normalized;

					current = CreateBranch (current, current.EndPos + (dir * _initialLength));

					branches.Add (current);
				}
			}

			return branches;
		}

		private bool Grow (ref List<IBranch> branches)
		{
			bool growing = false;

			if (_leaves.Count == 0) {
				return growing;
			}
	
			foreach (var leaf in _leaves) {

				IBranch closest = default(IBranch);
				float closestDist = float.MaxValue;

				foreach (var branch in branches) {
					var dist = Vector2.Distance (leaf.Position, branch.EndPos);

					if (dist < _minDistance) { // too close
						leaf.HasBeenReached = true;
						closest = default(IBranch);
						break;
					}

					if (dist <= _maxDistance && dist < closestDist) {
						closest = branch;
						closestDist = dist;
					}
				}

				if (closest != null) {

					var dir = (leaf.Position - closest.EndPos).normalized;

					closest.Direction += dir;

					closest.LeafCount++;
				}
			}


			for (int i = _leaves.Count - 1; i >= 0; i--) {
				if (_leaves [i].HasBeenReached) {
					//GameObject.Destroy (_leaves [i].gameObject);
					_leaves.RemoveAt (i);
				}
			}

			for (int i = branches.Count - 1; i >= 0; i--) {

				var branch = branches [i];

				if (branch.LeafCount > 0) {
					branch.Direction /= (branch.LeafCount + 1);

					branches.Add (CreateBranch (branch, 
						branch.EndPos + branch.Direction));

					growing = true;

				}

				branch.Reset ();
			}

			return growing;

		}
	}
}