using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;

namespace polygon_collision_detection {
    public static class intersection {
        public static bool pointInsidePoint(Vector2f a, Vector2f b) {
            return (a.X == b.X && a.Y == b.Y);
        }

        public static bool pointInsideCircle(Vector2f point, Vector2f circlePos, float circleRadius) {
            
            float distX = point.X - circlePos.X;
            float distY = point.Y - circlePos.Y;
            
            // We don't need to use square root here, we can just 
            // compare the squared values
            float distSq = distX * distX + distY * distY;
            float radSq = circleRadius * circleRadius;

            return distSq < radSq;
        }

        public static bool circleInsideCircle(Vector2f aPos, float aRadius, Vector2f bPos, float bRadius) {
            float distX = aPos.X - bPos.X;
            float distY = aPos.Y - bPos.Y;
            
            float distSq = distX * distX + distY * distY;
            float radSq = (aRadius + bRadius) * (aRadius + bRadius);

            return distSq < radSq;
        }

        public static bool pointInsideRectangle(Vector2f p, FloatRect r) {
            return (p.X >= r.Left && p.X <= r.Left + r.Width && p.Y >= r.Top && p.Y <= r.Top + r.Height);
        }

        public static bool rectangleInsideRectangle(FloatRect a, FloatRect b) {
            if (a.Left + a.Width >= b.Left &&
                a.Left           <= b.Left + b.Width && 
                a.Top + a.Height >= b.Top &&
                a.Top            <= b.Top + b.Height) {
                return true;
            }

            return false;
        }

        public static bool circleInsideRectangle(Vector2f circlePos, float circleRadius, FloatRect rect) {
            float testX = circlePos.X;
            float testY = circlePos.Y;

            if (circlePos.X < rect.Left) {
                // left edge
                testX = rect.Left;
            } else if (circlePos.X > rect.Left + rect.Width) {
                // right edge
                testX = rect.Left + rect.Width;
            }

            if (circlePos.Y < rect.Top) {
                // top edge
                testY = rect.Top;
            } else if (circlePos.Y > rect.Top + rect.Height) {
                // bottom edge
                testY = rect.Top + rect.Height;
            }

            return pointInsideCircle(new Vector2f(testX, testY), circlePos, circleRadius);
        }

        public static bool pointInsidePolygon(Vector2f point, List<Vector2f> polygon) {
            bool intersects = false;
            
            for (int i = 0; i < polygon.Count; i++) {
                int j = (i+1) % polygon.Count;
                
                Vector2f vc = polygon[i];
                Vector2f vn = polygon[j];
                float px = point.X;
                float py = point.Y;

                // www.jeffreythompson.org/collision-detection/poly-point.php
                if (((vc.Y >= py && vn.Y <  py) ||
                     (vc.Y <  py && vn.Y >= py)) &&
                     (px < (vn.X - vc.X) * (py - vc.Y) / (vn.Y - vc.Y) + vc.X)) {
                    intersects = !intersects;
                }
            }

            return intersects;
        }
    }
}