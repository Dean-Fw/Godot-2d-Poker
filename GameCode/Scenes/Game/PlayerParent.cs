using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class PlayerParent : Control
{
	public List<Player> GetPlayers() {
		return GetChildren()
			.OfType<Player>()
			.ToList();
	}
}
