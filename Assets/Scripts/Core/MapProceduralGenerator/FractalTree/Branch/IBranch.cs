using UnityEngine;

namespace FractalTree
{
	public interface IBranch
	{
		public Vector2 StartPos { get; }
		public Vector2 EndPos { get; }
		public Vector2 Direction { get; set; }
		public int LeafCount { get; set; }
		public void Setup (Vector2 start, Vector2 end);
		public void Reset ();
	}
}