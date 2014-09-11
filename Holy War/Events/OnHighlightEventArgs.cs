using System;
using Holy_War.Actors.Stats;

namespace Holy_War.Events
{
	public class OnHighlightEventArgs : EventArgs
	{
		public ActorStats ActorStats { get; private set; }

		public OnHighlightEventArgs(ActorStats actorStats)
		{
			ActorStats = actorStats;
		}
	}
}
