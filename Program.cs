﻿using System;
using System.Collections.Generic;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace polygon_collision_detection
{
    class Program
    {
        static void Main(string[] args)
        {
            Global.ScreenSize = new Vector2f(800, 600);
            PolygonCollisionDetectionDemo demo = new PolygonCollisionDetectionDemo();
            demo.run();
        }
    }

    public class PolygonCollisionDetectionDemo {
        public RenderWindow window;
        private const float timeStep = 1 / 60.0f;
        private float timeScale = 1.0f;
        private DateTime lastTime;

        // The point we want to check if it's inside a polygon
        point mousePoint;

        bool pointInsidePoly;
        List<body> bodies;
        
        public PolygonCollisionDetectionDemo() {
            window = new RenderWindow(new VideoMode((uint)Global.ScreenSize.X, (uint)Global.ScreenSize.Y), "Polygon Collision Detection Demo", Styles.Close);
            View view = new View(Global.ScreenSize/2f, Global.ScreenSize);
            window.SetView(view);
            window.SetKeyRepeatEnabled(false);
            window.Closed += window_CloseWindow;

            bodies = new List<body>();

            mousePoint = new point();
            bodies.Add(mousePoint);

            int numEnts = 6;
            uint vertexCount = 6;
            float minRadius = 20f;
            float maxRadius = minRadius + minRadius;
            float angIncrement = 2f * (float)Math.PI / vertexCount;

            // Create random polygons for us to test collision detection
            for (int i = 0; i < numEnts; i++) {
                switch (util.randint(0, 0)) {
                    case 0:
                        polygon p = new polygon();
                        p.SetPosition(util.randvec2(0, Global.ScreenSize.X, 0, Global.ScreenSize.Y));
                        p.Velocity = util.randvec2(-10, 10);
                        p.AngularVelocity = util.randfloat(-1, 1);                        
                        
                        List<Vector2f> verts = new List<Vector2f>();
                        for (int j = 0; j < vertexCount; j++) {
                            float angle = angIncrement * j;
                            float radius = util.randfloat(minRadius, maxRadius);

                            verts.Add(new Vector2f((float)Math.Sin(angle) * radius,
                                                   (float)Math.Cos(angle) * radius));
                        }
                        verts.Add(verts[0]);
                        p.SetVertices(verts);

                        bodies.Add(p);
                        break;
                    case 1:
                        break;
                }
            }
        }

        public void window_CloseWindow(object sender, EventArgs e) {
            if (sender == null) { return; }
            window.Close();
        }

        public void run() {
            while (window.IsOpen) {
                if (!window.HasFocus()) { continue; }

                if ((float)(DateTime.Now - lastTime).TotalMilliseconds / 1000f >= timeStep) {
                    float delta = timeStep * timeScale;
                    lastTime = DateTime.Now;

                    window.DispatchEvents();
                    update(delta);
                }

                draw();
            }
        }

        public void update(float delta) {
            Global.Keyboard.update();
            Global.Mouse.update(window);
            
            if (Global.Keyboard["escape"].isPressed) {
                window.Close();
            }

            mousePoint.SetPosition((Vector2f)Global.Mouse.Position);
            pointInsidePoly = false;

            for (int i = 0; i < bodies.Count; i++) {
                body b = bodies[i];
                
                if (b == mousePoint) { continue; }

                b.update(delta);

                if (!pointInsidePoly && b.BodyType == body.enumBodyType.polygon) {
                    pointInsidePoly = collision.pointInsidePolygon(mousePoint, (polygon)b);
                }

                // wrap objects around screen
                if (b.Position.X < 0) { b.SetXPosition(Global.ScreenSize.X); }
                if (b.Position.X > Global.ScreenSize.X) { b.SetXPosition(0); }
                if (b.Position.Y < 0) { b.SetYPosition(Global.ScreenSize.Y); }
                if (b.Position.Y > Global.ScreenSize.Y) { b.SetYPosition(0); }
            }
        }

        public void draw() {
            window.Clear();
            
            if (pointInsidePoly) {
                mousePoint.Colour = Color.Red;
            } else {
                mousePoint.Colour = Color.Green;
            }

            for (int i = 0; i < bodies.Count; i++) {
                bodies[i].draw(window);
            }

            //demoPointInsideCircle();
            //demoCircleInsideCircle();
            //demoPointInsideRectangle();
            //demoRectangleInsideRectangle();
            //demoCircleInsideRectangle();
            //demoPointInsideLine();
            //demoLineInsideCircle();
            demoLineInsideLine();

            // draw cursor position text
            Text cursorText = new Text(string.Format("{0}, {1}", Global.Mouse.Position.X, Global.Mouse.Position.Y), Fonts.Arial);
            cursorText.Position = (Vector2f)Global.Mouse.Position + new Vector2f(10, 10);
            cursorText.FillColor = Color.White;
            cursorText.CharacterSize = 12;
            cursorText.OutlineColor = Color.Black;
            cursorText.OutlineThickness = 1f;
            window.Draw(cursorText);
            
            window.Display();
        }

#region "Demo Functions"
        public void demoPointInsideCircle() {
            CircleShape cs = new CircleShape(60);
            cs.Origin = new Vector2f(60, 60);
            cs.Position = new Vector2f(100, 200);

            if (intersection.pointInsideCircle(mousePoint.Position, cs.Position, cs.Radius)) {
                cs.FillColor = Color.Green;
            } else {
                cs.FillColor = Color.White;
            }

            window.Draw(cs);
        }

        public void demoCircleInsideCircle() {
            CircleShape cs = new CircleShape(100f);
            cs.Origin = new Vector2f(100, 100);
            cs.Position = Global.ScreenSize / 2f;

            CircleShape mouseCs = new CircleShape(50);
            mouseCs.Origin = new Vector2f(50, 50);
            mouseCs.Position = mousePoint.Position;

            if (intersection.circleInsideCircle(mouseCs.Position, mouseCs.Radius, cs.Position, cs.Radius)) {
                cs.FillColor = Color.Cyan;
                mouseCs.FillColor = Color.Red;
            } else {
                cs.FillColor = Color.White;
                mouseCs.FillColor = Color.White;
            }

            window.Draw(cs);
            window.Draw(mouseCs);
        }

        public void demoPointInsideRectangle() {
            RectangleShape rs = new RectangleShape(new Vector2f(100, 200));
            rs.Position = Global.ScreenSize / 2f;
            
            if (intersection.pointInsideRectangle(mousePoint.Position, new FloatRect(rs.Position, rs.Size))) {
                rs.FillColor = Color.Red;
            } else {
                rs.FillColor = Color.White;
            }

            window.Draw(rs);
        }

        public void demoRectangleInsideRectangle() {
            // target rectangle
            RectangleShape targetRs = new RectangleShape(new Vector2f(300, 150));
            targetRs.Position = Global.ScreenSize / 2f;
            
            // mouse rectangle
            RectangleShape mouseRs = new RectangleShape(new Vector2f(50, 80));
            mouseRs.Position = mousePoint.Position;

            if (intersection.rectangleInsideRectangle(new FloatRect(mouseRs.Position, mouseRs.Size),
                                                      new FloatRect(targetRs.Position, targetRs.Size))) {
                targetRs.FillColor = Color.Red;
                mouseRs.FillColor = Color.Blue;
            } else {
                targetRs.FillColor = Color.White;
                mouseRs.FillColor = Color.Yellow;
            }

            window.Draw(targetRs);
            window.Draw(mouseRs);            
        }

        public void demoPointInsideLine() {
            // target line
            line L = new line();
            L.Position = new Vector2f(100, 100);
            L.EndPosition = new Vector2f(400, 600);

            if (intersection.pointInsideLine(mousePoint.Position, L.Position, L.EndPosition)) {
                L.Colour = Color.Green;
            } else {
                L.Colour = Color.White;
            }

            L.draw(window);
        }

        public void demoLineInsideCircle() {
            // target line
            line L = new line();
            L.Position = new Vector2f(300, 50);
            L.EndPosition = new Vector2f(500, 350);
            L.draw(window);

            // target circle
            CircleShape cs = new CircleShape(50);
            cs.Origin = new Vector2f(50, 50);
            cs.Position = mousePoint.Position;

            if (intersection.lineInsideCircle(L.Position, L.EndPosition, cs.Position, cs.Radius)) {
                cs.FillColor = Color.Yellow;
            } else {
                cs.FillColor = Color.White;
            }

            window.Draw(cs);
        }

        public void demoLineInsideLine() {
            line lineA = new line();
            lineA.Position = new Vector2f(20, 20);
            lineA.EndPosition = new Vector2f(200, 200);
            
            line lineB = new line();
            lineB.Position = Global.ScreenSize / 2f;
            lineB.EndPosition = mousePoint.Position;

            if (intersection.lineInsideLine(lineA.Position, lineA.EndPosition, lineB.Position, lineB.EndPosition)) {
                lineA.Colour = Color.Red;
                lineB.Colour = Color.Blue;
            } else {
                lineA.Colour = Color.White;
                lineB.Colour = Color.White;
            }

            lineA.draw(window);
            lineB.draw(window);
        }

        public void demoCircleInsideRectangle() {
            // rectangle
            RectangleShape targetRs = new RectangleShape(new Vector2f(50, 400));
            targetRs.Position = Global.ScreenSize / 2f;
            targetRs.OutlineColor = Color.Black;
            targetRs.OutlineThickness = 1f;

            // mouse circle
            CircleShape mouseCs = new CircleShape(75);
            mouseCs.Origin = new Vector2f(75, 75);
            mouseCs.Position = mousePoint.Position;
            mouseCs.OutlineColor = Color.Black;
            mouseCs.OutlineThickness = 1f;

            if (intersection.circleInsideRectangle(mouseCs.Position, mouseCs.Radius, util.RectShapeToFloatRect(targetRs))) {
                mouseCs.FillColor = Color.Red;
                targetRs.FillColor = Color.Blue;
            } else {
                mouseCs.FillColor = Color.White;
                targetRs.FillColor = Color.White;
            }
            
            window.Draw(targetRs);
            window.Draw(mouseCs);
        }
#endregion
    }
}
