using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;

namespace polygon_collision_detection {
    public class polygon : body {
        private List<Vector2f> vertices;
        public List<Vector2f> Vertices => vertices;

        public polygon() {
            bodytype = enumBodyType.polygon;
            vertices = new List<Vector2f>();
        }

        public void SetVertices(List<Vector2f> newVerts) {
            vertices = newVerts;
        }

        public List<Vector2f> GetWorldVertices() {
            List<Vector2f> output = new List<Vector2f>();

            for (int i = 0; i < Vertices.Count; i++) {
                output.Add(Position + util.rotate(Vertices[i], Angle));
            }

            return output;
        }

        public override void draw(RenderWindow window)
        {
            window.Draw(util.VectorsToVertexArray(GetWorldVertices()));

            base.draw(window);
        }
    }
}