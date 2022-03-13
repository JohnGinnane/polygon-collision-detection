using System;
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
        Vector2f point;
        bool pointInsidePoly;
        List<entity> entities;
        
        public PolygonCollisionDetectionDemo() {
            window = new RenderWindow(new VideoMode((uint)Global.ScreenSize.X, (uint)Global.ScreenSize.Y), "Polygon Collision Detection Demo", Styles.Close);
            View view = new View(Global.ScreenSize/2f, Global.ScreenSize);
            window.SetView(view);
            window.SetKeyRepeatEnabled(false);
            window.Closed += window_CloseWindow;

            entities = new List<entity>();

            int numEnts = 6;
            uint vertexCount = 6;
            float minRadius = 20f;
            float maxRadius = minRadius + minRadius;
            float angIncrement = 2f * (float)Math.PI / vertexCount;

            for (int i = 0; i < numEnts; i++) {
                entity e = new entity();
                e.SetPosition(util.randvec2(0, Global.ScreenSize.X, 0, Global.ScreenSize.Y));
                e.Velocity = util.randvec2(-10, 10);
                e.AngularVelocity = util.randfloat(-1, 1);

                switch (util.randint(0, 0)) {
                    case 0:
                        VertexArray va = new VertexArray(PrimitiveType.LineStrip, vertexCount + 1);
                        for (uint j = 0; j < vertexCount; j++) {
                            float radius = util.randfloat(minRadius, maxRadius);
                            float angle = angIncrement * j;
                            Vertex v = new Vertex();
                            v.Position = new Vector2f((float)Math.Sin(angle) * radius,
                                                      (float)Math.Cos(angle) * radius);
                            v.Color = Color.White;
                            va[j] = v;
                        }
                        va[vertexCount] = new Vertex(va[0].Position, va[0].Color);
                        e.Shape = va;
                        break;
                    case 1:
                        break;
                }

                entities.Add(e);
            }

            // polygon = new VertexArray(PrimitiveType.LineStrip, vertexCount + 1);
            // for (uint i = 0; i < vertexCount; i++) {
            //     Vector2f center = Global.ScreenSize / 2f;
            //     float radius = util.randfloat(minRadius, maxRadius);
            //     float angle = angIncrement * i * 2;

            //     if (i * 2 > vertexCount) {
            //         angle = angIncrement * vertexCount / 2 - (angIncrement * ((i * 2) - vertexCount / 2));
            //         radius -= minRadius;
            //     }

            //     Vertex v = new Vertex(center + new Vector2f((float)Math.Sin(angle) * radius, (float)Math.Cos(angle) * radius), Color.White);
            //     polygon[i] = v;
            // }
            // polygon[vertexCount] = new Vertex(polygon[0].Position, polygon[0].Color);
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

            point = (Vector2f)Global.Mouse.Position;
            pointInsidePoly = false;

            for (int i = 0; i < entities.Count; i++) {
                entity e = entities[i];

                e.update(delta);

                if (!pointInsidePoly) {
                    if (entities[i].Shape.GetType() == typeof(VertexArray)) {
                        pointInsidePoly = collision.pointInsidePolygon(point, e);
                    }
                }

                // wrap objects around screen
                if (e.Position.X < 0) { e.SetXPosition(Global.ScreenSize.X); }
                if (e.Position.X > Global.ScreenSize.X) { e.SetXPosition(0); }
                if (e.Position.Y < 0) { e.SetYPosition(Global.ScreenSize.Y); }
                if (e.Position.Y > Global.ScreenSize.Y) { e.SetYPosition(0); }
            }
        }

        public void draw() {
            window.Clear();
            
            for (int i = 0; i < entities.Count; i++) {
                entities[i].draw(window);
            }
            
            // draw circle where cursor is
            CircleShape cs = new CircleShape(5f);
            cs.Position = point;
            cs.Origin = new Vector2f(cs.Radius, cs.Radius);
            
            if (pointInsidePoly) {
                cs.FillColor = Color.Green;
            } else {
                cs.FillColor = Color.Blue; 
            }

            window.Draw(cs);

            // draw cursor position text
            Text cursorText = new Text(string.Format("{0}, {1}", Global.Mouse.Position.X, Global.Mouse.Position.Y), Fonts.Arial);
            cursorText.Position = (Vector2f)Global.Mouse.Position + new Vector2f(10, 10);
            cursorText.FillColor = Color.White;
            cursorText.CharacterSize = 12;
            window.Draw(cursorText);
            
            window.Display();
        }
    }
}
