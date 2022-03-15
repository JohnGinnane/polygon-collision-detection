using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;

namespace polygon_collision_detection {
    public class polygon : body {
        private List<Vector2f> vertices;
        public List<Vector2f> Vertices => vertices;

        private Color outlineColour;
        public Color OutlineColour {
            get { return outlineColour; }
            set { outlineColour = value; }
        }

        private Color fillColour ;
        public Color FillColour {
            get { return fillColour; }
            set { fillColour = value; }
        }

        public polygon() {
            bodytype = enumBodyType.polygon;
            vertices = new List<Vector2f>();
            OutlineColour = Color.White;
            FillColour = Color.White;
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
            window.Draw(util.VectorsToVertexArray(GetWorldVertices(), OutlineColour, FillColour));

            base.draw(window);
        }
    }
}