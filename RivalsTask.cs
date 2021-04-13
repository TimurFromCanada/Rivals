using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rivals
{
	public class RivalsTask
	{
		public static IEnumerable<OwnedLocation> AssignOwners(Map map)
		{
			var visited = new HashSet<Point>();
			var queue = new Queue<Tuple<int, Point, int>>();

			for (var i = 0; i < map.Players.Length; i++)
			{
				queue.Enqueue(Tuple.Create(i, new Point(map.Players[i].X, map.Players[i].Y), 0));
			}

			while (queue.Count != 0)
			{
				var previous = queue.Dequeue();
				var point = previous.Item2;

				if (point.X < 0
					|| point.X >= map.Maze.GetLength(0)
					|| point.Y < 0 || point.Y >= map.Maze.GetLength(1)
					|| map.Maze[point.X, point.Y] == MapCell.Wall)
				{
					continue;
				}

				if (visited.Contains(point))
					continue;

				visited.Add(point);
				yield return new OwnedLocation(previous.Item1, new Point(point.X, point.Y), previous.Item3);

				for (var dy = -1; dy <= 1; dy++)
					for (var dx = -1; dx <= 1; dx++)
					{
						if (dx != 0 && dy != 0)
							continue;
						else
						{
							queue.Enqueue(Tuple.Create(previous.Item1, new Point { X = point.X + dx, Y = point.Y + dy }, previous.Item3 + 1));
						}
					}
			}
		}
	}
}


