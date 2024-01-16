using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FractalTree
{
	public class Branch : IBranch
	{
		public const float LengthMultiplayer = 0.67f;
		public Vector2 Direction { get; set; }
		public int LeafCount { get; set; }
		public virtual Vector2 StartPos { get; private set; }
		public virtual Vector2 EndPos { get; private set; }

		public virtual void Setup (Vector2 start, Vector2 end)
		{
			StartPos = start;
			EndPos = end;

			Direction = end - start;
			LeafCount = 0;
		}
		
		public void Reset()
		{
			Direction = EndPos - StartPos;
			LeafCount = 0;
		}
	}
}