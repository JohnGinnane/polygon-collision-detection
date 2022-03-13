using SFML.System;
using SFML.Graphics;

namespace polygon_collision_detection {
    public abstract class body {
        public enum enumBodyType {
            point,
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

        internal Drawable shape;
        public Drawable Shape {
            get { return shape; }
            set { shape = value; }
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
#endregion
    }
}