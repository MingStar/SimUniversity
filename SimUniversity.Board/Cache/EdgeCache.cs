﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MingStar.SimUniversity.Board.Cache
{
    public class EdgeCache
    {
        private Edge _edge;
        private readonly Dictionary<Vertex, List<Edge>> _edgesSharingVertex;

        public EdgeCache(Edge edge)
        {
            _edge = edge;
            _edgesSharingVertex = new Dictionary<Vertex, List<Edge>>();
        }

        public void Cache()
        {
            foreach (var vertex in _edge.Adjacent.Vertices)
            {
                _edgesSharingVertex[vertex] = new List<Edge>();
                foreach (var edge in _edge.Adjacent.Edges)
                {
                    if (edge.Adjacent.Vertices.Contains(vertex))
                    {
                        _edgesSharingVertex[vertex].Add(edge);
                    }
                }
            }            
        }

        internal IEnumerable<Edge> GetAdjacentEdgesSharedWith(Vertex vertex)
        {
            return _edgesSharingVertex[vertex];
        }
    }
}
