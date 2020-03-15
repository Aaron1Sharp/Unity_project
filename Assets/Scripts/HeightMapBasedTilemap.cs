﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace CustomTilemap
{
    public class HeightMapBasedTilemap : ITileMap
    {
        public int Count => _heights.Sum();
        public int Width => _heights.Length;
        public int Height => _heights.Max();

        private int[] _heights;
        private ICell _cell;
        public HeightMapBasedTilemap(int wigth, ICell cell)
        {
            _heights = new int[wigth];
            _cell = cell;
        }
        public void SetHeight(int x, int value)
        {
            if (x >= 0 || x < _heights.Length)
            {
                _heights[x] = value;
            }
            else throw new System.ArgumentOutOfRangeException("x");
        }
        public ICell GetCell(Vector2Int _position)
        {
            if (_position.x < 0 && _position.x >= _heights.Length) throw new System.ArgumentOutOfRangeException("x");
            return _position.y > _heights[_position.x] ? null : _cell;
        }
        public Vector2[] GetCloseMash()
        {
            const float _halfCellSize = 0.5f;

            List<Vector2> _points = new List<Vector2>();
            for (int x = 0; x < Width; x++)
            {
                _points.Add(new Vector2(x - _halfCellSize, _heights[x] + _halfCellSize));
                _points.Add(new Vector2(x + _halfCellSize, _heights[x] + _halfCellSize));
            }
            _points.Add(new Vector2(Width - _halfCellSize, -_halfCellSize));
            _points.Add(new Vector2(-_halfCellSize, -_halfCellSize));

            return _points.ToArray();
        }
    }
}