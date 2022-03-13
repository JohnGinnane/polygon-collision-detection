using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;

namespace polygon_collision_detection {
    public class collision {
        private bool collided = false;
        public bool Collide => collided;

        body a;
        body b;

        public collision(body a, body b) {
            this.a = a;
            this.b = b;

            if (a.BodyType == body.enumBodyType.point && b.BodyType == body.enumBodyType.polygon) {
                collided = pointInsidePolygon((point)a, (polygon)b);
            }
        }

        public static bool pointInsidePolygon(point p, polygon e) {
            bool Colliding = false;
            List<Vector2f> poly = e.GetWorldVertices();

            for (int i = 0; i < poly.Count; i++) {
                int j = (i+1) % poly.Count;
                
                Vector2f vc = poly[i];
                Vector2f vn = poly[j];
                float px = p.Position.X;
                float py = p.Position.Y;

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