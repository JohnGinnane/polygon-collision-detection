using SFML.System;
using SFML.Graphics;

namespace polygon_collision_detection {
    public abstract class body {
        public enum enumBodyType {
            point,
            line,
            circle,
            rectangle,
            polygon
        }

#region "Properties"
        internal enumBodyType bodytype;
        public enumBodyType BodyType => bodytype;

        internal Vector2f position;
        public Vector2f Position => position;

        internal Vector2f velocity;
        public Vector2f Velocity {
            get { return velocity; }
            set { velocity = value; }
        }

        internal float angle;
        public float Angle {
            get { return angle; }
            set { angle = value; }
        }

        internal float angularVelocity;
        public float AngularVelocity {
            get { return angularVelocity; }
            set { angularVelocity = value; }
        }

        internal float scale = 1.0f;
        public float Scale {
            get { return scale; }
            set { scale = value; }
        }

        internal float mass = 100f;
        public float Mass {
            get { return mass; }
            set { mass = value; }
        }
    #endregion
    #region "Methods"
        public void update(float delta) {
            SetPosition(Position + Velocity * delta);
            Angle += AngularVelocity * delta;
        }

        public virtual void draw(RenderWindow window) { }

        public void SetPosition(Vector2f pos) {
            position = pos;
        }

        public void SetXPosition(float x) {
            position.X = x;
        }

        public void SetYPosition(float y) {
            position.Y = y;
        }

        public void SetColour(Color c) {
            switch (bodytype) {
                case body.enumBodyType.point:
                    ((point)this).Colour = c;
                    break;
                case body.enumBodyType.line:
                    ((line)this).Colour = c;
                    break;
                case body.enumBodyType.circle:
                    ((circle)this).FillColour = c;
                    break;
                case body.enumBodyType.rectangle:
                    ((rectangle)this).FillColour = c;
                    break;
                case body.enumBodyType.polygon:
                    ((polygon)this).OutlineColour = c;
                    break;
            }
        }
#endregion
    }
}