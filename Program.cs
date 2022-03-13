using System;
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
        VertexArray polygon;
        bool Colliding = false;
        
        public PolygonCollisionDetectionDemo() {
            window = new RenderWindow(new VideoMode((uint)Global.ScreenSize.X, (uint)Global.ScreenSize.Y), "Polygon Collision Detection Demo", Styles.Close);
            View view = new View(Global.ScreenSize/2f, Global.ScreenSize);
            window.SetView(view);
            window.SetKeyRepeatEnabled(false);
            window.Closed += window_CloseWindow;

            uint vertexCount = 16;
            float minRadius = 80f;
            float maxRadius = minRadius + 80;
            float angIncrement = 2f * (float)Math.PI / vertexCount;

            polygon = new VertexArray(PrimitiveType.LineStrip, vertexCount + 1);

            for (uint i = 0; i < vertexCount; i++) {
                Vector2f center = Global.ScreenSize / 2f;
                float radius = util.randfloat(minRadius, maxRadius);
                float angle = angIncrement * i;

                Vertex v = new Vertex(center + new Vector2f((float)Math.Sin(angle) * radius, (float)Math.Cos(angle) * radius), Color.White);
                polygon[i] = v;
            }

            polygon[vertexCount] = new Vertex(polygon[0].Position, polygon[0].Color);
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

            Colliding = false;
            // Check if point is inside the polygon
            for (uint i = 0; i < polygon.VertexCount; i++) {
                uint j = i + 1;
                if (j >= polygon.VertexCount) { j = 0; }

                Vector2f vc = polygon[i].Position;
                Vector2f vn = polygon[j].Position;
                float px = point.X;
                float py = point.Y;

                // www.jeffreythompson.org/collision-detection/poly-point.php
                if (((vc.Y >= py && vn.Y <  py) ||
                     (vc.Y <  py && vn.Y >= py)) &&
                     (px < (vn.X - vc.X) * (py - vc.Y) / (vn.Y - vc.Y) + vc.X)) {
                    Colliding = !Colliding;
                }
            }
        }

        public void draw() {
            window.Clear();
            
            window.Draw(polygon);

            // draw small circles on each vertex
            for (uint i = 0; i < polygon.VertexCount; i++) {
                CircleShape vcs = new CircleShape(2f);
                vcs.Position = polygon[i].Position;
                vcs.Origin = new Vector2f(vcs.Radius, vcs.Radius);
                vcs.FillColor = Color.Red;
                window.Draw(vcs);
                
                // draw cursor position text
                if (i < polygon.VertexCount - 1) {
                    Vector2i pos = (Vector2i)polygon[i].Position;
                    Text pointText = new Text(string.Format("{0}, {1}", pos.X, pos.Y), Fonts.Arial);
                    pointText.Position = (Vector2f)pos + new Vector2f(5, 5);
                    pointText.FillColor = Color.White;
                    pointText.CharacterSize = 16;
                    window.Draw(pointText);
                }                
            }

            // draw circle where cursor is
            CircleShape cs = new CircleShape(5f);
            cs.Position = point;
            cs.Origin = new Vector2f(cs.Radius, cs.Radius);
            
            if (Colliding) {
                cs.FillColor = Color.Green;
            } else {
                cs.FillColor = Color.Blue; 
            }

            window.Draw(cs);

            // draw cursor position text
            Text cursorText = new Text(string.Format("{0}, {1}", Global.Mouse.Position.X, Global.Mouse.Position.Y), Fonts.Arial);
            cursorText.Position = (Vector2f)Global.Mouse.Position + new Vector2f(20, 20);
            cursorText.FillColor = Color.White;
            cursorText.CharacterSize = 16;
            window.Draw(cursorText);
            
            window.Display();
        }
    }
}
