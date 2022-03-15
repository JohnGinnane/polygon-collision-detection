using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;

namespace polygon_collision_detection {
    public class collision {
        private bool debug = true;

        private bool collided = false;
        public bool Collided => collided;

        body a;
        body b;

        public collision(body a, body b) {
            this.a = a;
            this.b = b;

            if (a.BodyType == body.enumBodyType.point && 
                b.BodyType == body.enumBodyType.point) {
                collided = intersection.pointInsidePoint(((point)a).Position,
                                                         ((point)b).Position);

                if (collided && debug) {
                    a.SetColour(Color.Red);
                    b.SetColour(Color.Red);
                }
            } else
            if (a.BodyType == body.enumBodyType.point && 
                b.BodyType == body.enumBodyType.circle) {
                collided = intersection.pointInsideCircle(((point)a).Position,
                                                          ((circle)b).Position,
                                                          ((circle)b).Radius);
                                                         
                if (collided && debug) {
                    a.SetColour(Color.Red);
                    b.SetColour(Color.Yellow);
                }
            } else
            if (a.BodyType == body.enumBodyType.circle &&
                b.BodyType == body.enumBodyType.circle) {
                collided = intersection.circleInsideCircle(((circle)a).Position,
                                                           ((circle)a).Radius,
                                                           ((circle)b).Position,
                                                           ((circle)b).Radius);
                                                         
                if (collided && debug) {
                    a.SetColour(Color.Yellow);
                    b.SetColour(Color.Yellow);
                }
            } else
            if (a.BodyType == body.enumBodyType.point &&
                b.BodyType == body.enumBodyType.rectangle) {
                collided = intersection.pointInsideRectangle(((point)a).Position,
                                                             ((rectangle)b).ToFloatRect());
                                                         
                if (collided && debug) {
                    a.SetColour(Color.Red);
                    b.SetColour(Color.Green);
                }
            } else
            if (a.BodyType == body.enumBodyType.rectangle &&
                b.BodyType == body.enumBodyType.rectangle) {
                collided = intersection.rectangleInsideRectangle(((rectangle)a).ToFloatRect(),
                                                                 ((rectangle)b).ToFloatRect());
                                                         
                if (collided && debug) {
                    a.SetColour(Color.Green);
                    b.SetColour(Color.Green);
                }
            } else
            if (a.BodyType == body.enumBodyType.circle &&
                b.BodyType == body.enumBodyType.rectangle) {
                collided = intersection.circleInsideRectangle(((circle)a).Position,
                                                              ((circle)a).Radius,
                                                              ((rectangle)b).ToFloatRect());
                                                         
                if (collided && debug) {
                    a.SetColour(Color.Yellow);
                    b.SetColour(Color.Green);
                }
            } else
            if (a.BodyType == body.enumBodyType.point &&
                b.BodyType == body.enumBodyType.line) {
                collided = intersection.pointInsideLine(((point)a).Position,
                                                        ((line)b).StartPositionToWorld,
                                                        ((line)b).EndPositionToWorld);
                                                         
                if (collided && debug) {
                    a.SetColour(Color.Red);
                    b.SetColour(Color.Blue);
                }
            } else
            if (a.BodyType == body.enumBodyType.line &&
                b.BodyType == body.enumBodyType.circle) {
                collided = intersection.lineInsideCircle(((line)a).StartPositionToWorld,
                                                         ((line)a).EndPositionToWorld,
                                                         ((circle)b).Position,
                                                         ((circle)b).Radius);
                                                         
                if (collided && debug) {
                    a.SetColour(Color.Blue);
                    b.SetColour(Color.Yellow);
                }
            } else
            if (a.BodyType == body.enumBodyType.line &&
                b.BodyType == body.enumBodyType.line) {
                collided = intersection.lineInsideLine(((line)a).StartPositionToWorld,
                                                       ((line)a).EndPositionToWorld,
                                                       ((line)b).StartPositionToWorld,
                                                       ((line)b).EndPositionToWorld);
                                                         
                if (collided && debug) {
                    a.SetColour(Color.Blue);
                    b.SetColour(Color.Blue);
                }
            } else
            if (a.BodyType == body.enumBodyType.line &&
                b.BodyType == body.enumBodyType.rectangle) {
                collided = intersection.lineInsideRectangle(((line)a).StartPositionToWorld,
                                                            ((line)a).EndPositionToWorld,
                                                            ((rectangle)b).ToFloatRect());
                                                         
                if (collided && debug) {
                    a.SetColour(Color.Blue);
                    b.SetColour(Color.Green);
                }
            } else
            if (a.BodyType == body.enumBodyType.point &&
                b.BodyType == body.enumBodyType.polygon) {
                collided = intersection.pointInsidePolygon(((point)a).Position,
                                                           ((polygon)b).GetWorldVertices());
                                                         
                if (collided && debug) {
                    a.SetColour(Color.Red);
                    b.SetColour(Color.Cyan);
                }
            } else
            if (a.BodyType == body.enumBodyType.circle &&
                b.BodyType == body.enumBodyType.polygon) {
                collided = intersection.circleInsidePolygon(((circle)a).Position,
                                                            ((circle)a).Radius,
                                                            ((polygon)b).GetWorldVertices());

                if (collided && debug) {
                    a.SetColour(Color.Yellow);
                    b.SetColour(Color.Cyan);
                }
            } else
            if (a.BodyType == body.enumBodyType.rectangle &&
                b.BodyType == body.enumBodyType.polygon) {
                collided = intersection.rectangleInsidePolygon(((rectangle)a).ToFloatRect(),
                                                               ((polygon)b).GetWorldVertices());
                
                if (collided && debug) {
                    a.SetColour(Color.Green);
                    b.SetColour(Color.Cyan);
                }
            } else
            if (a.BodyType == body.enumBodyType.line &&
                b.BodyType == body.enumBodyType.polygon) {
                collided = intersection.lineInsidePolygon(((line)a).StartPositionToWorld,
                                                            ((line)a).EndPositionToWorld,
                                                            ((polygon)b).GetWorldVertices());
                
                if (collided && debug) {
                    a.SetColour(Color.Blue);
                    b.SetColour(Color.Cyan);
                }
            } else
            if (a.BodyType == body.enumBodyType.polygon && 
                b.BodyType == body.enumBodyType.polygon) {
                collided = intersection.polygonInsidePolygon(((polygon)a).GetWorldVertices(),
                                                             ((polygon)b).GetWorldVertices());

                if (collided && debug) {
                    a.SetColour(Color.Cyan);
                    b.SetColour(Color.Cyan);
                }
            }
        }
    }
}