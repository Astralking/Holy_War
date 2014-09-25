using System;
using Holy_War.Helpers;
using Microsoft.Xna.Framework;

namespace Holy_War.Events
{
	public class OnDamageEventArgs : EventArgs
	{
		public Vector2 GridLocationInPixels { get; private set; }
		public int Damage { get; private set; }

		public OnDamageEventArgs(Point gridLocation, int damage)
		{
            GridLocationInPixels = Converter.GetLocationInPixels(gridLocation);
			Damage = damage;
		}
	}
}
