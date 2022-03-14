using SFML.System;
using SFML.Graphics;

namespace polygon_collision_detection {
    public class line : body {
        private Vector2f endPosition;
        public Vector2f EndPosition {
            get { return endPosition; }
            set {
                endPosition = value;
                length = util.distance(Position, EndPosition);
            }
        }
        
        public new Vector2f Position {
            get { return position; }
            set {
                position = value;
                length = util.distance(Position, EndPosition);
            }
        }

        private Color colour;
        public Color Colour {
            get { return colour; }
            set { colour = value; }
        }

        private float length;
        public float Length {
            get { return length; }
        }

        public line() {
            colour = Color.White;
        }

        public override void draw(RenderWindow window)
        {
            VertexArray va = new VertexArray(PrimitiveType.LineStrip, 2);
            va[0] = new Vertex(Position, Colour);
            va[1] = new Vertex(EndPosition, Colour);
            window.Draw(va);

            base.draw(window);
        }
    }
}