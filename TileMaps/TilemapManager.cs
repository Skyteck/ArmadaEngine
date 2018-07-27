using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmadaEngine.TileMaps
{

    public class TilemapManager
    {
        List<TileMap> mapList = new List<TileMap>();
        public TileMap ActiveMap;

        public void AddMap(TileMap newMap)
        {
            mapList.Add(newMap);
        }

        public TileMap findMap(Vector2 pos)
        {
            foreach (TileMap map in mapList)
            {
                if (map.mapTilePos.X == pos.X && map.mapTilePos.Y == pos.Y)
                {
                    return map;
                }
            }
            return null;
        }

        public TileMap findMapByName(String name)
        {
            foreach (TileMap map in mapList)
            {
                if (map.name.Equals(name))
                {
                    return map;
                }
            }
            return null;
        }

        public Tile findTile(Vector2 pos)
        {
            Vector2 posToTileMapPos = PosToWorldTilePos(pos);
            TileMap mapClicked = findMap(PosToWorldTilePos(pos));
            if(mapClicked == null)
            {
                return null;
            }
            Vector2 localTileMapPos = new Vector2(pos.X - (posToTileMapPos.X * mapClicked.mapWidth), pos.Y - (posToTileMapPos.Y * mapClicked.mapHeight));
            if (mapClicked != null)
            {
                Tile clickedTile = mapClicked.findClickedTile(PosToMapPos(localTileMapPos, mapClicked.tileWidth));
                return clickedTile;
            }
            return null;
        }

        public List<Tile> FindAdjacentTiles(Vector2 pos, bool allowDiagonal = true)
        {
            Tile targetTile = findTile(pos);
            List<Tile> adjacentTiles = new List<Tile>();
            int tileWidth = targetTile.destRect.Width;
            //get top center tile
            Tile topCenter = findTile(new Vector2(targetTile.tileCenter.X, targetTile.tileCenter.Y - tileWidth));
            if (topCenter != null && topCenter.walkable)
            {
                adjacentTiles.Add(topCenter);
            }
            Tile LeftTile = findTile(new Vector2(targetTile.tileCenter.X - tileWidth, targetTile.tileCenter.Y));
            if (LeftTile != null && LeftTile.walkable)
            {
                adjacentTiles.Add(LeftTile);
            }
            Tile rightTIle = findTile(new Vector2(targetTile.tileCenter.X + tileWidth, targetTile.tileCenter.Y));
            if (rightTIle != null && rightTIle.walkable)
            {
                adjacentTiles.Add(rightTIle);
            }
            Tile bottomCenter = findTile(new Vector2(targetTile.tileCenter.X, targetTile.tileCenter.Y + tileWidth));
            if (bottomCenter != null && bottomCenter.walkable)
            {
                adjacentTiles.Add(bottomCenter);
            }

            //adjacentTiles.Add(targetTile);

            if (allowDiagonal)
            {
                Tile topleft = findTile(new Vector2(targetTile.tileCenter.X - tileWidth, targetTile.tileCenter.Y - tileWidth));
                if (topleft.walkable)
                {
                    adjacentTiles.Add(topleft);
                }
                Tile topRight = findTile(new Vector2(targetTile.tileCenter.X + tileWidth, targetTile.tileCenter.Y - tileWidth));
                if (topRight.walkable)
                {
                    adjacentTiles.Add(topRight);
                }
                Tile bottomLeft = findTile(new Vector2(targetTile.tileCenter.X - tileWidth, targetTile.tileCenter.Y + tileWidth));
                if (bottomLeft.walkable)
                {
                    adjacentTiles.Add(bottomLeft);
                }
                Tile bottomRight = findTile(new Vector2(targetTile.tileCenter.X + tileWidth, targetTile.tileCenter.Y + tileWidth));
                if (bottomRight.walkable)
                {
                    adjacentTiles.Add(bottomRight);
                }
            }

            return adjacentTiles;
        }
        


        public List<Tile> AStarTwo(Tile tileOne, Tile tileTwo, bool useDiagonal = false)
        {
            if (tileOne == tileTwo) return null;

            Dictionary<Tile, float> ClosedNodes = new Dictionary<Tile, float>();
            Dictionary<Tile, float> OpenNodes = new Dictionary<Tile, float>();
            Dictionary<Tile, Tile> Path = new Dictionary<Tile, Tile>();
            Dictionary<Tile, float> distanceFromPrevious = new Dictionary<Tile, float>();
            distanceFromPrevious.Add(tileOne, 0);
            Dictionary<Tile, float> distanceFromStart = new Dictionary<Tile, float>();
            float startCost = Vector2.Distance(tileOne.tileCenter, tileTwo.tileCenter);
            startCost = calcCost(tileOne, tileTwo);
            distanceFromStart.Add(tileOne, startCost);

            OpenNodes.Add(tileOne, startCost);

            while(OpenNodes.Count > 0)
            {
                Tile current = OpenNodes.OrderBy(x => x.Value).First().Key;

                if(current == tileTwo)
                {
                    //done
                    return BuildPath(Path, current);
                }

                OpenNodes.Remove(current);
                float closestFScore = Vector2.Distance(current.tileCenter, tileOne.tileCenter);
                closestFScore = calcCost(current, tileOne);
                ClosedNodes.Add(current, closestFScore);

                List<Tile> adjacents = this.FindAdjacentTiles(current.tileCenter, useDiagonal);

                foreach(Tile t in adjacents)
                {
                    if(ClosedNodes.ContainsKey(t))
                    {
                        continue;
                    }


                    float idk = distanceFromPrevious[current] + Vector2.Distance(t.tileCenter, current.tileCenter);
                    t.myColor = Color.Blue;

                    if(!OpenNodes.ContainsKey(t))
                    {
                        OpenNodes.Add(t, Vector2.Distance(t.tileCenter, tileTwo.tileCenter));
                    }
                    else if(idk >= distanceFromPrevious[t])
                    {
                        continue;
                    }

                    
                    Path[t] = current;
                    
                    distanceFromPrevious[t] = idk;
                    //distanceFromStart[t] = distanceFromPrevious[t] + Vector2.Distance(t.tileCenter, tileTwo.tileCenter);
                    distanceFromStart[t] = distanceFromPrevious[t] + calcCost(t, tileOne);

                    
                }

            }
            return null;
        }

        public void ResetTileColors()
        {
            foreach(TileMap t in mapList)
            {
                t.ResetColors();
            }
        }

        private float calcCost(Tile one, Tile two)
        {
            float dx = Math.Abs(one.tileCenter.X - two.tileCenter.X);
            float dy = Math.Abs(one.tileCenter.Y - two.tileCenter.Y);
            float cost = one.destRect.Width;
            float diagCost = (cost * cost) + (cost * cost);
            return cost * Math.Max(dx, dy) + (diagCost - cost) * Math.Min(dx, dy);

        }

        public List<Tile> BuildPath(Dictionary<Tile, Tile> Path, Tile current)
        {
            if (!Path.ContainsKey(current))
            {
                return new List<Tile> { current };
            }

            var path = BuildPath(Path, Path[current]);
            path.Add(current);
            return path;
        }

        public List<Tile> FindPath(Tile tileOne, Tile TileTwo)
        {
            List<Tile> pathTiles = new List<Tile>();
            while(tileOne != TileTwo)
            {
                pathTiles.Add(tileOne);
                List<Tile> adjacentTiles = this.FindAdjacentTiles(tileOne.tileCenter, false);
                Tile closest = FindClosestTile(adjacentTiles, TileTwo);
                if(tileOne == closest)
                {
                    return pathTiles;
                }
                tileOne = closest;
                if(tileOne == TileTwo)
                {
                    pathTiles.Add(TileTwo);
                }
            }

            //get tiles adjacent to tileOne
            // loop through adjacent tiles to find closest and return it
            // add it to pathTIles
            // repeat using new tile
            

            return pathTiles;
        }
        public Tile FindClosestTile(List<Tile> list, Tile target)
        {
            float distance = 99999999;
            Tile closest = list.First();
            float newDistance = Vector2.Distance(closest.tileCenter, target.tileCenter);

            foreach (Tile tile in list)
            {
                newDistance = Vector2.Distance(tile.tileCenter, target.tileCenter);
                if (newDistance < distance)
                {
                    distance = newDistance;
                    closest = tile;
                }
            }

            return closest;
        }

        public List<Tile> getAllTiles(TileMap map)
        {
            return map.backgroundTiles;
        }

        public Vector2 PosToWorldTilePos(Vector2 pos)
        {

            int clickMapX = (int)pos.X / 1600;
            int clickMapY = (int)pos.Y / 1600;
            return new Vector2(clickMapX, clickMapY);
        }

        private Vector2 PosToMapPos(Vector2 pos, int TileSize)
        {
            //need to change this to the coordinates within the tilemap itself...
            int clickMapX = (int)pos.X / TileSize;
            int clickMapY = (int)pos.Y / TileSize;
            return new Vector2(clickMapX, clickMapY);
        }

        internal Tile findWalkableTile(Vector2 newPos)
        {
            Tile newTile = findTile(newPos);
            if (newTile.walkable)
            {
                return newTile;
            }
            return null;
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle vp)
        {
            foreach (TileMap map in mapList)
            {
                map.Draw(spriteBatch, vp);
            }
        }

        public void LoadMap(String mapname, ContentManager Content)
        {
            TileMap testMap = new TileMap(mapname, Content);
            mapList.Add(testMap);
            testMap.active = true;

        }
    }

    public class Node
    {
        public Tile myTile;
        public Tile _one;
        public Tile _two;
        public float toStartCost;
        public float toGoalCost;
        public Tile closestTile;
        public bool open;
        public List<Node> neighbors = new List<Node>();
        public Node(Tile myTile, Tile two, Tile one)
        {
            this.myTile = myTile;
            toGoalCost = Vector2.Distance(myTile.tileCenter, two.tileCenter);
            toStartCost = Vector2.Distance(myTile.tileCenter, one.tileCenter);
            
        }

        public void SetNeighbors(List<Tile> n)
        {
            neighbors.Clear();
            foreach(Tile t in n)
            {
                Node nn = new Node(t, _two, _one);
                neighbors.Add(nn);
            }
        }
    }
}
