using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;

namespace polygon_collision_detection {
    public class line : body {
        private Vector2f startPosition;
        public Vector2f StartPosition {
            get { return startPosition; }
            set {
                startPosition = value;
                length = util.distance(StartPosition, EndPosition);
            }
        }

        public Vector2f StartPositionToWorld {
            get {
                return Position + util.rotate(StartPosition * scale, Angle);
            }
        }

        private Vector2f endPosition;
        public Vector2f EndPosition {
            get { return endPosition; }
            set {
                endPosition = value;
                length = util.distance(StartPosition, EndPosition);
            }
        }

        public Vector2f EndPositionToWorld {
            get {
                return Position + util.rotate(EndPosition * scale, Angle);
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
            va[0] = new Vertex(StartPositionToWorld, Colour);
            va[1] = new Vertex(EndPositionToWorld, Colour);
            window.Draw(va);

            base.draw(window);
        }
    }
}