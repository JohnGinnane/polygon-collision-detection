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
            return intersection.pointInsidePolygon(p.Position, e.GetWorldVertices());
        }
    }
}