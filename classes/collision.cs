using SFML.System;
using SFML.Graphics;

namespace polygon_collision_detection {
    public class collision {
        private bool collided = false;
        public bool Collide => collided;

        entity a;
        entity b;

        public collision(entity a, entity b) {
            
        }

        public static bool pointInsidePolygon(Vector2f p, entity e) {
            if (e.Shape.GetType() != typeof(VertexArray)) { return false; }

            bool Colliding = false;
            VertexArray poly = (VertexArray)e.Shape;
            poly = util.transform(util.rotate(poly, e.Angle), e.Position);

            for (uint i = 0; i < poly.VertexCount; i++) {
                uint j = (i+1) % poly.VertexCount;
                
                Vector2f vc = poly[i].Position;
                Vector2f vn = poly[j].Position;
                float px = p.X;
                float py = p.Y;

                // www.jeffreythompson.org/collision-detection/poly-point.php
                if (((vc.Y >= py && vn.Y <  py) ||
                     (vc.Y <  py && vn.Y >= py)) &&
                     (px < (vn.X - vc.X) * (py - vc.Y) / (vn.Y - vc.Y) + vc.X)) {
                    Colliding = !Colliding;
                }
            }

            return Colliding;
        }
    }
}