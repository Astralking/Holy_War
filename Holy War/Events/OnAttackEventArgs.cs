using System;
using Microsoft.Xna.Framework;

namespace Holy_War.Events
{
	public class OnAttackEventArgs : EventArgs
	{
		public Point GridLocation { get; private set; }
		public int Damage { get; private set; }

		public OnAttackEventArgs(Point gridLocation, int damage)
		{
			GridLocation = gridLocation;
			Damage = damage;
		}
	}
}
